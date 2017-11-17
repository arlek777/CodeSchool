import { Component, OnInit } from '@angular/core';
import { UserStatisticModel } from "../../models/userstatistic";
import { BackendService } from "../../services/backend.service";

@Component({
    templateUrl: './user-statistic.page.html'
})
export class UserStatisticPage implements OnInit {
    userStatistics: UserStatisticModel[] = [];

    constructor(private backendService: BackendService) {
    }

    ngOnInit(): void {
        this.backendService.getUserStatistics()
            .then(statistics => this.userStatistics = statistics);
    }
  
}
