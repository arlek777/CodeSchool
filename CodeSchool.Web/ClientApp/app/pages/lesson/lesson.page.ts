import { Component, OnInit, ViewChild } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { UserLessonModel } from "../../models/userlesson";
import { UserHelper } from "../../utils/helpers";

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    userLesson: UserLessonModel = new UserLessonModel();
    userLessonIds: UserLessonModel
    sanitizedLessonText: SafeHtml = null;
    result: LessonTestResult;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private sanitizer: DomSanitizer) {
    }

    ngOnInit(): void {
        this.backendService.getUserLesson(UserHelper.getUserId(), this.route.snapshot.params["id"]).then(lesson => {
            this._initLesson(lesson);
        });
    }

    getNextLesson() {
        
    }

    onTestResultsReceived(result) {
        this.result = result;
    }

    checkLesson() {
        this.lessonTester.checkLesson(this.userLesson);
    }

    private _initLesson(lesson) {
        this.userLesson = lesson;
        this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.userLesson.lesson.text);
    }
}
