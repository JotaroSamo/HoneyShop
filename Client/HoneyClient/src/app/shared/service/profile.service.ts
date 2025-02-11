import { Injectable } from '@angular/core';
import { environment } from '../../constant/enviroment';
import { HttpClient } from '@angular/common/http';
import { UserItem } from '../../data/interface/user/User';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private apiUrl = environment.apiUrl + "Profile";
 
   constructor(private http: HttpClient) {}

   getProfile() : Observable<UserItem>
   {
         return this.http.get<UserItem>(`${this.apiUrl}`);
   }
 
}
