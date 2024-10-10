using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects;
using PX.Objects.SO;
using PX.Objects.AR;
using System.Linq;
using PX.Objects.IN;
using System.Collections;
namespace DiscordIntegration
{
	public class DiscordSetupMaint : PXGraph<DiscordSetupMaint>
	{
		public SelectFrom<DCWebhookSetup>.View Setup;
		public PXSave<DCWebhookSetup> Save;
		public PXCancel<DCWebhookSetup> Cancel;


		#region Actions

		public PXAction<DCWebhookSetup> TestSalesOrder;
		[PXButton]
		[PXUIField(DisplayName = "Test Sales Order Create 5 SalesOrder")]

		public virtual IEnumerable testSalesOrder(PXAdapter adapter)
		{
            
            PXLongOperation.StartOperation(this, delegate ()
			{
                var graph = PXGraph.CreateInstance<SOOrderEntry>();
                for (int i = 0; i <= 4; i++)
				{
                    graph.Clear();
					graph.SelectTimeStamp();

					var insertSODocument = graph.Document.Insert(new SOOrder()
					{
						CustomerID = SelectFrom<Customer>.View.Select(graph).RowCast<Customer>().ToList().Select(a => a.BAccountID).First(),
						OrderDesc = string.Format("Test Document {0}", i)
					});

					var insertSOLine = graph.Transactions.Insert(new SOLine()
					{
						InventoryID = SelectFrom<InventoryItem>.View.Select(graph).RowCast<InventoryItem>().ToList().Select(a => a.InventoryID).First(),
						OrderQty = 10
					});

					graph.Save.Press();
				}
			});

			return adapter.Get();
		}

		#endregion
	}
}