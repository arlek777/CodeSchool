import { Component } from '@angular/core';
import { AuthService } from "../../services/auth.service";

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    menuCollapsed = true;

    constructor(private authService: AuthService) {
        
    }

    get isAdmin(): boolean {
        return this.authService.isAdmin;
    }

    get isLoggedIn(): boolean {
        return this.authService.isLoggedIn;
    }

    get userName(): string {
        return this.authService.user.userName;
    }

    logout() {
        this.authService.logout();
    }
}

