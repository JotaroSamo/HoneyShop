import { Component } from '@angular/core';
import { AuthService } from '../../data/service/auth.service';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports:[MatToolbarModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  constructor(private authService: AuthService) { }

  // Метод для проверки, аутентифицирован ли пользователь
  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  logout()
  {
    return this.authService.logout();
  }
}
