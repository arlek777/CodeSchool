import { Mapper } from "../utils/helpers";
import { AnswerSubTaskOptionViewModel } from "./answersubtaskoption";

export enum SubTaskType {
    Code = 0,
    Test,
    LongAnswer
}

export enum SubTaskLevel {
    Junior = 0,
    Middle,
    Senior
}

export class SubTaskViewModel {
    constructor(model?: SubTaskViewModel) {
        if (model) {
            Mapper.map(model, this);

            if (this.type === SubTaskType.Test && model.answerSubTaskOptions) {
                this.answerSubTaskOptions = model.answerSubTaskOptions.map(a => new AnswerSubTaskOptionViewModel(a));
            }
        }
    }

    id: number;
    taskHeadId: number;
    text: string;
    taskText: string;
    answer: string;
    unitTestsCode: string;
    reporterCode: string;
    title: string;
    order: number;
    published: boolean;
    publishNow: boolean;
    type: SubTaskType;
    level: SubTaskLevel;
    answerSubTaskOptions: AnswerSubTaskOptionViewModel[] = [];
}