import { Component } from '@angular/core';

import { AuthService } from '../../shared/service/auth.service';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { Router, RouterLink } from '@angular/router';
import { MatMenu } from '@angular/material/menu';
import { MatMenuModule} from '@angular/material/menu';
import { AdministratorRole, ManagerRole } from '../../constant/modelRole';


@Component({
  selector: 'app-header',
  imports: [MatToolbarModule, RouterLink, MatMenu,  MatMenuModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  

constructor(

  private authService: AuthService
) {}
  // Метод для проверки, аутентифицирован ли пользователь
  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  getUserName():string | null
  {
    return this.authService.getUserName();
  }
  logout()
  {
    return this.authService.logout();
  }
  IsAdmin():boolean
  {
    const roleName = this.authService.getUserRole()
    if(roleName && roleName === AdministratorRole)
    {
        return true;
    }
    return false;
  }
  IsManagerOrAdmin():boolean
  {
    const roleName = this.authService.getUserRole()
    if(roleName && roleName === AdministratorRole || roleName === ManagerRole)
    {
        return true;
    }
    return false;
  }
}
