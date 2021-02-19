
namespace SereneInventory.Setup {
    import fld = ProductRow.Fields;

    @Serenity.Decorators.registerClass()
    export class ProductGrid extends _Ext.GridBase<ProductRow, any> {
        protected getColumnsKey() { return 'Setup.Product'; }
        protected getDialogType() { return ProductDialog; }
        protected getIdProperty() { return ProductRow.idProperty; }
        protected getLocalTextPrefix() { return ProductRow.localTextPrefix; }
        protected getService() { return ProductService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum(fld.QuantityIn),
                    new Slick.Aggregators.Sum(fld.QuantityOut),
                    new Slick.Aggregators.Sum(fld.RemainingQuantity),
                ]
            });
        }

        protected getSlickOptions() {
            var opt = super.getSlickOptions();
            opt.showFooterRow = true;
            return opt;
        }

        protected usePager() {
            return false;
        }
    }
}