import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonType } from '../../models/lesson';

@Component({
    templateUrl: './user-lesson.page.html'
})
export class UserLessonPage implements OnInit {
    LessonType = LessonType;
    currentLessonType = LessonType.Code;
    userLessonId: number;
    userChapterId: number;

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.userLessonId = parseInt(this.route.snapshot.params["userLessonId"]);
        this.userChapterId = parseInt(this.route.snapshot.params["userChapterId"]);
        //this.backendService.getUserLesson(this.userLessonId)
        //    .then(userLesson => {
        //        this.currentLessonType = userLesson.lesson.type;
        //    });
    }
}
