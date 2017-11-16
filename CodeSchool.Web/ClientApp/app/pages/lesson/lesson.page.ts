import { Component, OnInit, ViewChild } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { UserLessonModel } from "../../models/userlesson";
import { UserHelper } from "../../utils/helpers";
import { UserChapterModel } from "../../models/userchapter";
import { LessonViewModel } from "../../models/lesson";
import { PopupService } from "../../services/popup.service";

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    userLesson: UserLessonModel = new UserLessonModel();
    userLessonIds: number[] = [];

    sanitizedLessonText: SafeHtml = null;
    sanitizedLessonTaskText: SafeHtml = null;
    result: LessonTestResult;
    currentIndex = -1;
    chapterId: number;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router,
        private popupService: PopupService,
        private sanitizer: DomSanitizer) {
    }

    ngOnInit(): void {
        this.chapterId = parseInt(this.route.snapshot.params["chapterId"]);
        var lessonId = parseInt(this.route.snapshot.params["lessonId"]);

        this._init(lessonId);

        this.backendService.getUserLessonIds(UserHelper.getUserId(), this.chapterId).then((ids) => {
            this.userLessonIds = ids;
            this.currentIndex = this.userLessonIds.indexOf(lessonId);
        });
    }

    getNextLesson() {
        if (!this.userLesson.isPassed) return;

        var nextIndex = ++this.currentIndex;
        if (nextIndex == this.userLessonIds.length) {
            this.router.navigate(['/chapters']);
            this.popupService.newSuccessMessage("Поздравляем с окончанием раздела!");
            return;
        }

        this._init(this.userLessonIds[nextIndex]);
        this.router.navigate(['/lesson', this.chapterId, this.userLessonIds[nextIndex]]);
    }

    getPreviousLesson() {
        if (this.currentIndex == 0) return;
        var prevIndex = --this.currentIndex;

        this._init(this.userLessonIds[prevIndex]);
    }

    onTestResultsReceived(result: LessonTestResult) {
        this.result = result;
        this.userLesson.isPassed = result.isSucceeded;

        this.backendService.updateUserLesson(this.userLesson);
    }

    checkLesson() {
        if (!this.userLesson.code) return;

        var lessonCheckModel = new LessonViewModel(this.userLesson.lesson);
        lessonCheckModel.startCode = this.userLesson.code;
        this.lessonTester.checkLesson(lessonCheckModel);
    }

    private _init(lessonId) {
        this.backendService.getUserLesson(UserHelper.getUserId(), lessonId)
            .then(userLesson => {
                this.userLesson = userLesson;
                this.result = new LessonTestResult();
                this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.userLesson.lesson.text);
                this.sanitizedLessonTaskText = this.sanitizer.bypassSecurityTrustHtml(this.userLesson.lesson.taskText);
            });
    }
}
