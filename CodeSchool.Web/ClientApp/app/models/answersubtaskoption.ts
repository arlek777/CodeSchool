import { Mapper } from "../utils/helpers";

export class AnswerSubTaskOptionViewModel {
    constructor(model?: AnswerSubTaskOptionViewModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    subTaskId: number;
    text: string;
    isCorrect: boolean;
}