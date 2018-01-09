import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { UserHelper } from "../../utils/helpers";
import { ActivatedRoute } from "@angular/router";
import { LessonType } from '../../models/lesson';

@Component({
    templateUrl: './user-lesson.page.html'
})
export class UserLessonPage implements OnInit {
    currentLessonType: LessonType;
    LessonType = LessonType;
    userLessonId: number;
    userChapterId: number;

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.userLessonId = parseInt(this.route.snapshot.params["userLessonId"]);
        this.userChapterId = parseInt(this.route.snapshot.params["userChapterId"]);
        this.backendService.getUserLesson(UserHelper.getUserId(), this.userLessonId)
            .then(userLesson => {
                this.currentLessonType = userLesson.lesson.type;
            });
    }
}
