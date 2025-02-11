import { Routes } from '@angular/router';
import { LayoutComponent } from './static/layout/layout.component';
import { LoginComponent } from './component/Auth/login/login.component';
import { UserCreateComponent } from './component/User/user-create/user-create.component';

export const routes: Routes = [
    {path: '', component: LayoutComponent, children :
        [
            {path: 'login', component: LoginComponent},
            {path: 'register', component: UserCreateComponent},
        ]
    }
];
