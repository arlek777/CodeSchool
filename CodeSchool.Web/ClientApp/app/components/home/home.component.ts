import { Component } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    text: string = "";
    options: any = { maxLines: 1000, printMargin: false };

    onChange(code) {
        console.log("new code", code);
    }
}
