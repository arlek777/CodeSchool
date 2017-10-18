import { Component } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";

export class TaskTestResult {
    tips:string[] = [];
    status: string;
    get isSucceeded() {
        return status === "success";
    }
}

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage {
    lesson: LessonViewModel = new LessonViewModel();
    result: TaskTestResult = new TaskTestResult();

    constructor(private backendService: BackendService) {
        this.backendService.getLesson().then(model => {
            this.lesson = model;
        });
    }

    testTask() {
        var that = this;
        (<any>window).resultsReceived = function (result) {
            that.result.status = result.status;
            if (that.result.isSucceeded) return;

            var tips = [];
            result.failedExpectations.forEach(exp => {
                if (exp.message.indexOf("never called") !== -1) {
                    tips.push("You should call alert.");
                }
            });

            that.result.tips = tips;
        }

        var iframe = <any>document.getElementById("testCode");

        var code = iframe.contentDocument.createElement("script");
        code.innerHTML = this.lesson.code;
        var testEl = iframe.contentDocument.createElement("script");
        testEl.innerHTML = "window.runJasmine();";

        iframe.contentDocument.body.appendChild(code);
        iframe.contentDocument.body.appendChild(testEl);
    }
}
