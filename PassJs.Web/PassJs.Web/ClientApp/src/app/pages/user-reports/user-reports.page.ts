import {Component, OnInit, ViewChild} from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { Router } from "@angular/router";

@Component({
    templateUrl: './user-reports.page.html'
})
export class UserReportsPage implements OnInit {
    reports: any[];

    constructor(private backendService: BackendService, private router: Router) {
    }

    ngOnInit(): void {
        this.backendService.getUserTaskReports().then((data) => {
            this.reports = data;
        });
    }

    openReport(rep) {
        this.router.navigate(['user-task-report', encodeURI(rep.userEmail)]);
    }
}
