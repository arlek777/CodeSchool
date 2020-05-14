import { Component, OnInit, ViewChild, Input, HostListener } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { SubTaskTestResult } from "../../models/tasktestresult";
import { SubTaskTesterDirective } from "../../directives/task-tester.directive";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from '../../user-messages';
import { UserSubTaskBaseComponent } from '../user-sub-task-base.component';
import { AuthService } from "../../services/auth.service";

@Component({
    selector: "user-sub-task-code",
    templateUrl: './user-sub-task-code.component.html'
})
export class UserSubTaskCodeComponent extends UserSubTaskBaseComponent implements OnInit  {
    @Input("user-task-head-id")
    userTaskHeadId: number;

    @Input("user-sub-task-id")
    userSubTaskId: number;

    result: SubTaskTestResult;
    failedAttempts = 0;

    @ViewChild(SubTaskTesterDirective, { static: false })
    private SubTaskTester: SubTaskTesterDirective;

    @HostListener('window:keydown',['$event'])
    onKeyPress($event: KeyboardEvent) {
        if(($event.ctrlKey || $event.metaKey) && $event.keyCode === 86)
            this.userSubTaskAutoSave.CPC += 1;
    }

    @HostListener('window:blur',['$event'])
    onWindowUnFocus($event: any) {
        this.userSubTaskAutoSave.UF += 1;
    }

    constructor(backendService: BackendService,
        authService: AuthService,
        route: ActivatedRoute,
        router: Router,
        popupService: PopupService) {

        super(backendService, authService, route, router, popupService);
    }

    ngOnInit(): void {
        this.newSubTaskLoaded$.subscribe(() => {
            this.result = new SubTaskTestResult();
        });

        this.backendService.canOpenSubTask(this.userTaskHeadId, this.userSubTaskId).then((canOpen) => {
            if (canOpen) {
                this.backendService.startUserTask().then(() => {
                    this.loadUserSubTasksId(this.userSubTaskId);
                    this.loadUserSubTask(this.userSubTaskId);
                    //const testTiming = 10000;
                    const realTiming = 1000 * 60 * 2;
                    setInterval(() => this.autoSave(), realTiming); // each 2 minute
                }, () => this.router.navigate(['invitation']));
            } else {
                this.router.navigate(['invitation']);
            }
        });

        
    }

    onTestResultsReceived(result: SubTaskTestResult) {
        this.result = result;
        this.userSubTask.isPassed = result.isSucceeded;
        this.userSubTaskIds[this.currentIndex].isPassed = result.isSucceeded;
        this.failedAttempts = result.isSucceeded ? 0 : ++this.failedAttempts;

        this.backendService.updateUserSubTask(this.userSubTask);
    }

    checkSubTask() {
        if (!this.userSubTask.code) return;

        this.SubTaskTester.checkSubTask(this.userSubTask.code, this.userSubTask.subTask);
    }

    showAnswer() {
        if (confirm(UserMessages.showAnswerConfirm)) {
            this.userSubTask.code = this.userSubTask.subTask.answer;
        }
    }

    getProgressInPercents(): string {
        var passedCount = this.userSubTaskIds.filter(l => l.isPassed).length;
        var result = (passedCount * 100) / this.userSubTaskIds.length;

        return Math.round(result).toString() + "%";
    }
}
