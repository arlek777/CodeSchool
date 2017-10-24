import { Component, OnInit, ViewChild } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";

@Component({
    templateUrl: './lesson.page.html'
})
export class LessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();
    result: LessonTestResult = new LessonTestResult();

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.backendService.getLesson(this.route.snapshot.params["id"]).then(lesson => {
            this.lesson = lesson;
        });
    }

    getNextLesson() {
        var chapterId = this.route.snapshot.params["chapterId"];
        var lessonId = this.route.snapshot.params["id"];
        this.backendService.getNextLesson(chapterId, lessonId).then(lesson => {
            this.lesson = lesson;
        });
    }

    onTestResultsReceived(result) {
        this.result = result;
    }

    checkLesson() {
        this.lessonTester.checkLesson(this.lesson);
    }
}
