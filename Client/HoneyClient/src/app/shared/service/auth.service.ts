import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtModel } from '../../data/interface/auth/JwtModel';
import { LoginUser } from '../../data/interface/auth/LoginUser';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../constant/enviroment';
import { catchError, tap, throwError } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt'



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = environment.apiUrl;


  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
  private static setToken(tokenPair: JwtModel | null): void {
    if (tokenPair) {
      localStorage.setItem('token', tokenPair.token);
      localStorage.setItem('token-exp', tokenPair.expiredAt.toString());
    } else {
      this.removeLocalStorageItems();
    }
  }
  private static removeLocalStorageItems(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('token-exp');
  }
  private static handleError(error: HttpErrorResponse): Observable<never> {
    return throwError(error);
  }
  get token(): string|null {
    const expDate = new Date(localStorage.getItem('token-exp') as string);
    if (new Date() > expDate) {
      this.logout();
      return null;
    }
    return localStorage.getItem('token');
  }
  getUserRole(): string | null {
    const token = this.token;
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    }
    this.logout();
    return null;
  }

  // Метод для получения имени пользователя из токена
  getUserName(): string | null {
    const token = this.token;
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || null;
    }
    this.logout();
    return null;
  }

  // Метод для получения идентификатора пользователя из токена
  getUserId(): string | null {
    const token = this.token;;
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || null;
    }
    this.logout();
    return null;
  }

  get expiredAt(): Date {
    const expDate = localStorage.getItem('token-exp');

    return expDate
      ? new Date(expDate)
      : new Date();
  }
  isAuthenticated(): boolean {
    return this.token != null;
  }
  logout(): void {
    AuthService.setToken(null);
  }
  
  login(user: LoginUser): Observable<JwtModel> {
    return this.http.post<JwtModel>(`${this.apiUrl}Auth/login`, user)
    .pipe(
      tap(token => AuthService.setToken(token)),
      catchError(AuthService.handleError.bind(this))
    );

  }

 }




