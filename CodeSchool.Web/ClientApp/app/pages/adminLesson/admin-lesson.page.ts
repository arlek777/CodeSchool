import { Component, OnInit } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";

@Component({
    templateUrl: './admin-lesson.page.html'
})
export class AdminLessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.backendService.getLesson(this.route.snapshot.params["id"]).then(lesson => {
            this.lesson = lesson;
        });
    }
}
