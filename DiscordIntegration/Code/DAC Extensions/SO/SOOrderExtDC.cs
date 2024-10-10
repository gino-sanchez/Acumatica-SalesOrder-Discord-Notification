using CRLocation = PX.Objects.CR.Standalone.Location;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.RelatedItems;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO.Attributes;
using PX.Objects.SO.Interfaces;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace PX.Objects.SO
{
    public class SOOrderExtDC : PXCacheExtension<PX.Objects.SO.SOOrder>
    {
        #region UsrNotificationSent

        [PXDBBool]
        [PXUIField(DisplayName = "Notification Sent",Enabled = false)]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIVisible(typeof(Where<SOOrderExtDC.usrNotificationSent,Equal<boolTrue>>))]
        public virtual bool? UsrNotificationSent { get; set; }
        public abstract class usrNotificationSent : PX.Data.BQL.BqlBool.Field<usrNotificationSent> { }

        #endregion
    }
}