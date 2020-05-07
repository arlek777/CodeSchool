import { Mapper } from "../utils/helpers";
import { UserSubTaskModel } from "./usersubtask";
import { TaskHeadViewModel } from "./task-head";

export class UserTaskHeadModel {
    constructor(model?: UserTaskHeadModel) {
        if (model) {
            Mapper.map(model, this);

            this.userSubTasks = model.userSubTasks.map(l => new UserSubTaskModel(l));
        }
    }

    id: number;
    userId: string;
    taskHeadId: number;
    taskHeadTitle: string;
    taskHeadOrder: number;
    userSubTasks: UserSubTaskModel[];
    isPassed: boolean;
    isExpanded: boolean;
}