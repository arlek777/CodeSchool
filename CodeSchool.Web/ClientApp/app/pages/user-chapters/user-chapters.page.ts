import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserLessonModel } from "../../models/userlesson";
import { Router, ActivatedRoute } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { AuthService } from "../../services/auth.service";
import { ChapterType } from '../../models/chapter';

@Component({
    templateUrl: './user-chapters.page.html'
})
export class UserChaptersPage implements OnInit {
    private chapterTypeParams = {
        "code": { type: ChapterType.Code, localStorageKey: "userCodeChapters" },
        "test": { type: ChapterType.Test, localStorageKey: "userTestChapters" }
    };

    currentTypeParams: { type: ChapterType, localStorageKey: string };
    userChapters: UserChapterModel[];

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService,
        private route: ActivatedRoute,
        private authService: AuthService) {
    }

    ngOnInit() {
        this.route.params.subscribe((params) => {
            var chapterUrlType = params["chapterType"];
            this.currentTypeParams = this.chapterTypeParams[chapterUrlType];
            if (!this.currentTypeParams) {
                this.router.navigate(['/home']);
                return;
            }
        });
    }

    onCategoryChanged(categoryId: number) {
        this.backendService.getUserChapters({ categoryId: categoryId, type: this.currentTypeParams.type }).then(userChapters => {
            this.userChapters = userChapters;
        });
    }

    openChapter(userChapter: UserChapterModel) {
        var userLessons = userChapter.userLessons;
        if (!userLessons.length) return;

        var lessonToOpen = userLessons.find(u => !u.isPassed);
        if (!lessonToOpen) lessonToOpen = userLessons[userLessons.length - 1];

        if (this.currentTypeParams.type === ChapterType.Test) {
            this.router.navigate(['/task', userChapter.id, lessonToOpen.id]);
            return;
        }

        this.backendService.canOpenChapter(userChapter.id)
            .then(canOpen => {
                if (canOpen) {
                    this.router.navigate(['/task', userChapter.id, lessonToOpen.id]);
                } else {
                    this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
                }
            });
    }

    openLesson(userChapter: UserChapterModel, userLesson: UserLessonModel) {
        if (this.currentTypeParams.type === ChapterType.Test) {
            this.router.navigate(['/task', userChapter.id, userLesson.id]);
            return;
        }

        this.backendService.canOpenLesson(userChapter.id, userLesson.id)
            .then(canOpen => {
                if (canOpen) {
                    this.router.navigate(['/task', userChapter.id, userLesson.id]);
                } else {
                    this.popupService.newValidationError(UserMessages.notAllowedOpenLesson);
                }
            });
    }
}
