
namespace SereneInventory.Setup {

    @Serenity.Decorators.registerClass()
    export class SupplierDialog extends _Ext.DialogBase<PartyRow, any> {
        protected getFormKey() { return PartyForm.formKey; }
        protected getIdProperty() { return PartyRow.idProperty; }
        protected getLocalTextPrefix() { return PartyRow.localTextPrefix; }
        protected getNameProperty() { return PartyRow.nameProperty; }
        protected getService() { return PartyService.baseUrl; }

        protected form = new PartyForm(this.idPrefix);

        constructor() {
            super();

            q.hideField(this.form.PartyType);
        }

        protected getSaveEntity() {
            let entity = super.getSaveEntity();
            entity.PartyType = PartyType.Supplier;
            return entity;
        }
    }
}