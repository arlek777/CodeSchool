import { Component, Input, Output, EventEmitter, ViewChild, Directive, ContentChild, TemplateRef, AfterContentChecked } from '@angular/core';

@Directive({
    selector: '[headerTemplate]'
})
export class HeaderTemplateDirective {}

@Component({
    selector: 'editor',
    templateUrl: './editor.component.html'
})
export class EditorComponent {
    @Input("css")
    cssClasses = {};

    @Input("header-text")
    headerText: string;

    @Input()
    text: string;

    @Input()
    mode: string;

    @Output()
    textChange = new EventEmitter<string>();

    @ContentChild(HeaderTemplateDirective, { read: TemplateRef }) headerTemplate;

    isTextPreviewMode = false;

    textChanged(event) {
        this.textChange.emit(event);
    }
}

