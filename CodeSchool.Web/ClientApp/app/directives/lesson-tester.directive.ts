import { Directive, ElementRef, Output, EventEmitter } from '@angular/core';
import { LessonViewModel } from "../models/lesson";
import { LessonTestResult } from "../models/lessontestresult";
import { Constants } from "../constants";

@Directive({
    selector: '[lesson-tester]'
})
export class LessonTesterDirective {
    @Output() onTestResultsReceived = new EventEmitter<LessonTestResult>();

    private iframe: any;

    constructor(private el: ElementRef) {
        this.iframe = this.el.nativeElement;
        this.iframe.style.width = 0;
        this.iframe.style.height = 0;
        this.iframe.style.border = 'none';

        var that = this;
        (<any>window).resultsReceived = function (result) {
            that.onTestResultsReceived.emit(result);
        }
    }

    checkLesson(lesson: LessonViewModel) {
        var unitTestsCode = lesson.unitTestsCode + " window.runJasmine();";

        this.createAndAppendScriptElement(this.decorateCodeWithFunction(lesson.startCode));
        this.createAndAppendScriptElement(lesson.reporterCode);
        this.createAndAppendScriptElement(unitTestsCode);
    }

    testLesson(lesson: LessonViewModel) {
        var unitTestsCode = lesson.unitTestsCode + " window.runJasmine();";

        this.createAndAppendScriptElement(this.decorateCodeWithFunction(lesson.startCode));
        this.createAndAppendScriptElement(Constants.defaultLessonReporter);
        this.createAndAppendScriptElement(unitTestsCode);
    }

    private decorateCodeWithFunction(code: string): string {
        return `
${code}
function runLesson() { 
    ${code} 
}`;
    }

    private createAndAppendScriptElement(content) {
        var element = this.iframe.contentDocument.createElement("script");
        element.innerHTML = content;

        try {
            this.iframe.contentDocument.body.appendChild(element);
        }
        catch (e) {
            this.onTestResultsReceived.emit({
                isSucceeded: false,
                messages: ['¬ведите корректный JavaScript код.'],
                isException: true
            });
        }
    }
}
