import { UserLessonModel } from "../models/userlesson";
import { LessonViewModel } from "../models/lesson";
import { BackendService } from "../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { PopupService } from "../services/popup.service";
import { AuthService } from "../services/auth.service";
import { Subject } from "rxjs/Subject";
import { UserMessages } from "../user-messages";

export abstract class UserLessonBaseComponent {
    private newLessonLoadedSource = new Subject<void>();

    protected newLessonLoaded$ = this.newLessonLoadedSource.asObservable();

    userLesson: UserLessonModel = new UserLessonModel();
    userLessonIds = [];
    currentIndex = -1;
    userChapterId: number;
    userLessonId: number;
    showFinishedTaskMessage = false;
    timeLimitDate = null;
    timeLimitFinished = false;

    constructor(protected backendService: BackendService,
        protected authService: AuthService,
        protected route: ActivatedRoute,
        protected router: Router,
        protected popupService: PopupService) {

        this.userLesson.lesson = new LessonViewModel();
    }

    getNextLesson() {
        var nextIndex = ++this.currentIndex;
        if (nextIndex === this.userLessonIds.length) {
            this.finishTask();
            this.currentIndex -= 1;
            return;
        }

       if (!this.userLesson.isPassed) {
            if (!confirm(UserMessages.notPassedTaskNextConfirm)
            ) {
                return;
            }
        }

        var nextId = this.userLessonIds[nextIndex].id;
        this.loadUserLesson(nextId);
        this.router.navigate(['/task', this.userChapterId, nextId]);
    }

    getPreviousLesson() {
        if (this.currentIndex === 0) return;
        var prevIndex = --this.currentIndex;

        this.loadUserLesson(this.userLessonIds[prevIndex].id);
    }

    timeLimitCountdown() {
        this.timeLimitFinished = true;
    }

    protected finishTask() {
        if (confirm(UserMessages.finishUserTaskConfirm)) {
            this.backendService.updateUserLesson(this.userLesson).then(() => {
                this.backendService.finishUserTask().then(() => {
                    this.showFinishedTaskMessage = true;

                    setTimeout(() => {
                        this.authService.logout();
                    }, 5000);
                });
            });
        }
    }

    protected loadUserLessonsId(userLessonId){
        this.backendService.getUserLessonIds(this.userChapterId).then((userLessons) => {
            this.userLessonIds = userLessons;
            this.currentIndex = this.userLessonIds.map(l => l.id).indexOf(userLessonId);
        });
    }

    protected loadUserLesson(userLessonId) {
        this.backendService.getUserLesson(userLessonId)
            .then(userLesson => {
                this.userLesson = userLesson;
                this.newLessonLoadedSource.next();
                if (this.userLesson.timeLimit) {
                    this.timeLimitDate = new Date(Date.now() + this.userLesson.timeLimit);
                }
            });
    }
}