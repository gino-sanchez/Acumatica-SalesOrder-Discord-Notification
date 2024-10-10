using System;
using PX.Data;
using PX.Objects.CS;

namespace DiscordIntegration
{
	[Serializable]
	[PXCacheName("DCWebhookSetup")]
	public class DCWebhookSetup : PXBqlTable, IBqlTable
	{
		#region EnableNotification
		[PXDBBool()]
		[PXUIField(DisplayName = "Enable Notification")]
		[PXDefault(false,PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? EnableNotification { get; set; }
		public abstract class enableNotification : PX.Data.BQL.BqlBool.Field<enableNotification> { }
		#endregion

		#region WebhookID
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Webhook ID")]
		[PXUIVisible(typeof(Where<enableNotification,Equal<boolTrue>>))]
		public virtual string WebhookID { get; set; }
		public abstract class webhookID : PX.Data.BQL.BqlString.Field<webhookID> { }
		#endregion

		#region WebhookToken
		[PXDBString(100, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Webhook Token")]
        [PXUIVisible(typeof(Where<enableNotification, Equal<boolTrue>>))]
        public virtual string WebhookToken { get; set; }
		public abstract class webhookToken : PX.Data.BQL.BqlString.Field<webhookToken> { }
		#endregion

		#region Url
		[PXDBString(200, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Url")]
        [PXUIVisible(typeof(Where<enableNotification, Equal<boolTrue>>))]
        public virtual string Url { get; set; }
		public abstract class url : PX.Data.BQL.BqlString.Field<url> { }
		#endregion

		#region CreatedByID
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
		#endregion

		#region CreatedByScreenID
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID { get; set; }
		public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
		#endregion

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
		#endregion

		#region LastModifiedByID
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
		#endregion

		#region LastModifiedByScreenID
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
		#endregion

		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
		#endregion

		#region Tstamp
		[PXDBTimestamp()]
		[PXUIField(DisplayName = "Tstamp")]
		public virtual byte[] Tstamp { get; set; }
		public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
		#endregion
	}
}