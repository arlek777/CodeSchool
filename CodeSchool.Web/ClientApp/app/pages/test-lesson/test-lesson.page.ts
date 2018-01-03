import { Component, OnInit, ViewChild } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { UserLessonModel, UserLessonAnswerScore } from "../../models/userlesson";
import { UserHelper } from "../../utils/helpers";
import { UserChapterModel } from "../../models/userchapter";
import { LessonViewModel, LessonType } from "../../models/lesson";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";

@Component({
    templateUrl: './test-lesson.page.html'
})
export class TestLessonPage implements OnInit {
    userLesson: UserLessonModel = new UserLessonModel();
    userLessonIds = [];

    confirmedShowAnswer = false;
    currentIndex = -1;
    userChapterId: number;
    selectedAnswerOptionId: number;

    UserLessonAnswerScore = UserLessonAnswerScore;
    LessonType = LessonType;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router,
        private popupService: PopupService) {

        this.userLesson.lesson = new LessonViewModel();
    }

    ngOnInit(): void {
        this.userChapterId = parseInt(this.route.snapshot.params["userChapterId"]);
        var userLessonId = parseInt(this.route.snapshot.params["userLessonId"]);
        this._loadUserLessonsId(userLessonId);
        this._loadUserLesson(userLessonId);
    }

    getNextLesson() {
        if (!this.userLesson.isPassed) return;

        this.backendService.updateUserLesson(this.userLesson);

        var nextIndex = ++this.currentIndex;
        if (nextIndex === this.userLessonIds.length) {
            this.router.navigate(['/testchapters']);
            this.popupService.newSuccessMessage(UserMessages.chapterDone);
            return;
        }

        var nextId = this.userLessonIds[nextIndex].id;
        this._loadUserLesson(nextId);
        this.router.navigate(['/testlesson', this.userChapterId, nextId]);
    }

    getPreviousLesson() {
        if (this.currentIndex === 0) return;
        var prevIndex = --this.currentIndex;

        this._loadUserLesson(this.userLessonIds[prevIndex].id);
    }

    submitAnswerOption() {
        if (!this.selectedAnswerOptionId) return;
        this.userLesson.selectedAnswerOptionId = this.selectedAnswerOptionId;
        this.userLesson.isPassed = this.isCorrectAnswer;
    }

    rateLesson(score: UserLessonAnswerScore) {
        this.userLesson.score = score;
        this.userLesson.isPassed = true;
    }

    showAnswer() {
        this.confirmedShowAnswer = confirm(UserMessages.showAnswerConfirm);
    }

    get isCorrectAnswer() {
        var correctAnswerId = this.userLesson.lesson.answerLessonOptions.find(a => a.isCorrect).id;
        return this.userLesson.selectedAnswerOptionId  === correctAnswerId;
    }

    private _loadUserLessonsId(userLessonId) {
        this.backendService.getUserLessonIds(UserHelper.getUserId(), this.userChapterId).then((userLessons) => {
            this.userLessonIds = userLessons;
            this.currentIndex = 0;
        });
    }

    private _loadUserLesson(userLessonId) {
        this.backendService.getUserLesson(UserHelper.getUserId(), userLessonId)
            .then(userLesson => {
                this.userLesson = userLesson;
                this.confirmedShowAnswer = false;
            });
    }
}
