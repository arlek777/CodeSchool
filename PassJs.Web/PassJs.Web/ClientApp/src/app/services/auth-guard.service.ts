import { Injectable } from '@angular/core';
import {
    CanActivate, Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let url: string = state.url;
        return this.checkLogin(url);
    }

    checkLogin(redirectUrl: string): boolean {
        if (this.authService.isLoggedIn) { return true; }

        this.authService.redirectUrl = redirectUrl;

        this.router.navigate(['/login']);
        return false;
    }
}

@Injectable()
export class AdminAuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.isAdmin) return true;

        this.router.navigate(['/home']);
        return false;
    }
}