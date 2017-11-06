import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserHelper } from "../../utils/helpers";
import { UserLessonModel } from "../../models/userlesson";
import { Router } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";

@Component({
    templateUrl: './chapters.page.html'
})
export class ChaptersPage implements OnInit {
    userChapters: UserChapterModel[] = [];

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService) {
    }

    ngOnInit() {
        this.backendService.getUserChapters(UserHelper.getUserId()).then(userChapters => {
            this.userChapters = userChapters;
        });
    }

    openChapter(userChapter: UserChapterModel) {
        var userLessons = userChapter.userLessons;
        if (!userLessons.length) return;

        var lessonToOpen = userLessons.find(u => !u.isPassed);
        if (!lessonToOpen) lessonToOpen = userLessons[userLessons.length - 1];

        var currentIndex = this.userChapters.indexOf(userChapter);
        if (currentIndex == 0 || userChapter.isPassed) {
            this.router.navigate(['/lesson', userChapter.id, lessonToOpen.id]);
            return;
        }

        this.allowToNavigateOrShowError(this.userChapters,
            currentIndex, userChapter.id, lessonToOpen.id);
    }

    openLesson(userChapter: UserChapterModel, userLesson: UserLessonModel) {
        var chapterIndex = this.userChapters.indexOf(userChapter);
        var lessonIndex = userChapter.userLessons.indexOf(userLesson);

        var isFirstInList = chapterIndex == 0 && lessonIndex == 0;
        if (userLesson.isPassed || isFirstInList) {
            this.router.navigate(['/lesson', userChapter.id, userLesson.id]);
            return;
        }

        var isNotFirstChapter = chapterIndex > 0;
        if (isNotFirstChapter) {
            if (!this.isAllBeforePassed(this.userChapters, chapterIndex)) {
                this.popupService
                    .newValidationError(UserMessages.notAllowedOpenLesson);
                return;
            }
        }

        this.allowToNavigateOrShowError(userChapter.userLessons,
            lessonIndex, userChapter.id, userLesson.id);
    }

    private allowToNavigateOrShowError(array, index, chapterId, lessonId) {
        if (this.isAllBeforePassed(array, index)) {
            this.router.navigate(['/lesson', chapterId, lessonId]);
        } else {
            this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
        }
    }

    private isAllBeforePassed(array, tillIndex): boolean {
        var before = array.slice(0, tillIndex);
        return before.every((value) => value.isPassed);
    }
}
