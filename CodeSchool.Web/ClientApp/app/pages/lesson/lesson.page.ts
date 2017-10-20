import { Component, OnInit } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();
    result: LessonTestResult = new LessonTestResult();

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.backendService.getLesson(this.route.snapshot.params["id"]).then(lesson => {
            this.lesson = lesson;
        });
    }

    getNextLesson() {
        var chapterId = this.route.snapshot.params["chapterId"];
        var lessonId = this.route.snapshot.params["id"];
        this.backendService.getNextLesson(chapterId, lessonId).then(lesson => {
            this.lesson = lesson;
        });
    }

    testLesson() {
        var that = this;
        (<any>window).resultsReceived = function (result) {
            console.log(result);
            that.result = result;
        }

        var iframe = <any>document.getElementById("testLessonIframe");

        var code = iframe.contentDocument.createElement("script");
        code.innerHTML = this.lesson.startCode;

        var reporter = iframe.contentDocument.createElement("script");
        reporter.innerHTML = this.lesson.reporterCode;

        var tests = iframe.contentDocument.createElement("script");
        tests.innerHTML = this.lesson.unitTestsCode;

        iframe.contentDocument.body.appendChild(code);
        iframe.contentDocument.body.appendChild(reporter);
        iframe.contentDocument.body.appendChild(tests);
    }
}
