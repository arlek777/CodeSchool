import { Mapper } from "../utils/helpers";

export class LessonViewModel {
    constructor(model?: LessonViewModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    chapterId: number;
    text: string;
    taskText: string;
    answerCode: string;
    unitTestsCode: string;
    reporterCode: string;
    title: string;
    order: number;
    published: boolean;
    publishNow: boolean;
}