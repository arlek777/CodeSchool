import { Component, OnInit, ViewChild, Input, HostListener } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from '../../user-messages';
import { UserLessonBaseComponent } from '../user-lesson-base.component';
import { AuthService } from "../../services/auth.service";

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

    @HostListener('window:keydown',['$event'])
    onKeyPress($event: KeyboardEvent) {
        if(($event.ctrlKey || $event.metaKey) && $event.keyCode === 86)
            this.userLessonAutoSave.CPC += 1;
    }

    @HostListener('window:blur',['$event'])
    onWindowUnFocus($event: any) {
        this.userLessonAutoSave.UF += 1;
    }

    constructor(backendService: BackendService,
        authService: AuthService,
        route: ActivatedRoute,
        router: Router,
        popupService: PopupService) {

        super(backendService, authService, route, router, popupService);
    }

    ngOnInit(): void {
        this.newLessonLoaded$.subscribe(() => {
            this.result = new LessonTestResult();
        });

        this.backendService.canOpenLesson(this.userChapterId, this.userLessonId).then((canOpen) => {
            if (canOpen) {
                this.backendService.startUserTask().then(() => {
                    this.loadUserLessonsId(this.userLessonId);
                    this.loadUserLesson(this.userLessonId);
                    //const testTiming = 10000;
                    const realTiming = 1000 * 60 * 2;
                    setInterval(() => this.autoSave(), realTiming); // each 2 minute
                }, () => this.router.navigate(['invitation']));
            } else {
                this.router.navigate(['invitation']);
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
        var passedCount = this.userLessonIds.filter(l => l.isPassed).length;
        var result = (passedCount * 100) / this.userLessonIds.length;

        return Math.round(result).toString() + "%";
    }
}
