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
            this.userChapters = userChapters;
        });
    }

    goToLatestLesson(userChapterId: number) {
        
    }

    goToLesson(userLessonId: number) {
        
    }
}
