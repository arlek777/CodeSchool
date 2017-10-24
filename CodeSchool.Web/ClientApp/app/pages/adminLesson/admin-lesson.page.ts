import { Component, OnInit, ViewChild } from '@angular/core';
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LessonTestResult } from "../../models/lessontestresult";
import { LessonTesterDirective } from "../../directives/lesson-tester.directive";

@Component({
    templateUrl: './admin-lesson.page.html'
})
export class AdminLessonPage implements OnInit {
    lesson: LessonViewModel = new LessonViewModel();

    @ViewChild(LessonTesterDirective)
    private lessonTester: LessonTesterDirective;

    constructor(private backendService: BackendService,
        private route: ActivatedRoute,
        private router: Router) {
    }

    ngOnInit(): void {
        var lessonId = this.route.snapshot.params["id"];
        if (!lessonId) {
            this.lesson.chapterId = this.route.snapshot.params["chapterId"];
        }

        this.backendService.getLesson(lessonId).then(lesson => {
            this.lesson = lesson;
        });
    }

    addOrUpdate() {
        this.backendService.addOrUpdateLesson(this.lesson).then((lesson) => {
            this.router.navigate(['/adminchapters']);
        });
    }

    onTestResultsReceived(result) {
        console.log(result);
    }

    testLesson() {
        this.lessonTester.testLesson(this.lesson);
    }
}
