import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateUserAdminRoot, UpdatePasswordUser, UpdateUser, UserItem } from '../interface/user/User';
import { environment } from '../../constant/enviroment';
import { PaginationListModel } from '../interface/PaginationListModel';
import { Role } from '../enum/Role';




@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = environment.apiUrl + "User";

  constructor(private http: HttpClient) {}

  // Создание администратора
  createAdmin(userData: CreateUserAdminRoot): Observable<UserItem> {
    return this.http.post<UserItem>(`${this.apiUrl}/create-admin`, userData);
  }

  // Обновление администратора
  updateAdmin(userData: UpdateUser): Observable<UserItem> {
    return this.http.patch<UserItem>(`${this.apiUrl}/update-admin`, userData);
  }

  // Обновление роли администратора
  updateRoleAdmin(id: number, role: Role): Observable<UserItem> {
    const params = new HttpParams()
      .set('id', id.toString())
      .set('role', role);
    return this.http.patch<UserItem>(`${this.apiUrl}/update-role-admin`, null, {
      params,
    });
  }

  // Смена пароля администратора
  changePasswordAdmin(passwordData: UpdatePasswordUser): Observable<UserItem> {
    return this.http.patch<UserItem>(
      `${this.apiUrl}/change-password-admin`,
      passwordData
    );
  }

  // Удаление пользователя администратором
  deleteUserAdmin(id: number): Observable<boolean> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.delete<boolean>(`${this.apiUrl}/delete-user-admin`, {
      params,
    });
  }

  // Получение списка пользователей с пагинацией
  getUsersAdmin(page: number, pageSize: number): Observable<PaginationListModel<UserItem>> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<PaginationListModel<UserItem>>(`${this.apiUrl}/get-users-admin`, {
      params,
    });
  }

  // Создание пользователя
  createUser(userData: CreateUserAdminRoot): Observable<UserItem> {
    return this.http.post<UserItem>(`${this.apiUrl}/create`, userData);
  }

  // Обновление пользователя
  updateUser(userData: UpdateUser): Observable<UserItem> {
    return this.http.post<UserItem>(`${this.apiUrl}/update`, userData);
  }

  // Смена пароля пользователя
  changePassword(passwordData: UpdatePasswordUser): Observable<UserItem> {
    return this.http.patch<UserItem>(`${this.apiUrl}/change-password`, passwordData);
  }

  // Удаление пользователя
  deleteUser(id: number): Observable<boolean> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.delete<boolean>(`${this.apiUrl}/delete-user`, { params });
  }
}