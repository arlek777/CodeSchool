import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserHelper } from "../../utils/helpers";
import { UserLessonModel } from "../../models/userlesson";
import { Router } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { AuthService } from "../../services/auth.service";

@Component({
    templateUrl: './user-chapters.page.html'
})
export class UserChaptersPage implements OnInit {
    userChapters: UserChapterModel[] = [];

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService,
        private authService: AuthService) {
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
        this.router.navigate(['/user-lesson', userChapter.id, lessonToOpen.id]);

        this.backendService.canOpenChapter(userChapter.userId, userChapter.id)
            .then(canOpen => {
                if (canOpen) {
                    this.router.navigate(['/user-lesson', userChapter.id, lessonToOpen.id]);
                } else {
                    this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
                }
            });
    }

    openLesson(userChapter: UserChapterModel, userLesson: UserLessonModel) {
        this.router.navigate(['/user-lesson', userChapter.id, userLesson.id]);

        this.backendService.canOpenLesson(userChapter.userId, userChapter.id, userLesson.id)
            .then(canOpen => {
                if (canOpen) {
                    this.router.navigate(['/user-lesson', userChapter.id, userLesson.id]);
                } else {
                    this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
                }
            });
    }
}
