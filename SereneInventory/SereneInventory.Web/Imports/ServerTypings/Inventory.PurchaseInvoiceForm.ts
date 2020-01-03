namespace SereneInventory.Inventory {
    export interface PurchaseInvoiceForm {
        TransactionType: Serenity.EnumEditor;
        TransactionNumber: Serenity.StringEditor;
        TransactionDate: Serenity.DateEditor;
        PartyId: Serenity.LookupEditor;
        TenantId: Serenity.StringEditor;
        TransactionDetailRows: TransactionDetailGridEditor;
        TotalAmount: Serenity.DecimalEditor;
        TransactionExpenseRows: TransactionExpenseGridEditor;
        ExpensePerPiece: Serenity.DecimalEditor;
        TotalExpense: Serenity.DecimalEditor;
    }

    export class PurchaseInvoiceForm extends Serenity.PrefixedContext {
        static formKey = 'Inventory.PurchaseInvoice';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseInvoiceForm.init)  {
                PurchaseInvoiceForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateEditor;
                var w3 = s.LookupEditor;
                var w4 = TransactionDetailGridEditor;
                var w5 = s.DecimalEditor;
                var w6 = TransactionExpenseGridEditor;

                Q.initFormType(PurchaseInvoiceForm, [
                    'TransactionType', w0,
                    'TransactionNumber', w1,
                    'TransactionDate', w2,
                    'PartyId', w3,
                    'TenantId', w1,
                    'TransactionDetailRows', w4,
                    'TotalAmount', w5,
                    'TransactionExpenseRows', w6,
                    'ExpensePerPiece', w5,
                    'TotalExpense', w5
                ]);
            }
        }
    }
}

