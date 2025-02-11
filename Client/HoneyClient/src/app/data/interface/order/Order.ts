import { OrderDetailsItem } from "../order-detail/OrderDetail";

export interface OrderItem {
  id: number;
  totalPrice: number; // Цена продукта
  quantity: number; // Количество товара в корзине
  fullName: string; // Имя пользователя
  number: string; // Номер телефона
  status: string; // Статус заказа (например, Pending, Completed, Cancelled)
  orderDetails: OrderDetailsItem[]; // Связь с деталями заказа
}
