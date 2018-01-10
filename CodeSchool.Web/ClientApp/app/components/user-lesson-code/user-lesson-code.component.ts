import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { UserHelper } from "../../utils/helpers";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from '../../user-messages';
import { UserLessonBaseComponent } from '../user-lesson-base.component';

@Component({
    selector: "user-lesson-code",
    templateUrl: './user-lesson-code.component.html'
})
export class UserLessonCodeComponent extends UserLessonBaseComponent implements OnInit  {
    @Input("user-chapter-id")
    userChapterId: number;

    @Input("user-lesson-id")
    userLessonId: number;

    result: LessonTestResult;
    failedAttempts = 0;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(backendService: BackendService,
        route: ActivatedRoute,
        router: Router,
        popupService: PopupService) {

        super(backendService, route, router, popupService);
    }

    ngOnInit(): void {
        this.newLessonLoaded$.subscribe(() => {
            this.result = new LessonTestResult();
        });

        this.backendService.canOpenLesson(UserHelper.getUserId(), this.userChapterId, this.userLessonId).then(canOpen => {
            if (canOpen) {
                this.loadUserLessonsId(this.userLessonId);
                this.loadUserLesson(this.userLessonId);
            } else {
                this.router.navigate(['/code-chapters']);
                this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
            }
        });
    }

    onTestResultsReceived(result: LessonTestResult) {
        this.result = result;
        this.userLesson.isPassed = result.isSucceeded;
        this.userLessonIds[this.currentIndex].isPassed = result.isSucceeded;
        this.failedAttempts = result.isSucceeded ? 0 : ++this.failedAttempts;

        this.backendService.updateUserLesson(this.userLesson);
    }

    checkLesson() {
        if (!this.userLesson.code) return;

        this.lessonTester.checkLesson(this.userLesson.code, this.userLesson.lesson);
    }

    showAnswer() {
        if (confirm(UserMessages.showAnswerConfirm)) {
            this.userLesson.code = this.userLesson.lesson.answer;
        }
    }

    getProgressInPercents(): string {
        var passedCount = this.getPassedUserLessonsCount();
        var result = (passedCount * 100) / this.userLessonIds.length;

        return Math.round(result).toString() + "%";
    }

    getPassedUserLessonsCount(): number {
        return this.userLessonIds.filter(l => l.isPassed).length;
    }
}
