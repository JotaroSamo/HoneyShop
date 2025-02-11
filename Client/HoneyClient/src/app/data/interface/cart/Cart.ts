export interface CartItem {
    id: number;
    productId: number; // Идентификатор продукта
    productName: string; // Название продукта
    fileIds: number[]; // Идентификаторы файлов
    price: number; // Цена продукта
    quantity: number; // Количество товара в корзине
  }
  