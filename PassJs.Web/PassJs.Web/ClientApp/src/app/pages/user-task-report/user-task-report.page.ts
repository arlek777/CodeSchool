import {Component, OnInit, ViewChild} from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";

@Component({
    templateUrl: './user-task-report.page.html'
})
export class UserTaskReportPage implements OnInit {
    report: any;

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        const userEmail = this.route.snapshot.params["userEmail"];
        this.backendService.getUserTaskReport(userEmail).then((data) => this.report = data);
    }

    openReport(rep) {

    }
}
