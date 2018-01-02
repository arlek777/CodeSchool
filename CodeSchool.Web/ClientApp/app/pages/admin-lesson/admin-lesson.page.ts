import { Component, OnInit, ViewChild } from '@angular/core';
import { LessonViewModel, LessonType } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { PopupService } from "../../services/popup.service";
import { Constants } from "../../constants";
import { UserMessages } from '../../user-messages';
import { AnswerLessonOptionViewModel } from '../../models/answerlessonoption';

@Component({
    templateUrl: './admin-lesson.page.html'
})
export class AdminLessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();
    answerOption: AnswerLessonOptionViewModel = new AnswerLessonOptionViewModel();
    isTextPreviewMode = false;

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

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
        var lessonId = this.route.snapshot.params["lessonId"];
        if (!lessonId) {
            this.lesson.chapterId = this.route.snapshot.params["chapterId"];
            this.lesson.unitTestsCode = Constants.startUnitTest;
            this.lesson.reporterCode = Constants.startLessonReporter;
            this.lesson.type = 1;
            this.lesson.level = 0;
        } else {
            this.backendService.getLesson(lessonId).then(lesson => {
                this.lesson = lesson;
            });
        }
    }

    addOrUpdate(finished: boolean) {
        this.backendService.addOrUpdateLesson(this.lesson).then(() => {
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
        this.lessonTester.testLesson(this.lesson.answer, this.lesson);
    }

    showTextPreview() {
        this.isTextPreviewMode = !this.isTextPreviewMode;
    }

    saveAnswerOption() {
        if (!this.answerOption.text) return;

        var optionIndex = this.lesson.answerLessonOptions.indexOf(this.answerOption);
        if (optionIndex === -1) {
            this.lesson.answerLessonOptions.push(this.answerOption);
        } else {
            this.lesson.answerLessonOptions[optionIndex] = this.answerOption;
        }

        this.answerOption = new AnswerLessonOptionViewModel();
    }

    editAnswerOption(answerOption: AnswerLessonOptionViewModel) {
        this.answerOption = answerOption;
    }

    removeAnswerOption(option: AnswerLessonOptionViewModel) {
        if (!confirm(UserMessages.confrimQuestion)) return;

        var index = this.lesson.answerLessonOptions.indexOf(option);
        if (index !== -1) {
            this.lesson.answerLessonOptions.splice(index, 1);
        }
    }
}
