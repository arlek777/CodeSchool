import { Mapper } from "../utils/helpers";
import { SubTaskViewModel } from "./sub-task";

export enum UserSubTaskAnswerScore {
    DontKnow = 0,
    HardToRemember,
    KnowIt
}

export class UserSubTaskModel {
    constructor(model?: UserSubTaskModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    subTaskId: number;
    userTaskHeadId: number;
    selectedAnswerOptionId?: number;
    score?: UserSubTaskAnswerScore;
    userId: string;
    isPassed: boolean;
    code: string;
    createdDt: any;
    subTaskTitle: string;
    subTaskOrder: number;
    timeLimit: number;
    subTask: SubTaskViewModel;
}