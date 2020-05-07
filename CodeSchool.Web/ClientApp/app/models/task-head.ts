import { Mapper } from "../utils/helpers";
import { SubTaskViewModel } from "./sub-task";

export enum TaskHeadType {
    Code = 0,
    Test
}

export class TaskHeadViewModel {
    constructor(model?: TaskHeadViewModel) {
        if (model) {
            Mapper.map(model, this);

            this.subTasks = model.subTasks.map(l => new SubTaskViewModel(l));
        }
    }

    id: number;
    text: string;
    subTasks: SubTaskViewModel[];
    categoryId: number;
    order: number;
    type: TaskHeadType;
}