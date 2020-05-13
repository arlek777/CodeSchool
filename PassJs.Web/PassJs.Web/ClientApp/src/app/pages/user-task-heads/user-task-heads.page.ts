import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserTaskHeadModel } from "../../models/usertaskhead";
import { UserSubTaskModel } from "../../models/user-sub-task";
import { Router, ActivatedRoute } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { AuthService } from "../../services/auth.service";
import { TaskHeadType } from '../../models/task-head';

@Component({
    templateUrl: './user-task-heads.page.html'
})
export class UserTaskHeadsPage implements OnInit {
    private TaskHeadTypeParams = {
        "code": { type: TaskHeadType.Code, localStorageKey: "userCodeTaskHeads" },
        "test": { type: TaskHeadType.Test, localStorageKey: "userTestTaskHeads" }
    };

    currentTypeParams: { type: TaskHeadType, localStorageKey: string };
    userTaskHeads: UserTaskHeadModel[];

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService,
        private route: ActivatedRoute,
        private authService: AuthService) {
    }

    ngOnInit() {
        this.route.params.subscribe((params) => {
            var taskHeadUrlType = params["TaskHeadType"];
            this.currentTypeParams = this.TaskHeadTypeParams[taskHeadUrlType];
            if (!this.currentTypeParams) {
                this.router.navigate(['/home']);
                return;
            }
        });
    }

    onCategoryChanged(categoryId: number) {
        this.backendService.getUserTaskHeads({ categoryId: categoryId, type: this.currentTypeParams.type }).then(userTaskHeads => {
            this.userTaskHeads = userTaskHeads;
        });
    }

    openTaskHead(userTaskHead: UserTaskHeadModel) {
        var userSubTasks = userTaskHead.userSubTasks;
        if (!userSubTasks.length) return;

        var SubTaskToOpen = userSubTasks.find(u => !u.isPassed);
        if (!SubTaskToOpen) SubTaskToOpen = userSubTasks[userSubTasks.length - 1];

        if (this.currentTypeParams.type === TaskHeadType.Test) {
            this.router.navigate(['/task', userTaskHead.id, SubTaskToOpen.id]);
            return;
        }

        this.backendService.canOpenTaskHead(userTaskHead.id)
            .then(canOpen => {
                if (canOpen) {
                    this.router.navigate(['/task', userTaskHead.id, SubTaskToOpen.id]);
                } else {
                    this.popupService.newValidationError(UserMessages.notAllowedOpenSubTask);
                }
            });
    }

    openSubTask(userTaskHead: UserTaskHeadModel, userSubTask: UserSubTaskModel) {
        if (this.currentTypeParams.type === TaskHeadType.Test) {
            this.router.navigate(['/task', userTaskHead.id, userSubTask.id]);
            return;
        }

        this.backendService.canOpenSubTask(userTaskHead.id, userSubTask.id)
            .then(canOpen => {
                if (canOpen) {
                    this.router.navigate(['/task', userTaskHead.id, userSubTask.id]);
                } else {
                    this.popupService.newValidationError(UserMessages.notAllowedOpenSubTask);
                }
            });
    }
}
