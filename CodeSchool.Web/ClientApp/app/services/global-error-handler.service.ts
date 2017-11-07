import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { PopupService } from "./popup.service";
import { Router } from "@angular/router";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private injector: Injector) { }

    handleError(error) {
        var response = <Response>error.rejection;
        var popupService = this.injector.get(PopupService);
        var router = this.injector.get(Router);

        // bad request
        if (response.status === 400) {
            popupService.newValidationError("");
        } else {
            console.log(response.body);
            popupService.newServerError("");   
        }
    }
}