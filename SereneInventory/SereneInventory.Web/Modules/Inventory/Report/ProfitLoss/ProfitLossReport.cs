namespace SereneInventory.Inventory
{
    using _Ext;
    using SereneInventory.Inventory.Entities;
    using SereneInventory.Setup.Entities;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Reporting;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    [Report("Inventory.ProfitLossReport")]
    [ReportDesign(MVC.Views.Inventory.Report.ProfitLoss.ProfitLossReport)]
    public class ProfitLossReport : ListReportBase, IReport
    {
        public new ProfitLossReportRequest Request { get; set; }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<TransactionRow>())
            {
                return new ProfitLossReportModel(connection, Request);
            }
        }
    }

    public class ProfitLossReportModel : ListReportModelBase
    {
        public new ProfitLossReportRequest Request { get; set; }

        public List<ItemModel> Items { get; set; } = new List<ItemModel>();

        public ProfitLossReportModel(IDbConnection connection, ProfitLossReportRequest request)
        {
            Request = request;
            var tdfld = TransactionDetailRow.Fields;
            var pfld = ProductRow.Fields;

            var openingQuery = new SqlQuery().From(tdfld)
                .Select(tdfld.ProductName, nameof(ItemModel.ItemName))
                .Select(tdfld.TransactionTransactionType, nameof(ItemModel.TransactionType))
                .Select(Sql.Sum(tdfld.Quantity.Expression), nameof(ItemModel.OpeningQuantity))
                //.Select(Sql.Avg(tdfld.UnitPrice.Expression), nameof(ItemModel.AverageopeningPrice))
                //.Select(Sql.Sum(tdfld.Amount.Expression), nameof(ItemModel.OpeningAmount))
                .Select(Sql.Avg(tdfld.UnitPrice.Expression), nameof(ItemModel.AverageOpeningPrice))
                //.Where(tdfld.TransactionTransactionType == (int)TransactionType.openingInvoice)
                .Where(tdfld.TransactionTransactionDate < request.DateFrom)
                .GroupBy(tdfld.ProductName)
                .GroupBy(tdfld.TransactionTransactionType)
                ;

            //if (!string.IsNullOrWhiteSpace(Request.PurchaseInvoiceNumber))
            //{
            //    openingQuery.Where(tdfld.TransactionTransactionNumber == Request.PurchaseInvoiceNumber);
            //}
            //if (Request.PurchasedFromPartyId > 0)
            //{
            //    openingQuery.Where(tdfld.TransactionPartyId == Request.PurchasedFromPartyId.Value);
            //}

            var openingItems = connection.Query<ItemModel>(openingQuery);

            var purcahseQuery = new SqlQuery().From(tdfld)
                .Select(tdfld.ProductName, nameof(ItemModel.ItemName))
                .Select(Sql.Sum(tdfld.Quantity.Expression), nameof(ItemModel.PurchaseQuantity))
                //.Select(Sql.Avg(tdfld.UnitPrice.Expression), nameof(ItemModel.AveragePurchasePrice))
                .Select(Sql.Sum(tdfld.Amount.Expression), nameof(ItemModel.PurchaseAmount))
                .Where(tdfld.TransactionTransactionType == (int)TransactionType.PurchaseInvoice)
                .Where(tdfld.TransactionTransactionDate >= request.DateFrom)
                .Where(tdfld.TransactionTransactionDate <= request.DateTo)
                .GroupBy(tdfld.ProductName)
                ;

            if (!string.IsNullOrWhiteSpace(Request.PurchaseInvoiceNumber))
            {
                purcahseQuery.Where(tdfld.TransactionTransactionNumber == Request.PurchaseInvoiceNumber);
            }
            if (Request.PurchasedFromPartyId > 0)
            {
                purcahseQuery.Where(tdfld.TransactionPartyId == Request.PurchasedFromPartyId.Value);
            }

            var purchaseItems = connection.Query<ItemModel>(purcahseQuery);

            var salesQuery = new SqlQuery().From(tdfld)
                .Select(tdfld.ProductName, nameof(ItemModel.ItemName))
                .Select(Sql.Sum(tdfld.Quantity.Expression), nameof(ItemModel.SalesQuantity))
                //.Select(Sql.Avg(tdfld.UnitPrice.Expression), nameof(ItemModel.AverageSalesPrice))
                .Select(Sql.Sum(tdfld.Amount.Expression), nameof(ItemModel.SalesAmount))
                .Where(tdfld.TransactionTransactionType == (int)TransactionType.SalesInvoice)
                .Where(tdfld.TransactionTransactionDate >= request.DateFrom)
                .Where(tdfld.TransactionTransactionDate <= request.DateTo)
                .GroupBy(tdfld.ProductName)
                ;

            var salesItems = connection.Query<ItemModel>(salesQuery);


            var productQuery = new SqlQuery().From(pfld)
                .Select(pfld.Name, nameof(ItemModel.ItemName));

            Items = connection.Query<ItemModel>(productQuery).ToList();

            foreach (var item in Items)
            {
                var openingItem = openingItems.Where(f => f.ItemName == item.ItemName);

                if (openingItem?.Count() > 0)
                {
                    item.OpeningQuantity = openingItem.Sum(s => s.TransactionType == TransactionType.PurchaseInvoice ? s.OpeningQuantity : -s.OpeningQuantity);

                    var openingPurhaseOnly = openingItem.Where(s => s.TransactionType == TransactionType.PurchaseInvoice);

                    if (openingPurhaseOnly?.Count() > 0)
                    {
                        item.AverageOpeningPrice = openingPurhaseOnly.Average(s => s.AverageOpeningPrice);
                    }
                }

                var purchaseItem = purchaseItems.Where(f => f.ItemName == item.ItemName);

                if (purchaseItem?.Count() > 0)
                {
                    item.PurchaseQuantity = purchaseItem.Sum(s => s.PurchaseQuantity);
                    item.PurchaseAmount = purchaseItem.Sum(s => s.PurchaseAmount);
                }

                var salesItem = salesItems.Where(f => f.ItemName == item.ItemName);

                if (salesItem?.Count() > 0)
                {
                    item.SalesQuantity = salesItem.Sum(s => s.SalesQuantity);
                    item.SalesAmount = salesItem.Sum(s => s.SalesAmount);
                }
            }

            Items = Items.FindAll(f => f.OpeningQuantity > 0 || f.PurchaseQuantity > 0 || f.SalesQuantity > 0);
        }

    }

    public class ItemModel
    {
        public string ItemName { get; set; }
        public TransactionType TransactionType { get; set; }

        public decimal OpeningQuantity { get; set; }
        public decimal AverageOpeningPrice { get; set; }
        public decimal OpeningAmount => OpeningQuantity * AverageOpeningPrice;

        public decimal PurchaseQuantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal AveragePurchasePrice => PurchaseQuantity > 0 ? PurchaseAmount / PurchaseQuantity : 0;

        public decimal SalesQuantity { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal AverageSalesPrice => SalesQuantity > 0 ? SalesAmount / SalesQuantity : 0;

        public decimal OpeningPlusPurchaseQuantity => OpeningQuantity + PurchaseQuantity;
        public decimal OpeningPlusPurchaseAmount => OpeningAmount + PurchaseAmount;
        public decimal AveragePrice => OpeningPlusPurchaseQuantity > 0 ? OpeningPlusPurchaseAmount / OpeningPlusPurchaseQuantity : 0;

        public decimal RemainigQuantity => OpeningQuantity + PurchaseQuantity - SalesQuantity;
        public decimal BalanceAmount => RemainigQuantity * AveragePrice;

        public decimal AverageProfit => AverageSalesPrice == 0 ? 0 : AverageSalesPrice - AveragePrice;
        public decimal TotalProfit => AverageProfit * SalesQuantity;
    }

}