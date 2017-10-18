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
    code: string;
    unitTestCode: string;
}