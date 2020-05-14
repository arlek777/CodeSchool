import { UserSubTaskModel } from "../models/user-sub-task";
import { UserSubTaskAutoSaveModel } from "../models/user-sub-task-autosave";
import { SubTaskViewModel } from "../models/sub-task";
import { BackendService } from "../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { PopupService } from "../services/popup.service";
import { AuthService } from "../services/auth.service";
import { Subject } from "rxjs";
import { UserMessages } from "../user-messages";

export abstract class UserSubTaskBaseComponent {
    private newSubTaskLoadedSource = new Subject<void>();

    protected newSubTaskLoaded$ = this.newSubTaskLoadedSource.asObservable();

    userSubTask: UserSubTaskModel = new UserSubTaskModel();
    userSubTaskAutoSave: UserSubTaskAutoSaveModel = new UserSubTaskAutoSaveModel();
    userSubTaskIds = [];
    currentIndex = -1;
    userTaskHeadId: number;
    userSubTaskId: number;
    showFinishedTaskMessage = false;
    timeLimitDate = null;
    timeLimitFinished = false;

    constructor(protected backendService: BackendService,
        protected authService: AuthService,
        protected route: ActivatedRoute,
        protected router: Router,
        protected popupService: PopupService) {

        this.userSubTask.subTask = new SubTaskViewModel();
    }

    getNextSubTask() {
        var nextIndex = ++this.currentIndex;
        if (nextIndex === this.userSubTaskIds.length) {
            this.finishTask();
            this.currentIndex -= 1;
            return;
        }

        if (!this.userSubTask.isPassed && !confirm(UserMessages.notPassedTaskNextConfirm)) {
            this.currentIndex -= 1;
            return;
        }

        var nextId = this.userSubTaskIds[nextIndex].id;
        this.loadUserSubTask(nextId);
        this.router.navigate(['/task', this.userTaskHeadId, nextId]);
    }

    getPreviousSubTask() {
        if (this.currentIndex === 0) return;
        var prevIndex = --this.currentIndex;

        this.loadUserSubTask(this.userSubTaskIds[prevIndex].id);
    }

    timeLimitCountdown() {
        this.timeLimitFinished = true;
    }

    protected autoSave() {
        this.userSubTaskAutoSave.userSubTask = this.userSubTask;
        this.backendService.autoSaveUserSubTask(this.userSubTaskAutoSave).then(() => {
            this.userSubTaskAutoSave = new UserSubTaskAutoSaveModel();
        });
    }

    protected finishTask() {
        if (confirm(UserMessages.finishUserTaskConfirm)) {
            this.backendService.updateUserSubTask(this.userSubTask).then(() => {
                this.backendService.finishUserTask().then(() => {
                    this.showFinishedTaskMessage = true;

                    setTimeout(() => {
                        this.authService.logout();
                    }, 5000);
                });
            });
        }
    }

    protected loadUserSubTasksId(userSubTaskId){
        this.backendService.getUserSubTaskIds(this.userTaskHeadId).then((userSubTasks) => {
            this.userSubTaskIds = userSubTasks;
            this.currentIndex = this.userSubTaskIds.map(l => l.id).indexOf(userSubTaskId);
        });
    }

    protected loadUserSubTask(userSubTaskId) {
        this.backendService.getUserSubTask(userSubTaskId)
            .then(userSubTask => {
                this.userSubTask = userSubTask;
                this.newSubTaskLoadedSource.next();
                if (this.userSubTask.timeLimit) {
                    this.timeLimitDate = new Date(Date.now() + this.userSubTask.timeLimit);
                }
            });
    }
}
