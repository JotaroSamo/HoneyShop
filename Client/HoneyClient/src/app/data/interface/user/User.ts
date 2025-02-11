export interface CreateUser {
    username: string;
    fullName: string;
    phoneNumber: string;
    password: string;
    confirmPassword: string;
  }
import { Role } from "../../enum/Role";
export interface CreateUserAdminRoot {
  username: string;
  password: string;
  confirmPassword: string;
  role: Role;
}
export interface UpdatePasswordUser {
    id: number;
    password: string;
    newPassword: string;
    confirmPassword: string;
  }
  export interface UpdateUser {
    id: number;
    username: string;
    fullName?: string;
    phoneNumber?: string;
  }
  export interface UserItem {
    id: number;
    username: string;
    phoneNumber: string;
    roleName: string;
  }

  import {OrderItem } from "../order/Order";

export interface UserOrder {
  id: number; // Идентификатор пользователя
  username: string; // Полное имя пользователя
  orderItems: OrderItem[]; // Список товаров в заказе
}

import { CartItem } from "../cart/Cart";

export interface UserCart {
  userId: number; // Идентификатор пользователя
  cartItems: CartItem[]; // Список товаров в корзине
}
