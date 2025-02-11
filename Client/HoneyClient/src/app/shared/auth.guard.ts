import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from './service/auth.service'; 
@Injectable({
    providedIn: 'root'
  })
  export class AuthGuard implements CanActivate {
    constructor(private router: Router,
        private authService: AuthService)
    {}
    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
       if(this.authService.isAuthenticated())
       {
                const roles = next.data['roles'] as Array<string>;
                const isAuthenticated = this.checkRoles(roles)
        
                if (!isAuthenticated) {
                this.router.navigate(['login']).then();
                return false;
                }
  
            return true;
       }
       this.router.navigate(['login']).then();
       return false;
    }
   
  checkRoles(acceptedRoles: string[]): boolean {
    const userRole = this.authService.getUserRole();
    return userRole ? acceptedRoles.includes(userRole) : false;
  }
  
}
