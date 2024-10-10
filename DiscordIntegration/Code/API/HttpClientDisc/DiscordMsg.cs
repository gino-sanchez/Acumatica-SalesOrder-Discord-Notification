using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using PX.Objects.SO;
using PX.Data;
using System.Web;
using PX.Data.Update;
using PX.Data.BQL.Fluent;
using PX.Objects.CA.Light;
using PX.Data.BQL;
using Newtonsoft.Json;
using System.Security.Policy;
using PX.Objects.CM;
namespace DiscordIntegration.Code.API.HttpClientDisc
{
    internal class DiscordMsg
    {
       
        public static IEnumerable<List<T>> ChunkBy<T>(List<T> source, int chunkSize)
        {
            for (int i = 0; i < source.Count; i += chunkSize)
            {
                yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
            }
        }

        public static int GenerateRandomColor(Random random)
        {
            int red = random.Next(0, 256);
            int green = random.Next(0, 256);
            int blue = random.Next(0, 256);

            // Convert RGB to an integer (Discord embed expects an int for color)
            return (red << 16) + (green << 8) + blue;
        }

        public HttpResponseMessage SendDiscordMsg(SOOrder order, PXCache sender, DCWebhookSetup setup, List<SOLine> soLine)
        {
            if (soLine == null || !soLine.Any())
            {
                PXTrace.WriteError("SOLine list is null or empty.");
                return null;
            }

            if (order == null)
            {
                PXTrace.WriteError("Order is null.");
                return null;
            }

            if (setup == null || string.IsNullOrEmpty(setup.Url) || string.IsNullOrEmpty(setup.WebhookID) || string.IsNullOrEmpty(setup.WebhookToken))
            {
                PXTrace.WriteError("Webhook setup is invalid.");
                return null;
            }
            Random random = new Random();
            var chunks = ChunkBy(soLine, 10).ToList();
            HttpResponseMessage response = null;
            string localhost = "localhost/24r1";
            string hostName = string.Format("http://{0}/main?ScreenID=SO301000&OrderType={1}&OrderNbr={2}", localhost, order.OrderType,order.OrderNbr);
            PXTrace.WriteInformation("hostName: " + hostName);
            string curySymbol = SelectFrom<Currency>.Where<Currency.curyID.IsEqual<@P.AsString>>.View.Select(sender.Graph,order.CuryID).RowCast<Currency>().Select(a => a.CurySymbol).First();
            string customerName = SelectFrom<Customer>.Where<Customer.bAccountID.IsEqual<@P.AsInt>>.View.Select(sender.Graph,order.CustomerID).RowCast<Customer>().Select(a => a.AcctName).First();
            int x = 1; 
            for (int i = 0; i < chunks.Count; i++)
            {
                var chunk = chunks[i]; // Get the current chunk
                object webHookMsg;
                if (i > 0)
                {
                    webHookMsg = new
                    {
                        username = "Acumatica Sales Order " + order.OrderNbr,
                        avatar_url = "https://logowik.com/content/uploads/images/acumatica-cloud-erp-system3793.logowik.com.webp",
                        embeds = chunk.Select(soLines => new
                        {
                            title = soLines.TranDesc,
                            color = GenerateRandomColor(random),
                            description = string.Format("**Line** {0}" + "\n" + "**Order Qty:** {1}" + "\n"
                            + "**Unit Price:** {2}" + "\n" + "**Ext Price:** {3}",x++ ,soLines.OrderQty, soLines.UnitPrice, soLines.ExtPrice)
                        }).ToList()
                    };
                }
                else
                {
                    webHookMsg = new
                    {
                        username = "Acumatica Sales Order" + order.OrderNbr,
                        content = string.Format("**Customer:** {0}" + "\n" + "**Order Total:** {1}{2}" + "\n" + "**Order Date:** {3}" + "\n" + "**Order Description:** {4}" + "\n"
                        + "# **DOCUMENT LINES**"
                       , customerName, curySymbol, order.OrderTotal, order.OrderDate.Value.ToString("MM-dd-yyyy"), order.OrderDesc),
                        avatar_url = "https://logowik.com/content/uploads/images/acumatica-cloud-erp-system3793.logowik.com.webp",
                        embeds = chunk.Select(soLines => new
                        {
                            title = soLines.TranDesc,
                            color = GenerateRandomColor(random),
                            description = string.Format("**Line** {0}" + "\n" + "**Order Qty:** {1}" + "\n"
                            + "**Unit Price:** {2}" + "\n" + "**Ext Price:** {3}",x++ ,soLines.OrderQty, soLines.UnitPrice, soLines.ExtPrice)
                        }).ToList()
                    };
                }
                
                PXTrace.WriteInformation("Json: {0}", JsonConvert.SerializeObject(webHookMsg));
                string endpoint = string.Format("{0}/{1}/{2}", setup.Url, setup.WebhookID, setup.WebhookToken);
                PXTrace.WriteInformation("Endpoint: " + endpoint);
                var content = new StringContent(JsonConvert.SerializeObject(webHookMsg), Encoding.UTF8, "application/json");
                using (var client = new HttpClient()) // Ensure the client is instantiated here
                {
                    var httpResponse = client.PostAsync(endpoint, content).Result;
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        response = httpResponse;
                        PXTrace.WriteInformation("Success Discord Notification");
                    }
                    else
                    {
                        response = httpResponse;
                        var errorMessage = httpResponse.Content.ReadAsStringAsync().Result;
                        PXTrace.WriteError($"Failed to send message. Status Code: {httpResponse.StatusCode}. Error: {errorMessage}");
                    }
                }
            }
            return response;
        }
    }
}
