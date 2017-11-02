import { Component, OnInit, ViewChild } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { UserLessonModel } from "../../models/userlesson";
import { UserHelper } from "../../utils/helpers";
import { UserChapterModel } from "../../models/userchapter";
import { LessonViewModel } from "../../models/lesson";

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    userLesson: UserLessonModel = new UserLessonModel();
    userLessonIds: number[] = [];
    sanitizedLessonText: SafeHtml = null;
    result: LessonTestResult;
    currentLessonIndex = -1;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private sanitizer: DomSanitizer) {
    }

    ngOnInit(): void {
        var chapterId = this.route.snapshot.params["chapterId"];
        var lessonId = this.route.snapshot.params["lessonId"];
        this.backendService.getUserLesson(UserHelper.getUserId(), lessonId)
            .then(lesson => {
                console.log(lesson);
                this._initLesson(lesson);
            });

        //this.backendService.getUserLessonIds(UserHelper.getUserId(), chapterId).then((ids) => {
        //    this.userLessonIds = ids;
        //    this.currentLessonIndex = this.userLessonIds.indexOf(lessonId);
        //});
    }

    getNextLesson() {
        if (!this.userLesson.isPassed) return;

        var nextIndex = ++this.currentLessonIndex;
        //if (nextIndex == this.userLessonIds.length) {
        //    this.router.navigate(['/chapters']);
        //    this.popupService.newSuccessMessage("Поздравляем с окончанием раздела!");
        //}

        this.backendService.getUserLesson(UserHelper.getUserId(), this.userLessonIds[nextIndex])
            .then(lesson => {
                this._initLesson(lesson);
            });
    }

    getPreviousLesson() {
        if (this.currentLessonIndex == 0) return;
        var prevIndex = --this.currentLessonIndex;

        this.backendService.getUserLesson(UserHelper.getUserId(), this.userLessonIds[prevIndex])
            .then(lesson => {
                this._initLesson(lesson);
            });
    }

    onTestResultsReceived(result) {
        this.result = result;
    }

    checkLesson() {
        var lessonCheckModel = new LessonViewModel(this.userLesson.lesson);
        lessonCheckModel.startCode = this.userLesson.code;
        this.lessonTester.checkLesson(lessonCheckModel);
    }

    private _initLesson(lesson) {
        this.userLesson = lesson;
        this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.userLesson.lesson.text);
    }
}
