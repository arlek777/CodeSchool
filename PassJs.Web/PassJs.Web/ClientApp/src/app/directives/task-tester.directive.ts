import { Directive, ElementRef, Output, EventEmitter } from '@angular/core';
import { SubTaskViewModel } from "../models/sub-task";
import { SubTaskTestResult } from "../models/tasktestresult";
import { Constants } from "../constants";

@Directive({
    selector: '[task-tester]'
})
export class SubTaskTesterDirective {
    @Output() onTestResultsReceived = new EventEmitter<SubTaskTestResult>();

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

    checkSubTask(code: string, SubTask: SubTaskViewModel) {
        var unitTestsCode = SubTask.unitTestsCode + " window.runJasmine();";

        this.createAndAppendScriptElement(this.decorateCodeWithFunction(code));
        this.createAndAppendScriptElement(SubTask.reporterCode);
        this.createAndAppendScriptElement(unitTestsCode);
    }

    testSubTask(code: string, SubTask: SubTaskViewModel) {
        var unitTestsCode = SubTask.unitTestsCode + " window.runJasmine();";

        this.createAndAppendScriptElement(this.decorateCodeWithFunction(code));
        this.createAndAppendScriptElement(Constants.defaultSubTaskReporter);
        this.createAndAppendScriptElement(unitTestsCode);
    }

    private decorateCodeWithFunction(code: string): string {
        return `
try{
    ${code}
}
catch(e) {}
function runSubTask() { 
try{
    ${code} 
}
catch(e) {}
}`;
    }

    private createAndAppendScriptElement(content) {
        var element = this.iframe.contentDocument.createElement("script");
        element.innerHTML = content;
        this.iframe.contentDocument.body.appendChild(element);
    }
}
