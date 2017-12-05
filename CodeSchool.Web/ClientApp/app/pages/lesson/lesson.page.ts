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
import Usermessages = require("../../user-messages");
import UserMessages = Usermessages.UserMessages;

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    userLesson: UserLessonModel = new UserLessonModel();
    userLessons = [];

    sanitizedLessonText: SafeHtml = null;
    sanitizedLessonTaskText: SafeHtml = null;
    result: LessonTestResult;
    currentIndex = -1;
    userChapterId: number;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router,
        private popupService: PopupService,
        private sanitizer: DomSanitizer) {
    }

    ngOnInit(): void {
        this.userChapterId = parseInt(this.route.snapshot.params["userChapterId"]);
        var userLessonId = parseInt(this.route.snapshot.params["userLessonId"]);

        this.backendService.canOpenLesson(UserHelper.getUserId(), this.userChapterId, userLessonId).then(canOpen => {
            if (canOpen) {
                this._init(userLessonId);
            } else {
                this.router.navigate(['/chapters']);
                this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
            }
        });
    }

    getNextLesson() {
        if (!this.userLesson.isPassed) return;

        var nextIndex = ++this.currentIndex;
        if (nextIndex == this.userLessons.length) {
            this.router.navigate(['/chapters']);
            this.popupService.newSuccessMessage(UserMessages.chapterDone);
            return;
        }

        this._init(this.userLessons[nextIndex].id);
        this.router.navigate(['/lesson', this.userChapterId, this.userLessons[nextIndex].id]);
    }

    getPreviousLesson() {
        if (this.currentIndex == 0) return;
        var prevIndex = --this.currentIndex;

        this._init(this.userLessons[prevIndex].id);
    }

    onTestResultsReceived(result: LessonTestResult) {
        this.result = result;
        this.userLesson.isPassed = result.isSucceeded;
        this.userLessons[this.currentIndex].isPassed = result.isSucceeded;

        this.backendService.updateUserLesson(this.userLesson);
    }

    checkLesson() {
        if (!this.userLesson.code) return;

        this.lessonTester.checkLesson(this.userLesson.code, this.userLesson.lesson);
    }

    showAnswer() {
        if (confirm(UserMessages.showAnswerConfirm)) {
            this.userLesson.code = this.userLesson.lesson.answerCode;
        }
    }

    getProgressInPercents(): string {
        var passedCount = this.getPassedUserLessonsCount();
        var result = (passedCount * 100) / this.userLessons.length;

        return Math.round(result).toString() + "%";
    }

    getPassedUserLessonsCount(): number {
        return this.userLessons.filter(l => l.isPassed).length;
    }

    private _init(lessonId) {
        this.backendService.getUserLesson(UserHelper.getUserId(), lessonId)
            .then(userLesson => {
                this.userLesson = userLesson;
                this.result = new LessonTestResult();
                this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.userLesson.lesson.text);
                this.sanitizedLessonTaskText = this.sanitizer.bypassSecurityTrustHtml(this.userLesson.lesson.taskText);
            });

        this.backendService.getUserLessons(UserHelper.getUserId(), this.userChapterId).then((userLessons) => {
            this.userLessons = userLessons;
            this.currentIndex = this.userLessons.map(l => l.id).indexOf(lessonId);
        });
    }
}
