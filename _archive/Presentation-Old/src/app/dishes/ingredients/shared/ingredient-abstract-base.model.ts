export class IngredientAbstractBase {
  name: string;
  pricePerUnit: number;
  quantity: number;
  dishId: number; // probably required, but needs to be tested.
}
