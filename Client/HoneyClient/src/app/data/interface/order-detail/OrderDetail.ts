import { ProductItem } from "../product/Product"; 

export interface OrderDetailsItem {
  id: number; // Идентификатор детали заказа
  product: ProductItem; // Связь с продуктом
  quantity: number; // Количество продукта
}
