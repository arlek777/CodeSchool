import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from "../../services/auth.service";

@Component({
    templateUrl: './home.page.html'
})
export class HomePage {
    constructor(private authService: AuthService, private router: Router) {
    }

    ngOnInit() {
        if (this.authService.isLoggedIn && this.authService.isAdmin) {
            this.router.navigate(['/admin-chapters']);
        } else if (this.authService.isLoggedIn && !this.authService.isAdmin) {
            this.router.navigate(['/invitation', false]);
        } else {
            this.router.navigate(['/login']);
        }
    }
}
