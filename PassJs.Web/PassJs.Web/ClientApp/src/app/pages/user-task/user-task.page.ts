import { Component, OnInit } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { ActivatedRoute } from "@angular/router";
import { SubTaskType } from '../../models/sub-task';

@Component({
    templateUrl: './user-task.page.html'
})
export class UserSubTaskPage implements OnInit {
    SubTaskType = SubTaskType;
    currentSubTaskType = SubTaskType.Code;
    userSubTaskId: number;
    userTaskHeadId: number;

    constructor(private backendService: BackendService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.userSubTaskId = parseInt(this.route.snapshot.params["userSubTaskId"]);
        this.userTaskHeadId = parseInt(this.route.snapshot.params["userTaskHeadId"]);
        //this.backendService.getUserSubTask(this.userSubTaskId)
        //    .then(userSubTask => {
        //        this.currentSubTaskType = userSubTask.SubTask.type;
        //    });
    }
}
