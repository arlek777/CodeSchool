import { Component, OnInit, ViewChild } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { PopupService } from "../../services/popup.service";
import { Constants } from "../../constants";
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { UserMessages } from '../../user-messages';

@Component({
    templateUrl: './admin-lesson.page.html'
})
export class AdminLessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();
    sanitizedLessonText: SafeHtml = null;
    sanitizedLessonTaskText: SafeHtml = null;
    isTextPreviewMode = false;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router,
        private popupService: PopupService,
        private sanitizer: DomSanitizer) {
    }

    onKey(event) {
        // Ctrl + S handled
        if (event.keyCode === 83 && event.ctrlKey) {
            event.preventDefault();
            this.addOrUpdate(false);
        }
    }

    ngOnInit(): void {
        var lessonId = this.route.snapshot.params["lessonId"];
        if (!lessonId) {
            this.lesson.chapterId = this.route.snapshot.params["chapterId"];
            this.lesson.unitTestsCode = Constants.startUnitTest;
            this.lesson.reporterCode = Constants.startLessonReporter;
        } else {
            this.backendService.getLesson(lessonId).then(lesson => {
                this.lesson = lesson;
            });
        }
    }

    addOrUpdate(finished: boolean) {
        this.backendService.addOrUpdateLesson(this.lesson).then((lesson) => {
            if (finished) {
                this.router.navigate(['/adminchapters']);
            }
            this.popupService.newSuccessMessage(UserMessages.addedItem);
        });
    }

    back() {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.router.navigate(['/adminchapters']);
    }

    onTestResultsReceived(result) {
        console.log(result);
    }

    testLesson() {
        this.lessonTester.testLesson(this.lesson.answerCode, this.lesson);
    }

    showTextPreview() {
        if (!this.isTextPreviewMode) {
            this.sanitizedLessonText = this.sanitizer.bypassSecurityTrustHtml(this.lesson.text);
            this.sanitizedLessonTaskText = this.sanitizer.bypassSecurityTrustHtml(this.lesson.taskText);
            this.isTextPreviewMode = true;
        } else {
            this.isTextPreviewMode = false;
        }
    }
}
