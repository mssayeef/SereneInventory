namespace SereneInventory.Inventory {
    export interface ProfitLossReportRequest extends Serenity.ServiceRequest {
        DateFrom?: string;
        DateTo?: string;
        PurchaseInvoiceNumber?: string;
        PurchasedFromPartyId?: number;
        PurchasedFromPartyName?: string;
    }
}

