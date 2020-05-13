import { Component, ViewContainerRef, OnInit } from '@angular/core';
import { ToastsManager } from 'ng2-toastr';
import { PopupService } from "../../services/popup.service";
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(private popupService: PopupService,
        private toastr: ToastsManager,
        private viewRef: ViewContainerRef,
        private sanitizer: DomSanitizer) {

        this.toastr.setRootViewContainerRef(viewRef);
    }

    ngOnInit() {
        this.popupService.successMessages$.subscribe((text: string) => {
            setTimeout(() => {
                this.toastr.success(text);
            });
        });

        this.popupService.validationErrors$.subscribe((text: string) => {
            setTimeout(() => {
                this.toastr.error(text);
            });
        });

        this.popupService.serverErrors$.subscribe((text: string) => {
            setTimeout(() => {
                this.toastr.error(text);
            });
        });
    }
}
