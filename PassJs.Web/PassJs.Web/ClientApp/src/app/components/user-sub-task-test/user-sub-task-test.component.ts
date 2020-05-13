import { Component, OnInit, Input } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { UserSubTaskAnswerScore } from "../../models/user-sub-task";
import { SubTaskType } from "../../models/sub-task";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { UserSubTaskBaseComponent } from '../user-sub-task-base.component';

@Component({
    selector: "user-sub-task-test",
    templateUrl: './user-sub-task-test.component.html'
})
export class UserSubTaskTestComponent extends UserSubTaskBaseComponent implements OnInit {
    @Input("user-task-head-id")
    userTaskHeadId: number;

    @Input("user-sub-task-id")
    userSubTaskId: number;

    confirmedShowAnswer = false;
    selectedAnswerOptionId: number;
    UserSubTaskAnswerScore = UserSubTaskAnswerScore;
    SubTaskType = SubTaskType;

    constructor(backendService: BackendService,
        route: ActivatedRoute,
        router: Router,
        popupService: PopupService) {

        super(backendService, null, route, router, popupService);
    }

    ngOnInit(): void {
        this.newSubTaskLoaded$.subscribe(() => {
            this.confirmedShowAnswer = this.userSubTask.score != null;
            this.selectedAnswerOptionId = this.userSubTask.selectedAnswerOptionId;
        });

        this.loadUserSubTasksId(this.userSubTaskId);
        this.loadUserSubTask(this.userSubTaskId);
    }

    submitAnswerOption() {
        if (!this.selectedAnswerOptionId) return;
        this.userSubTask.selectedAnswerOptionId = this.selectedAnswerOptionId;
        this.userSubTask.isPassed = true;

        this.backendService.updateUserSubTask(this.userSubTask);
    }

    rateSubTask(score: UserSubTaskAnswerScore) {
        this.userSubTask.score = score;
        this.userSubTask.isPassed = true;

        this.backendService.updateUserSubTask(this.userSubTask);
    }

    showAnswer() {
        this.confirmedShowAnswer = true;
    }
}
