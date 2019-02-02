
namespace SereneInventory.Inventory.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Inventory.TransactionDetail")]
    [BasedOnRow(typeof(Entities.TransactionDetailRow), CheckNames = true)]
    public class TransactionDetailColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), Hidden]
        public Int64 Id { get; set; }
        [Width(80)]
        public String RefTransactionTransactionNumber { get; set; }
        public String ProductName { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        [Width(150, Min = 150)]
        public Decimal Amount { get; set; }
        public Decimal? RemainingQuantity { get; set; }
    }
}