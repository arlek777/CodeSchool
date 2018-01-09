import { Component, OnInit, Input } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { UserLessonAnswerScore } from "../../models/userlesson";
import { LessonType } from "../../models/lesson";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { UserLessonBaseComponent } from '../user-lesson-base.component';

@Component({
    selector: "user-lesson-test",
    templateUrl: './user-lesson-test.component.html'
})
export class UserLessonTestComponent extends UserLessonBaseComponent implements OnInit {
    @Input("user-chapter-id")
    userChapterId: number;

    @Input("user-lesson-id")
    userLessonId: number;

    confirmedShowAnswer = false;
    selectedAnswerOptionId: number;
    UserLessonAnswerScore = UserLessonAnswerScore;
    LessonType = LessonType;

    constructor(backendService: BackendService,
        route: ActivatedRoute,
        router: Router,
        popupService: PopupService) {

        super(backendService, route, router, popupService);
    }

    ngOnInit(): void {
        this.loadUserLessonsId(this.userLessonId);
        this.loadUserLesson(this.userLessonId).then(() => {
            this.confirmedShowAnswer = this.userLesson.score != null;
            this.selectedAnswerOptionId = this.userLesson.selectedAnswerOptionId;
        });
    }

    submitAnswerOption() {
        if (!this.selectedAnswerOptionId) return;
        this.userLesson.selectedAnswerOptionId = this.selectedAnswerOptionId;
        this.userLesson.isPassed = true;
    }

    rateLesson(score: UserLessonAnswerScore) {
        this.userLesson.score = score;
        this.userLesson.isPassed = true;
    }

    showAnswer() {
        this.confirmedShowAnswer = confirm(UserMessages.showAnswerConfirm);
    }
}
