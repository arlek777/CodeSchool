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

    changeChapterOrder(currentId, mode) {
        var toSwapId = this._swapByOrder(currentId, mode, this.chapters);
        this.chapters = this._sortArrayByOrder(this.chapters);

        this.backendService.changeChapterOrder(currentId, toSwapId);
    }

    changeLessonOrder(chapter, currentId, mode) {
        var toSwapId = this._swapByOrder(currentId, mode, chapter.lessons);
        this._sortLessonsByOrder();

        this.backendService.changeLessonOrder(currentId, toSwapId);
    }

    private _swapByOrder(currentId: any, mode: string, array: Array<any>) {
        var currentIndex = array.findIndex(c => c.id === currentId);
        var tempCurrentIndex = currentIndex;
        var toSwapIndex = mode === 'up' ? --tempCurrentIndex : ++tempCurrentIndex;
        var toSwapId = array[toSwapIndex].id;

        var toSwapOrder = array[toSwapIndex].order;
        array[toSwapIndex].order = array[currentIndex].order;
        array[currentIndex].order = toSwapOrder;

        return toSwapId;
    }

    private _sortArrayByOrder(array: Array<any>): Array<any> {
        return array.sort((a, b) => a.order - b.order);
    }

    private _sortLessonsByOrder() {
        this.chapters.forEach(c => c.lessons = this._sortArrayByOrder(c.lessons));
    }
}
