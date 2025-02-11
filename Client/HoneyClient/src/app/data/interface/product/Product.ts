export interface ProductItem {
    id: number; // Идентификатор продукта
    name: string; // Название продукта
    description: string; // Описание продукта
    price: number; // Цена продукта
    status: string; // Статус продукта (например, Available, Out of Stock)
    files: number[]; // Список идентификаторов файлов
  }
  