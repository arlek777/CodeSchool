import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserChapterModel } from "../../models/userchapter";
import { UserHelper } from "../../utils/helpers";

@Component({
    templateUrl: './chapters.page.html'
})
export class ChaptersPage implements OnInit {
    userChapters: UserChapterModel[] = [];

    constructor(private backendService: BackendService) {
    }

    ngOnInit() {
        this.backendService.getUserChapters(UserHelper.getUserId()).then(userChapters => {
            this.userChapters = userChapters.sort((a, b) => a.chapter.order - b.chapter.order);
            this.userChapters.forEach(c => c.userLessons = c.userLessons.sort((a, b) => a.lesson.order - b.lesson.order));
        });
    }
}
