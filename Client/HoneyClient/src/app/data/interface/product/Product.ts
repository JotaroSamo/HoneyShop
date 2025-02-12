import { ProductStatusEnum } from "../../enum/ProductStatusEnum";

export interface ProductItem {
    id: number; // Идентификатор продукта
    name: string; // Название продукта
    description: string; // Описание продукта
    price: number; // Цена продукта
    status: string; // Статус продукта (например, Available, Out of Stock)
    files: number[]; // Список идентификаторов файлов
  }
  export interface CreateProduct {
    name: string;
    description?: string;
    price: number;
    status: ProductStatusEnum;
    files: number[];
  }
  
  export interface UpdateProduct {
    id: number;
    name: string;
    description?: string;
    price: number;
    statusId: number;
    files: number[];
  }