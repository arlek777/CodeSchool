import { Component } from '@angular/core';
import { TaskHeadViewModel } from "../../models/task-head";
import { SubTaskViewModel } from "../../models/sub-task";
import { BackendService } from "../../services/backend.service";
import { PopupService } from "../../services/popup.service";
import { UserMessages } from "../../user-messages";
import { TaskHeadType } from "../../models/task-head";

@Component({
    templateUrl: './admin-task-heads.page.html'
})
export class AdminTaskHeadsPage {
    private currentCategoryId: number = 1;

    taskHeads: TaskHeadViewModel[] = [];
    taskHead: TaskHeadViewModel = new TaskHeadViewModel();
    TaskHeadType = TaskHeadType;

    constructor(private backendService: BackendService, private popupService: PopupService) {
    }

    ngOnInit() {
        this.taskHead.categoryId = this.currentCategoryId;
        this.backendService.getTaskHeadsByCategoryId(this.currentCategoryId).then(taskHeads => {
            this.taskHeads = taskHeads;
        });
    }

    addOrUpdateTaskHead() {
        this.backendService.addOrUpdateTaskHead(this.taskHead).then((newTaskHead) => {
            if (this.taskHead.id !== newTaskHead.id) {
                this.taskHeads.push(newTaskHead);
            } 
            this.taskHead = new TaskHeadViewModel();
            this.taskHead.categoryId = this.currentCategoryId;
            this.popupService.newSuccessMessage(UserMessages.addedItem);
        });
    }

    editTaskHead(taskHead) {
        this.taskHead = taskHead;
    }

    publishSubTask(TaskHeadId, SubTask: SubTaskViewModel) {
        if (!confirm(UserMessages.publishSubTask)) return;

        this.backendService.publishSubTask(TaskHeadId, SubTask.id).then(() => {
            this.popupService.newSuccessMessage(UserMessages.published);
            SubTask.published = true;
        });
    }

    removeSubTask(taskHead: TaskHeadViewModel, id) {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.backendService.removeSubTask(id).then(() => {
            taskHead.subTasks = taskHead.subTasks.filter(l => l.id !== id);
        });
    }

    removeTaskHead(id) {
        var result = confirm("Are you sure?");
        if (!result) return;

        this.backendService.removeTaskHead(id).then(() => {
            this.taskHeads = this.taskHeads.filter(ch => ch.id !== id);
        });
    }

    changeTaskHeadOrder(currentIndex, toSwapIndex) {
        this.swapOrder(currentIndex, toSwapIndex, this.taskHeads);
        this.taskHeads = this.sortArrayByOrder(this.taskHeads);

        var currentId = this.taskHeads[currentIndex].id;
        var toSwapId = this.taskHeads[toSwapIndex].id;

        this.backendService.changeTaskHeadOrder(currentId, toSwapId);
    }

    changeSubTaskOrder(taskHead: TaskHeadViewModel, currentIndex, toSwapIndex) {
        this.swapOrder(currentIndex, toSwapIndex, taskHead.subTasks);
        this.sortSubTasksByOrder();

        var currentId = taskHead.subTasks[currentIndex].id;
        var toSwapId = taskHead.subTasks[toSwapIndex].id;

        this.backendService.changeSubTaskOrder(currentId, toSwapId);
    }

    private swapOrder(currentIndex, toSwapIndex, array: Array<any>) {
        var toSwapOrder = array[toSwapIndex].order;
        array[toSwapIndex].order = array[currentIndex].order;
        array[currentIndex].order = toSwapOrder;
    }

    private sortArrayByOrder(array: Array<any>): Array<any> {
        return array.sort((a, b) => a.order - b.order);
    }

    private sortSubTasksByOrder() {
        this.taskHeads.forEach(c => c.subTasks = this.sortArrayByOrder(c.subTasks));
    }
}
