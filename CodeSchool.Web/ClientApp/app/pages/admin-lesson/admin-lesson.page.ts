import { Component, OnInit, ViewChild } from '@angular/core';
import { LessonViewModel, LessonType, LessonLevel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";
import { PopupService } from "../../services/popup.service";
import { Constants } from "../../constants";
import { UserMessages } from '../../user-messages';
import { AnswerLessonOptionViewModel } from '../../models/answerlessonoption';
import { UserHelper } from "../../utils/helpers";

@Component({
    templateUrl: './admin-lesson.page.html'
})
export class AdminLessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();
    answerOption: AnswerLessonOptionViewModel = new AnswerLessonOptionViewModel();
    isTextPreviewMode = false;

    LessonType = LessonType;
    LessonLevel = LessonLevel;

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
            this.lesson.type = LessonType.Code;
            this.lesson.level = LessonLevel.Junior;
        } else {
            this.backendService.getLesson(UserHelper.getCompanyId(), lessonId).then(lesson => {
                this.lesson = lesson;
            });
        }
    }

    addOrUpdate(finished: boolean) {
        this.backendService.addOrUpdateLesson(this.lesson).then((lesson) => {
            if (finished) {
                this.router.navigate(['/admin-chapters']);
            } else {
                this.lesson = lesson;
                this.router.navigate(["/admin-lesson", this.lesson.chapterId, this.lesson.id]);
            }
            this.popupService.newSuccessMessage(UserMessages.addedItem);
        });
    }

    back() {
        if (!confirm(UserMessages.confrimQuestion)) return;
        this.router.navigate(['/admin-chapters']);
    }

    onTestResultsReceived(result) {
        console.log(result);
    }

    testLesson() {
        this.lessonTester.testLesson(this.lesson.answer, this.lesson);
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
