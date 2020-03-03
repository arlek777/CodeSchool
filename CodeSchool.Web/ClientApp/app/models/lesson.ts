import { Mapper } from "../utils/helpers";
import { AnswerLessonOptionViewModel } from "./answerlessonoption";

export enum LessonType {
    Code = 0,
    Test,
    LongAnswer
}

export enum LessonLevel {
    Junior = 0,
    Middle,
    Senior
}

export class LessonViewModel {
    constructor(model?: LessonViewModel) {
        if (model) {
            Mapper.map(model, this);

            if (this.type === LessonType.Test && model.answerLessonOptions) {
                this.answerLessonOptions = model.answerLessonOptions.map(a => new AnswerLessonOptionViewModel(a));
            }
        }
    }

    id: number;
    chapterId: number;
    companyId: string;
    text: string;
    taskText: string;
    answer: string;
    unitTestsCode: string;
    reporterCode: string;
    title: string;
    order: number;
    published: boolean;
    publishNow: boolean;
    type: LessonType;
    level: LessonLevel;
    answerLessonOptions: AnswerLessonOptionViewModel[] = [];
}