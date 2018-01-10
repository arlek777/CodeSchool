import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserHelper } from "../../utils/helpers";
import { UserLessonModel } from "../../models/userlesson";
import { Router } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { AuthService } from "../../services/auth.service";
import { ChapterType } from '../../models/chapter';

@Component({
    templateUrl: './user-code-chapters.page.html'
})
export class UserCodeChaptersPage implements OnInit {
    userChapters: UserChapterModel[] = [];

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService,
        private authService: AuthService) {
    }

    ngOnInit() {
        this.backendService.getUserChapters(UserHelper.getUserId(), { type: ChapterType.Code}).then(userChapters => {
            this.userChapters = userChapters;
        });
    }

    openChapter(userChapter: UserChapterModel) {
        var userLessons = userChapter.userLessons;
        if (!userLessons.length) return;

        var lessonToOpen = userLessons.find(u => !u.isPassed);
        if (!lessonToOpen) lessonToOpen = userLessons[userLessons.length - 1];
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
