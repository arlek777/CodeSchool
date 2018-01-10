import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserHelper } from "../../utils/helpers";
import { UserLessonModel } from "../../models/userlesson";
import { Router } from "@angular/router";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { AuthService } from "../../services/auth.service";
import { CategoryViewModel } from '../../models/category';
import { ChapterType } from '../../models/chapter';

@Component({
    templateUrl: './user-test-chapters.page.html'
})
export class UserTestChaptersPage implements OnInit {
    cachedUserChapters: { [categoryId: number]: UserChapterModel[] } = {};
    userChapters: UserChapterModel[] = [];
    categories: CategoryViewModel[] = [];
    selectedCategoryId: number;

    constructor(private backendService: BackendService,
        private router: Router, private popupService: PopupService,
        private authService: AuthService) {
    }

    ngOnInit() {
        this.backendService.getCategories().then(categories => {
            this.categories = categories;
            this.selectedCategoryId = this.categories[0].id;
            this.onCategorySelected(this.selectedCategoryId);
        });
    }

    onCategorySelected(categoryId: number) {
        if (this.cachedUserChapters[categoryId]) {
            this.userChapters = this.cachedUserChapters[categoryId];
        } else {
            this.backendService.getUserChapters(UserHelper.getUserId(), { categoryId: categoryId, type: ChapterType.Test }).then(userChapters => {
                this.userChapters = userChapters;
                this.cachedUserChapters[categoryId] = userChapters;
            });
        }
    }
}
