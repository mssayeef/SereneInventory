
namespace SereneInventory.Inventory.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Entities;
    using Serenity.Data.Mapping;

    [FormScript("Inventory.PurchaseInvoice")]
    [BasedOnRow(typeof(Entities.TransactionRow), CheckNames = true)]
    public class PurchaseInvoiceForm
    {
        [Hidden]
        public TransactionType TransactionType { get; set; }
        //public Int64 RefTransactionId { get; set; }
        [HalfWidth]
        public String TransactionNumber { get; set; }
        [HalfWidth]
        public DateTime TransactionDate { get; set; }
        [LookupEditor(typeof(SuppliesLookup), InplaceAdd = true, DialogType = "Setup.SupplierDialog")]
        public Int64 PartyId { get; set; }

        [Category("Transaction Details")]
        public List<TransactionDetailRow> TransactionDetailRows { get; set; }


        [DisplayName("Total Purchase"), HalfWidth, OneWay, NotMapped, ReadOnly(true)]
        public Decimal TotalAmount { get; set; }
        [DisplayName("Total Sales"), HalfWidth, ReadOnly(true)]
        public Decimal TotalRefferencedAmount { get; set; }
        [DisplayName("Profit"), HalfWidth, OneWay, NotMapped, ReadOnly(true), DecimalEditor(MinValue = "-999999.99", MaxValue = "999999.99")]
        public Decimal Profit { get; set; }

        [Category("Expenses")]
        [HalfWidth]
        public List<TransactionExpenseRow> TransactionExpenseRows { get; set; }
        [DisplayName("Expense per Piece"), HalfWidth, OneWay, NotMapped, ReadOnly(true)]
        public decimal ExpensePerPiece { get; set; }
        [DisplayName("Total Expense"), HalfWidth, OneWay, NotMapped, ReadOnly(true)]
        public decimal TotalExpense { get; set; }

        //[Category("Related Transactions")]
        //public List<TransactionRow> RelatedTransactionRows { get; set; }

        //public Int64 RemainingQuantity { get; set; }

    }
}