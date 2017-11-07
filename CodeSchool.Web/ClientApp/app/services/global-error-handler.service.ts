import { Response } from '@angular/http';
import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { PopupService } from "./popup.service";
import { Router } from "@angular/router";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private injector: Injector) { }

    handleError(error) {
        var response = <Response>error.rejection;
        console.log(response);
        if (response) {
            var popupService = this.injector.get(PopupService);
            var router = this.injector.get(Router);

            // bad request
            if (response.status === 400) {
                popupService.newValidationError(response.text());
            } else {
                console.log(error);
                router.navigate['/home'];
            }
        } else {
            console.log(error);
            throw error;
        }
    }
}