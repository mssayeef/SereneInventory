﻿namespace SereneInventory.Inventory.Entities
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), TableName("[dbo].[Transaction]")]
    [DisplayName("Transaction"), InstanceName("Transaction"), TwoLevelCached]
    [ReadPermission("Inventory:Transaction:Read")]
    [InsertPermission("Inventory:Transaction:Insert")]
    [UpdatePermission("Inventory:Transaction:Update")]
    [DeletePermission("Inventory:Transaction:Delete")]
    [LookupScript]
    public sealed class TransactionRow : NRow, IIdRow, INameRow
    {

        [DisplayName("Id"), Identity]
        public Int64? Id { get { return Fields.Id[this]; } set { Fields.Id[this] = value; } }
		public partial class RowFields { public Int64Field Id; }

        [DisplayName("Transaction Type"), NotNull]
        public TransactionType? TransactionType { get { return (TransactionType?)Fields.TransactionType[this]; } set { Fields.TransactionType[this] = (Int32?)value; } }
		public partial class RowFields { public Int32Field TransactionType; }

        [DisplayName("Ref. Transaction"), ForeignKey("[dbo].[Transaction]", "Id"), LeftJoin("jRefTransaction"), TextualField("RefTransactionTransactionNumber")]
        [LookupEditor(typeof(TransactionRow))]
        public Int64? RefTransactionId { get { return Fields.RefTransactionId[this]; } set { Fields.RefTransactionId[this] = value; } }
		public partial class RowFields { public Int64Field RefTransactionId; }

        [DisplayName("Transaction Number"), Size(50), NotNull, QuickSearch]
        public String TransactionNumber { get { return Fields.TransactionNumber[this]; } set { Fields.TransactionNumber[this] = value; } }
		public partial class RowFields { public StringField TransactionNumber; }

        [DisplayName("Transaction Date"), NotNull]
        public DateTime? TransactionDate { get { return Fields.TransactionDate[this]; } set { Fields.TransactionDate[this] = value; } }
		public partial class RowFields { public DateTimeField TransactionDate; }

        [DisplayName("Party Id")]
        public Int64? PartyId { get { return Fields.PartyId[this]; } set { Fields.PartyId[this] = value; } }
		public partial class RowFields { public Int64Field PartyId; }

        #region Foreign Fields



        [DisplayName("Ref Transaction Transaction Type"), Expression("jRefTransaction.[TransactionType]")]
        public Int32? RefTransactionTransactionType { get { return Fields.RefTransactionTransactionType[this]; } set { Fields.RefTransactionTransactionType[this] = value; } }
		public partial class RowFields { public Int32Field RefTransactionTransactionType; }



        [DisplayName("Ref Transaction Ref Transaction Id"), Expression("jRefTransaction.[RefTransactionId]")]
        public Int64? RefTransactionRefTransactionId { get { return Fields.RefTransactionRefTransactionId[this]; } set { Fields.RefTransactionRefTransactionId[this] = value; } }
		public partial class RowFields { public Int64Field RefTransactionRefTransactionId; }



        [DisplayName("Ref Transaction Transaction Number"), Expression("jRefTransaction.[TransactionNumber]")]
        public String RefTransactionTransactionNumber { get { return Fields.RefTransactionTransactionNumber[this]; } set { Fields.RefTransactionTransactionNumber[this] = value; } }
		public partial class RowFields { public StringField RefTransactionTransactionNumber; }



        [DisplayName("Ref Transaction Transaction Date"), Expression("jRefTransaction.[TransactionDate]")]
        public DateTime? RefTransactionTransactionDate { get { return Fields.RefTransactionTransactionDate[this]; } set { Fields.RefTransactionTransactionDate[this] = value; } }
		public partial class RowFields { public DateTimeField RefTransactionTransactionDate; }



        [DisplayName("Ref Transaction Party Id"), Expression("jRefTransaction.[PartyId]")]
        public Int64? RefTransactionPartyId { get { return Fields.RefTransactionPartyId[this]; } set { Fields.RefTransactionPartyId[this] = value; } }
		public partial class RowFields { public Int64Field RefTransactionPartyId; }



        [DisplayName("Ref Transaction Tenant Id"), Expression("jRefTransaction.[TenantId]")]
        public Int64? RefTransactionTenantId { get { return Fields.RefTransactionTenantId[this]; } set { Fields.RefTransactionTenantId[this] = value; } }
		public partial class RowFields { public Int64Field RefTransactionTenantId; }



        #endregion Foreign Fields


        IIdField IIdRow.IdField { get { return Fields.Id; } }

        StringField INameRow.NameField { get { return Fields.TransactionNumber; } }

        public static readonly RowFields Fields = new RowFields().Init();

        public TransactionRow() : base(Fields) { }

		public partial class RowFields : NRowFields { }
    }
}