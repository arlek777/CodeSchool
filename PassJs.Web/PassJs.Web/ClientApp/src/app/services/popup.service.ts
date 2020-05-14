import { Subject } from "rxjs";
import { Injectable } from '@angular/core';

@Injectable()
export class PopupService {
    private validationErrorsSource = new Subject<string>();
    private serverErrorsSource = new Subject<string>();
    private successMessagesSource = new Subject<string>();

    validationErrors$ = this.validationErrorsSource.asObservable();
    serverErrors$ = this.serverErrorsSource.asObservable();
    successMessages$ = this.successMessagesSource.asObservable();

    newValidationError(error: string) {
        this.validationErrorsSource.next(error);
    }

    newServerError(error: string) {
        this.serverErrorsSource.next(error);
    }

    newSuccessMessage(msg: string) {
        this.successMessagesSource.next(msg);
    }
}
