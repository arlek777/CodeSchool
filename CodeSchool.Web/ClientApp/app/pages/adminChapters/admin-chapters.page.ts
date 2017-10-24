import { Component, OnInit } from '@angular/core';
import { ChapterViewModel } from "../../models/chapter";
import { BackendService } from "../../services/backend.service";

@Component({
    templateUrl: './admin-chapters.page.html'
})
export class AdminChaptersPage implements OnInit {
    chapters: ChapterViewModel[] = [];
    oldChapter: ChapterViewModel;
    chapter: ChapterViewModel = new ChapterViewModel();

    constructor(private backendService: BackendService) {
    }

    ngOnInit() {
        this.backendService.getChapters().then(chapters => {
            this.chapters = chapters;
        });
    }

    addOrUpdateChapter() {
        this.backendService.addOrUpdateChapter(this.chapter).then((newChapter) => {
            if (this.chapter.id !== newChapter.id) {
                this.chapters.push(newChapter);
            } 
            this.chapter = new ChapterViewModel();
        });
    }

    editChapter(chapter) {
        this.chapter = chapter;
    }

    removeLesson(chapter: ChapterViewModel, id) {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.backendService.removeLesson(id).then(() => {
            chapter.lessons = chapter.lessons.filter(l => l.id !== id);
        })
    }

    removeChapter(id) {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.backendService.removeChapter(id).then(() => {
            this.chapters = this.chapters.filter(ch => ch.id !== id);
        });
    }
}
