import { Component, ViewContainerRef, OnInit } from '@angular/core';
import {ToastaService, ToastaConfig, ToastOptions, ToastData} from 'ngx-toasta';
import { PopupService } from "../../services/popup.service";
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(private popupService: PopupService,
        private toastaService:ToastaService,
        private toastaConfig: ToastaConfig,
        private sanitizer: DomSanitizer) {

      this.toastaConfig.theme = 'material';
    }

    private toastOptions:ToastOptions = {
      title: "",
      showClose: true,
      timeout: 5000
    };

    ngOnInit() {
        this.popupService.successMessages$.subscribe((text: string) => {
            setTimeout(() => {
              this.toastaService.success({msg: text, title: "Сообщение" , ...this.toastOptions});
            });
        });

        this.popupService.validationErrors$.subscribe((text: string) => {
            setTimeout(() => {
              this.toastaService.error({msg: text, title: "Ошибка" , ...this.toastOptions});
            });
        });

        this.popupService.serverErrors$.subscribe((text: string) => {
            setTimeout(() => {
              this.toastaService.error({msg: "Произошла ошибка сервера", title: "Ошибка" , ...this.toastOptions});
            });
        });
    }
}
