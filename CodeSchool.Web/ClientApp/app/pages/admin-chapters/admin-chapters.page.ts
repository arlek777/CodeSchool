import { Component, OnInit } from '@angular/core';
import { ChapterViewModel } from "../../models/chapter";
import { LessonViewModel } from "../../models/lesson";
import { BackendService } from "../../services/backend.service";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";

@Component({
    templateUrl: './admin-chapters.page.html'
})
export class AdminChaptersPage implements OnInit {
    private currentCategoryId: number;

    chapters: ChapterViewModel[] = [];
    chapter: ChapterViewModel = new ChapterViewModel();

    constructor(private backendService: BackendService, private popupService: PopupService) {
    }

    ngOnInit() {
        
    }

    onCategoryChanged(categoryId: number) {
        this.currentCategoryId = categoryId;
        this.chapter.categoryId = categoryId;
        this.backendService.getChaptersByCategoryId(categoryId).then(chapters => {
            this.chapters = chapters;
        });
    }

    addOrUpdateChapter() {
        this.backendService.addOrUpdateChapter(this.chapter).then((newChapter) => {
            if (this.chapter.id !== newChapter.id) {
                this.chapters.push(newChapter);
            } 
            this.chapter = new ChapterViewModel();
            this.chapter.categoryId = this.currentCategoryId;
            this.popupService.newSuccessMessage(UserMessages.addedItem);
        });
    }

    editChapter(chapter) {
        this.chapter = chapter;
    }

    publishLesson(chapterId, lesson: LessonViewModel) {
        if (!confirm(UserMessages.publishLesson)) return;

        this.backendService.publishLesson(chapterId, lesson.id).then(() => {
            this.popupService.newSuccessMessage(UserMessages.published);
            lesson.published = true;
        });
    }

    removeLesson(chapter: ChapterViewModel, id) {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.backendService.removeLesson(id).then(() => {
            chapter.lessons = chapter.lessons.filter(l => l.id !== id);
        });
    }

    removeChapter(id) {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.backendService.removeChapter(id).then(() => {
            this.chapters = this.chapters.filter(ch => ch.id !== id);
        });
    }

    changeChapterOrder(currentIndex, toSwapIndex) {
        this.swapOrder(currentIndex, toSwapIndex, this.chapters);
        this.chapters = this.sortArrayByOrder(this.chapters);

        var currentId = this.chapters[currentIndex].id;
        var toSwapId = this.chapters[toSwapIndex].id;

        this.backendService.changeChapterOrder(currentId, toSwapId);
    }

    changeLessonOrder(chapter: ChapterViewModel, currentIndex, toSwapIndex) {
        this.swapOrder(currentIndex, toSwapIndex, chapter.lessons);
        this.sortLessonsByOrder();

        var currentId = chapter.lessons[currentIndex].id;
        var toSwapId = chapter.lessons[toSwapIndex].id;

        this.backendService.changeLessonOrder(currentId, toSwapId);
    }

    private swapOrder(currentIndex, toSwapIndex, array: Array<any>) {
        var toSwapOrder = array[toSwapIndex].order;
        array[toSwapIndex].order = array[currentIndex].order;
        array[currentIndex].order = toSwapOrder;
    }

    private sortArrayByOrder(array: Array<any>): Array<any> {
        return array.sort((a, b) => a.order - b.order);
    }

    private sortLessonsByOrder() {
        this.chapters.forEach(c => c.lessons = this.sortArrayByOrder(c.lessons));
    }
}
