import { Injectable, ErrorHandler, Injector } from '@angular/core';
import { PopupService } from "./services/popup.service";


@Injectable()
export class AppErrorHandler extends ErrorHandler {

    constructor( private popupService: PopupService, private injector: Injector) {
        super();
    }


    handleError(error: any) {
         if (this.popupService == null) {
            this.popupService = this.injector.get(PopupService);
         }

        this.popupService.newValidationError(error.message);
        console.log("ERROR", error);  

        super.handleError(error);
    }
}
