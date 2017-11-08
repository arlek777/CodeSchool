import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { PopupService } from "./popup.service";
import { Router } from "@angular/router";
import { Response } from '@angular/http';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private injector: Injector) { }

    handleError(error) {
        var response = <Response>error.rejection;
        var popupService = this.injector.get(PopupService);
        var router = this.injector.get(Router);

        if (response.status === 400) {
            popupService.newValidationError(response.text());
        } else {
            var errorInfo = response.json();
            console.log("Server Error", errorInfo);
            popupService.newServerError(errorInfo.message);
        }
    }
}