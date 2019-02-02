namespace SereneInventory {
    export enum ExpenseType {
        Transportation = 10,
        Media = 20,
        Delivery = 30,
        Labour = 40,
        QC = 50,
        Misc = 100
    }
    Serenity.Decorators.registerEnumType(ExpenseType, 'SereneInventory.ExpenseType');
}

