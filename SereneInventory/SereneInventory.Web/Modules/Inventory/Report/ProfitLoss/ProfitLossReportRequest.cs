
namespace SereneInventory.Inventory
{
    using _Ext;
    using SereneInventory.Inventory.Entities;
    using SereneInventory.Setup.Entities;
    using Serenity.ComponentModel;
    using Serenity.Services;
    using System;
    using System.ComponentModel;

    [FormScript("Inventory.ProfitLossReportRequestForm")]
    public class ProfitLossReportRequestForm
    {
        [HalfWidth(UntilNext = true)]
        [DisplayName("Sales Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Sales Date To"), Required]
        public DateTime DateTo { get; set; }
        [DisplayName("Purchase Invoice Number")]
        public String PurchaseInvoiceNumber { get; set; }
        [DisplayName("Purchased from Party")]
        [LookupEditor(typeof(SuppliesLookup))]
        public Int64 PurchasedFromPartyId { get; set; }
    }

    public class ProfitLossReportRequest : ServiceRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public String PurchaseInvoiceNumber { get; set; }
        public Int64? PurchasedFromPartyId { get; set; }
        public String PurchasedFromPartyName { get; set; }

    }
}