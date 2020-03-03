import { Injectable, Inject } from '@angular/core';
import { BackendService } from "./backend.service";
import { LoginViewModel } from "../models/auth/login";
import { RegistrationViewModel } from "../models/auth/registration";
import { User } from "../models/user";
import { JWTTokens } from "../models/auth/jwttokens";
import { Constants } from "../constants";
import { Router } from "@angular/router";

@Injectable()
export class AuthService {
    private _accessToken: string;
    private _user: User;

    constructor(private backendService: BackendService, private router: Router) {
    }

    redirectUrl: string;

    get user(): User {
        if (this._user) return this._user;
        var localStorageUser = localStorage.getItem(Constants.currentUserKey);
        if (localStorageUser) {
            this._user = JSON.parse(localStorageUser);
            return this._user;
        }
        return null;
    }

    get accessToken(): string {
        if (this._accessToken) return this._accessToken;
        var localStorageToken = localStorage.getItem(Constants.accessTokenKey);
        if (localStorageToken) {
            this._accessToken = localStorageToken;
            return this._accessToken;
        }
        return null;
    }

    get isLoggedIn(): boolean { return this.user !== null && this.accessToken !== null }
    get isAdmin(): boolean { return this.isLoggedIn && this.user.isAdmin; }

    login(model: LoginViewModel): Promise<void> {
        return this.backendService.login(model).then((tokens: JWTTokens) => {
            this.setTokensAndUserToLocalStorage(tokens);
        });
    }

    register(model: RegistrationViewModel): Promise<void> {
        return this.backendService.register(model).then((tokens: JWTTokens) => {
            this.setTokensAndUserToLocalStorage(tokens);
        });
    }

    logout(): void {
        localStorage.removeItem(Constants.accessTokenKey);
        localStorage.removeItem(Constants.currentUserKey);
        localStorage.removeItem(Constants.userIdKey);
        this._user = null;

        this.router.navigate(['/login']);
    }

    private setTokensAndUserToLocalStorage(tokens: JWTTokens) {
        var base64UserClaims = tokens.idToken.split(".")[1];
        var userClaims = JSON.parse(atob(base64UserClaims));

        this._user = new User({
            id: userClaims.id,
            userName: userClaims.username,
            companyId: userClaims.companyId,
            email: userClaims.email,
            isAdmin: userClaims.isAdmin
        });

        localStorage.setItem(Constants.accessTokenKey, tokens.accessToken);
        localStorage.setItem(Constants.currentUserKey, JSON.stringify(this._user));
        localStorage.setItem(Constants.userIdKey, this._user.id);
        localStorage.setItem(Constants.userIdKey, this._user.companyId);
    }
}