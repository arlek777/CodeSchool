import { Component } from '@angular/core';
import { LoginViewModel } from "../../models/auth/login";
import { AuthService } from "../../services/auth.service";
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.page.html'
})
export class LoginPage {
    constructor(private authService: AuthService, private router: Router) {
        if (this.authService.isLoggedIn) this.router.navigate(['/home']);
    }

    model = new LoginViewModel();

    onSubmit() {
        this.authService.login(this.model).then(() => {
            this.router.navigate(['/home']);
            if (this.authService.redirectUrl) { this.router.navigate([this.authService.redirectUrl]); }
            else { this.router.navigate(['/home']); }
        });
    }
}
