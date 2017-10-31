import { Response } from '@angular/http';
import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { PopupService } from "./popup.service";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private injector: Injector) { }

    handleError(error) {
        var response = <Response>error.rejection;
        if (response) {
            var popupService = this.injector.get(PopupService);

            // bad request
            if (response.status === 400) {
                popupService.newValidationError(response.text());
            } else {
                console.log(error);
                throw error;
            }
        } else {
            throw error;
        }
    }
}