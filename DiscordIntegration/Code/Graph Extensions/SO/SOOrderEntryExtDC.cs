using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using POLine = PX.Objects.PO.POLine;
using POOrder = PX.Objects.PO.POOrder;
using PX.CarrierService;
using PX.Concurrency;
using CRLocation = PX.Objects.CR.Standalone.Location;
using ARRegisterAlias = PX.Objects.AR.Standalone.ARRegisterAlias;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Extensions;
using PX.CS.Contracts.Interfaces;
using Message = PX.CarrierService.Message;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.SO.GraphExtensions.CarrierRates;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.Objects.SO.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using OrderActions = PX.Objects.SO.SOOrderEntryActionsAttribute;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.InventoryRelease;
using PX.Data.BQL;
using PX.Objects.IN.InventoryRelease.Utility;
using PX.Objects.SO.Standalone;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.Common.Interfaces;
using PX.Objects;
using PX.Objects.SO;
using DiscordIntegration.Code.API.HttpClientDisc;
using DiscordIntegration;
using System.Net.Http;
namespace PX.Objects.SO
{
    public class SOOrderEntryExtDC : PXGraphExtension<PX.Objects.SO.SOOrderEntry>
    { 
        public PXSetup<DCWebhookSetup> DiscordSetup;
    

        #region Overrides

        public delegate void PersistDelegate();
        [PXOverride]
        public void Persist(PersistDelegate baseMethod)
        {
            var row = Base.Document.Current;
            PXCache sender = Base.Document.Cache;
            var stp = DiscordSetup.Current;
            baseMethod();
            var rowExt = sender.GetExtension<SOOrderExtDC>(row);

            if (rowExt.UsrNotificationSent != true && stp.EnableNotification == true)
            {
                DiscordMsg msg = new DiscordMsg();
                if (msg.SendDiscordMsg(row, sender, stp, Base.Transactions.Select().RowCast<SOLine>().ToList()).IsSuccessStatusCode)
                {
                    PXDatabase.Update<SOOrder>(
                        new PXDataFieldAssign<SOOrderExtDC.usrNotificationSent>(true),
                        new PXDataFieldRestrict<SOOrder.orderNbr>(row.OrderNbr),
                        new PXDataFieldRestrict<SOOrder.orderType>(row.OrderType));
                }
            }
        }

        #endregion
    }
}