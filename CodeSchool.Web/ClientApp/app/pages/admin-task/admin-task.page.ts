import { Component, OnInit, ViewChild } from '@angular/core';
import { SubTaskViewModel, SubTaskType, SubTaskLevel } from "../../models/sub-task";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { SubTaskTesterDirective } from "../../directives/task-tester.directive";
import { PopupService } from "../../services/popup.service";
import { Constants } from "../../constants";
import { UserMessages } from '../../user-messages';
import { AnswerSubTaskOptionViewModel } from '../../models/answersubtaskoption';

@Component({
    templateUrl: './admin-task.page.html'
})
export class AdminSubTaskPage implements OnInit {
    subTask: SubTaskViewModel = new SubTaskViewModel();
    answerOption: AnswerSubTaskOptionViewModel = new AnswerSubTaskOptionViewModel();
    isTextPreviewMode = false;

    SubTaskType = SubTaskType;
    SubTaskLevel = SubTaskLevel;

    @ViewChild(SubTaskTesterDirective)
    private SubTaskTester: SubTaskTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router,
        private popupService: PopupService) {
    }

    onKey(event) {
        // Ctrl + S handled
        if (event.keyCode === 83 && event.ctrlKey) {
            event.preventDefault();
            this.addOrUpdate(false);
        }
    }

    ngOnInit(): void {
        var subTaskId = this.route.snapshot.params["subTaskId"];
        if (!subTaskId) {
            this.subTask.taskHeadId = this.route.snapshot.params["taskHeadId"];
            this.subTask.unitTestsCode = Constants.startUnitTest;
            this.subTask.reporterCode = Constants.startSubTaskReporter;
            this.subTask.type = SubTaskType.Code;
            this.subTask.level = SubTaskLevel.Junior;
        } else {
            this.backendService.getSubTask(subTaskId).then(subTask => {
                this.subTask = subTask;
            });
        }
    }

    addOrUpdate(finished: boolean) {
        this.backendService.addOrUpdateSubTask(this.subTask).then((subTask) => {
            if (finished) {
                this.router.navigate(['/admin-task-heads']);
            } else {
                this.subTask = subTask;
                this.router.navigate(["/admin-sub-tasks", this.subTask.taskHeadId, this.subTask.id]);
            }
            this.popupService.newSuccessMessage(UserMessages.addedItem);
        });
    }

    back() {
        if (!confirm(UserMessages.confrimQuestion)) return;
        this.router.navigate(['/admin-task-heads']);
    }

    onTestResultsReceived(result) {
        console.log(result);
    }

    testSubTask() {
        this.SubTaskTester.testSubTask(this.subTask.answer, this.subTask);
    }

    saveAnswerOption() {
        if (!this.answerOption.text) return;

        var optionIndex = this.subTask.answerSubTaskOptions.indexOf(this.answerOption);
        if (optionIndex === -1) {
            this.subTask.answerSubTaskOptions.push(this.answerOption);
        } else {
            this.subTask.answerSubTaskOptions[optionIndex] = this.answerOption;
        }

        this.answerOption = new AnswerSubTaskOptionViewModel();
    }

    editAnswerOption(answerOption: AnswerSubTaskOptionViewModel) {
        this.answerOption = answerOption;
    }

    removeAnswerOption(option: AnswerSubTaskOptionViewModel) {
        if (!confirm(UserMessages.confrimQuestion)) return;

        var index = this.subTask.answerSubTaskOptions.indexOf(option);
        if (index !== -1) {
            this.subTask.answerSubTaskOptions.splice(index, 1);
        }
    }
}
