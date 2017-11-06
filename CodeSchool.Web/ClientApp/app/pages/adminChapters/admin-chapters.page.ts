import { Component, OnInit } from '@angular/core';
import { ChapterViewModel } from "../../models/chapter";
import { BackendService } from "../../services/backend.service";

@Component({
    templateUrl: './admin-chapters.page.html'
})
export class AdminChaptersPage implements OnInit {
    chapters: ChapterViewModel[] = [];
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

    changeChapterOrder(currentIndex, toSwapIndex) {
        this._swapOrder(currentIndex, toSwapIndex, this.chapters);
        this.chapters = this._sortArrayByOrder(this.chapters);

        var currentId = this.chapters[currentIndex];
        var toSwapId = this.chapters[toSwapIndex];

        this.backendService.changeChapterOrder(currentId, toSwapId);
    }

    changeLessonOrder(chapter: ChapterViewModel, currentIndex, toSwapIndex) {
        this._swapOrder(currentIndex, toSwapIndex, chapter.lessons);
        this._sortLessonsByOrder();

        var currentId = chapter.lessons[currentIndex];
        var toSwapId = chapter.lessons[toSwapIndex];

        this.backendService.changeLessonOrder(currentId, toSwapId);
    }

    private _swapOrder(currentIndex, toSwapIndex, array: Array<any>) {
        var toSwapOrder = array[toSwapIndex].order;
        array[toSwapIndex].order = array[currentIndex].order;
        array[currentIndex].order = toSwapOrder;
    }

    private _sortArrayByOrder(array: Array<any>): Array<any> {
        return array.sort((a, b) => a.order - b.order);
    }

    private _sortLessonsByOrder() {
        this.chapters.forEach(c => c.lessons = this._sortArrayByOrder(c.lessons));
    }
}
