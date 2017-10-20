import { Component, OnInit } from '@angular/core';
import { ChapterViewModel } from "../../models/chapter";
import { BackendService } from "../../services/backend.service";

@Component({
    templateUrl: './admin-chapters.page.html'
})
export class AdminChaptersPage implements OnInit {
    chapters: ChapterViewModel[] = [];

    constructor(private backendService: BackendService) {
    }

    ngOnInit() {
        this.backendService.getChapters().then(chapters => {
            this.chapters = chapters;
        });
    }
}
