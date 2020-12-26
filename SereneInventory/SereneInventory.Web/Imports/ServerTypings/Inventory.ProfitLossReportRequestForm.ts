namespace SereneInventory.Inventory {
    export interface ProfitLossReportRequestForm {
        DateFrom: Serenity.DateEditor;
        DateTo: Serenity.DateEditor;
        PurchaseInvoiceNumber: Serenity.StringEditor;
        PurchasedFromPartyId: Serenity.LookupEditor;
    }

    export class ProfitLossReportRequestForm extends Serenity.PrefixedContext {
        static formKey = 'Inventory.ProfitLossReportRequestForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProfitLossReportRequestForm.init)  {
                ProfitLossReportRequestForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = s.StringEditor;
                var w2 = s.LookupEditor;

                Q.initFormType(ProfitLossReportRequestForm, [
                    'DateFrom', w0,
                    'DateTo', w0,
                    'PurchaseInvoiceNumber', w1,
                    'PurchasedFromPartyId', w2
                ]);
            }
        }
    }
}

