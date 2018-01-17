import { Directive, ElementRef, Renderer, OnInit, Input } from '@angular/core';

@Directive({
    selector: '[spinner]'
})
export class SpinnerDirective {
    @Input("spinner")
    spinnerPredicate: boolean;

    constructor(private elementRef: ElementRef, private renderer: Renderer) {
        var loading = this.elementRef.nativeElement.querySelector("div[class='loading']");
        loading.classList.remove("hidden");

        this.elementRef.nativeElement.classList.addClass("hidden");
    }
}