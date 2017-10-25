import { Component, OnInit, ViewChild } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();
    sanitizedLessonText: SafeHtml = null;
    result: LessonTestResult;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private sanitizer: DomSanitizer) {
    }

    ngOnInit(): void {
        this.backendService.getLesson(this.route.snapshot.params["id"]).then(lesson => {
            this.lesson = lesson;
            this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.lesson.text);
        });
    }

    getNextLesson() {
        var chapterId = this.route.snapshot.params["chapterId"];
        var lessonId = this.route.snapshot.params["id"];
        this.backendService.getNextLesson(chapterId, lessonId).then(lesson => {
            this.lesson = lesson;
            this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.lesson.text);
        });
    }

    onTestResultsReceived(result) {
        this.result = result;
    }

    checkLesson() {
        this.lessonTester.checkLesson(this.lesson);
    }
}
