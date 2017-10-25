import { Directive, ElementRef, Output, EventEmitter } from '@angular/core';
import { LessonViewModel } from "../models/lesson";
import { LessonTestResult } from "../models/lessontestresult";

@Directive({
    selector: '[lesson-tester]'
})
export class LessonTesterDirective {
    @Output() onTestResultsReceived = new EventEmitter<any>();

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
        var code = `function lesson() { 
                ${lesson.startCode} 
            }`;

        var unitTestsCode = lesson.unitTestsCode + " window.runJasmine();";

        this.createAndAppendScriptElement(code);
        this.createAndAppendScriptElement(lesson.reporterCode);
        this.createAndAppendScriptElement(unitTestsCode);
    }

    testLesson(lesson: LessonViewModel) {
        var reporterCode = ` 
            var myReporter = {
                specDone: function (result) {
                    window.parent.resultsReceived(result);
                    window.location.reload();
                }
            };

            jasmine.getEnv().clearReporters();
            jasmine.getEnv().addReporter(myReporter);`;

        var code = `function lesson() { 
                ${lesson.startCode} 
            }`;

        var unitTestsCode = lesson.unitTestsCode + " window.runJasmine();";

        this.createAndAppendScriptElement(code);
        this.createAndAppendScriptElement(reporterCode);
        this.createAndAppendScriptElement(unitTestsCode);
    }

    private createAndAppendScriptElement(content) {
        var element = this.iframe.contentDocument.createElement("script");
        element.innerHTML = content;

        this.iframe.contentDocument.body.appendChild(element);
    }
}
