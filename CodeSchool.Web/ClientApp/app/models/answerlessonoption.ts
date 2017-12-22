import { Mapper } from "../utils/helpers";

export class AnswerLessonOptionViewModel {
    constructor(model?: AnswerLessonOptionViewModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    lessonId: number;
    text: string;
    isCorrect: boolean;
}