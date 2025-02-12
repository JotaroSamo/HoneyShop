import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../constant/enviroment';
import { CreateProduct, UpdateProduct, ProductItem } from '../../data/interface/product/Product';
import { ProductStatusEnum } from '../../data/enum/ProductStatusEnum';
import { PaginationListModel } from '../../data/interface/PaginationListModel';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = environment.apiUrl + 'Product';


  constructor(private http: HttpClient) {}

  // Создание продукта
  createProduct(productData: CreateProduct): Observable<ProductItem> {
    return this.http.post<ProductItem>(`${this.apiUrl}/create`, productData);
  }

  // Получение страницы продуктов
  getProducts(page: number, pageSize: number): Observable<PaginationListModel<ProductItem>> {
    let params = new HttpParams().set('page', page).set('pageSize', pageSize);
    return this.http.get<PaginationListModel<ProductItem>>(`${this.apiUrl}/get-page`, { params });
  }

  // Изменение статуса продукта
  changeProductStatus(id: number, status: ProductStatusEnum): Observable<ProductItem> {
    let params = new HttpParams().set('id', id).set('status', status);
    return this.http.patch<ProductItem>(`${this.apiUrl}/change-status`, {}, { params });
  }

  // Обновление продукта
  updateProduct(productData: UpdateProduct): Observable<ProductItem> {
    return this.http.put<ProductItem>(`${this.apiUrl}/update`, productData);
  }

  // Получение продукта по ID
  getProductById(id: number): Observable<ProductItem> {
    return this.http.get<ProductItem>(`${this.apiUrl}/get-by-id/${id}`);
  }

  // Удаление продукта
  deleteProduct(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/delete/${id}`);
  }
}