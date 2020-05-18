import { Component, Input, Output, EventEmitter, Directive } from '@angular/core';

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

    @Input("header-template")
    headerTemplate;

    isTextPreviewMode = false;

    editorOptions = { theme: 'vs-dark', language: 'javascript' };

    textChanged(event) {
        this.textChange.emit(event);
    }
}

