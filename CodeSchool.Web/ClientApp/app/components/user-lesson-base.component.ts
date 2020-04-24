import { UserLessonModel } from "../models/userlesson";
import { LessonViewModel } from "../models/lesson";
import { UserMessages } from "../user-messages";
import { BackendService } from "../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { PopupService } from "../services/popup.service";
import { Subject } from "rxjs/Subject";

export abstract class UserLessonBaseComponent {
    private newLessonLoadedSource = new Subject<void>();

    protected newLessonLoaded$ = this.newLessonLoadedSource.asObservable();

    userLesson: UserLessonModel = new UserLessonModel();
    userLessonIds = [];
    currentIndex = -1;
    userChapterId: number;
    userLessonId: number;

    constructor(protected backendService: BackendService,
        protected route: ActivatedRoute,
        protected router: Router,
        protected popupService: PopupService) {

        this.userLesson.lesson = new LessonViewModel();
    }

    getNextLesson() {
        if (!this.userLesson.isPassed) return;

        var nextIndex = ++this.currentIndex;
        if (nextIndex === this.userLessonIds.length) {
            this.router.navigate(['/user-chapters']);
            this.popupService.newSuccessMessage(UserMessages.chapterDone);
            return;
        }

        var nextId = this.userLessonIds[nextIndex].id;
        this.loadUserLesson(nextId);
        this.router.navigate(['/user-lesson', this.userChapterId, nextId]);
    }

    getPreviousLesson() {
        if (this.currentIndex === 0) return;
        var prevIndex = --this.currentIndex;

        this.loadUserLesson(this.userLessonIds[prevIndex].id);
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
            });
    }
}