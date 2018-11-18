import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { SesionService } from './servicios/sesion.service';

@Injectable()
export class TokenGuard implements CanActivate {

    constructor(private router: Router, private auth: SesionService) {}

    canActivate(route: ActivatedRouteSnapshot): boolean {
        if (!this.auth.isAuthenticated()) {
            this.router.navigate(['/login']);
            return false;
        }
        return true;
    }
}
