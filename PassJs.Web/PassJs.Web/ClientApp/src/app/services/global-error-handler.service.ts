import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { PopupService } from "./popup.service";
import { Response } from '@angular/http';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private injector: Injector) { }

    handleError(error) {
        var response = <Response>error.rejection;
        if (!response || !response.status) {
            console.log("Client Error", error);
            return;
        }

        var popupService = this.injector.get(PopupService);

        if (response.status === 400) {
            popupService.newValidationError(response.text());
        } else {
            var errorInfo = response.json();
            if (errorInfo) {
                console.log("Server Error", errorInfo);
                popupService.newServerError(errorInfo.message);
            }
        }
    }
}