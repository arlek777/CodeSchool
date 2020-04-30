import { Injectable, Inject } from '@angular/core';
import { BackendService } from "./backend.service";
import { LoginViewModel } from "../models/auth/login";
import { RegistrationViewModel } from "../models/auth/registration";
import { User } from "../models/user";
import { JWTTokens } from "../models/auth/jwttokens";
import { Constants } from "../constants";
import { Router } from "@angular/router";

export let authStorage: any = localStorage;

@Injectable()
export class AuthService {
    private _accessToken: string;
    private _user: User;

    constructor(private backendService: BackendService, private router: Router) {
    }

    redirectUrl: string;

    get user(): User {
        if (this._user) return this._user;
        var localStorageUser = authStorage.getItem(Constants.currentUserKey);
        if (localStorageUser) {
            this._user = JSON.parse(localStorageUser);
            return this._user;
        }
        return null;
    }

    get accessToken(): string {
        if (this._accessToken) return this._accessToken;
        var localStorageToken = authStorage.getItem(Constants.accessTokenKey);
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

    loginByToken(token: string): Promise<void> {
        return this.backendService.loginByToken(token).then((tokens: JWTTokens) => {
            this.setTokensAndUserToLocalStorage(tokens, true);
        });
    }

    register(model: RegistrationViewModel): Promise<void> {
        return this.backendService.register(model).then((tokens: JWTTokens) => {
            this.setTokensAndUserToLocalStorage(tokens);
        });
    }

    logout(): void {
        authStorage.removeItem(Constants.accessTokenKey);
        authStorage.removeItem(Constants.currentUserKey);
        this._user = null;

        this.router.navigate(['/login']);
    }

    private setTokensAndUserToLocalStorage(tokens: JWTTokens, sessionExpiration = false) {
        var base64UserClaims = tokens.idToken.split(".")[1];
        var userClaims = JSON.parse(atob(base64UserClaims));

        this._user = new User({
            userName: userClaims.username,
            email: userClaims.email,
            isAdmin: userClaims.isAdmin
        });

        authStorage = sessionExpiration ? sessionStorage : localStorage;

        authStorage.setItem(Constants.accessTokenKey, tokens.accessToken);
        authStorage.setItem(Constants.currentUserKey, JSON.stringify(this._user));
    }
}