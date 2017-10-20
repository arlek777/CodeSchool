import { Component, OnInit } from '@angular/core';
import { ChapterViewModel } from "../../models/chapter";
import { BackendService } from "../../services/backend.service";

@Component({
    templateUrl: './chapters.page.html'
})
export class ChaptersPage implements OnInit {
    chapters: ChapterViewModel[] = [];

    constructor(private backendService: BackendService) {
    }

    ngOnInit() {
        this.backendService.getChapters().then(chapters => {
            this.chapters = chapters;
        });
    }
}
