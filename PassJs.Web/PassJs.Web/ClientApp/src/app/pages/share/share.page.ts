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
        this.share.subTaskId = this.route.snapshot.params["subTaskId"];
        this.share.taskHeadId = this.route.snapshot.params["taskHeadId"];

        this.share.linkLifetimeInDays = 1;
        this.share.taskDurationTimeLimit = "1:00";
    }

    submit() {
        if (this.share.subTaskId) {
            this.backendService.shareSubTask(this.share).then(result => this.shareLink = result);
        } else {
            this.backendService.shareTaskHead(this.share).then(result => this.shareLink = result);
        }
    }

    close() {
        this.router.navigate(['/admin-TaskHeads']);
    }
}
