import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserHelper } from "../../utils/helpers";
import { UserLessonModel } from "../../models/userlesson";
import { Router, ActivatedRoute } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { AuthService } from "../../services/auth.service";
import { ChapterType } from '../../models/chapter';
import { CategoryViewModel } from '../../models/category';

@Component({
    templateUrl: './user-chapters.page.html'
})
export class UserChaptersPage implements OnInit {
    private currentTypeParams: { type: ChapterType, localStorageKey: string };
    private chapterTypeParams = {
        "code": { type: ChapterType.Code, localStorageKey: "userCodeChapters" },
        "test": { type: ChapterType.Test, localStorageKey: "userTestChapters" }
    };

    userChapters: UserChapterModel[] = [];
    categories: CategoryViewModel[] = [];
    selectedCategoryId: number;

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService,
        private route: ActivatedRoute,
        private authService: AuthService) {
    }

    ngOnInit() {
        this.backendService.getCategories().then(categories => {
            this.categories = categories;

            this.route.params.subscribe((params) => {
                var chapterUrlType = params["chapterType"];
                this.currentTypeParams = this.chapterTypeParams[chapterUrlType];
                if (!this.currentTypeParams) {
                    this.router.navigate(['/home']);
                    return;
                }

                this.selectedCategoryId = this.getSavedSelectedCategoryId();
                this.onCategorySelected(this.selectedCategoryId);
            });
        });
    }

    onCategorySelected(categoryId: number) {
        this.selectedCategoryId = categoryId;
        this.saveSelectedCategoryId();
        this.backendService.getUserChapters(UserHelper.getUserId(), { categoryId: categoryId, type: this.currentTypeParams.type }).then(userChapters => {
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

    private saveSelectedCategoryId() {
        localStorage[this.currentTypeParams.localStorageKey] = this.selectedCategoryId;
    }

    private getSavedSelectedCategoryId(): number {
        var savedCategoryId = localStorage[this.currentTypeParams.localStorageKey];
        if (!savedCategoryId) {
            localStorage[this.currentTypeParams.localStorageKey] = this.categories[0].id;
            savedCategoryId = this.categories[0].id;
        }

        return parseInt(savedCategoryId);
    }
}
