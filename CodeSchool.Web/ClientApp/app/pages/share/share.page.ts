import { Component, OnInit } from '@angular/core';
import { ShareModel } from "../../models/share";
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    templateUrl: './share.page.html'
})
export class SharePage implements OnInit {
    share: ShareModel = new ShareModel();
    shareLink: string = null;

    constructor(private backendService: BackendService, 
        private route: ActivatedRoute,
        private router: Router) {
    }

    ngOnInit(): void {
        this.share.lessonId = this.route.snapshot.params["lessonId"];
        this.share.chapterId = this.route.snapshot.params["chapterId"];

        this.share.linkLifetimeInDays = 1;
    }

    submit() {
        if (this.share.lessonId) {
            this.backendService.shareLesson(this.share).then(result => this.shareLink = result);
        } else {
            this.backendService.shareChapter(this.share).then(result => this.shareLink = result);
        }
    }

    close() {
        this.router.navigate(['/admin-chapters']);
    }
}
