import { Mapper } from "../utils/helpers";
import { LessonViewModel } from "./lesson";

export class ChapterViewModel {
    constructor(model?: ChapterViewModel) {
        if (model) {
            Mapper.map(model, this);

            this.lessons = model.lessons.map(l => new LessonViewModel(l));
        }
    }

    id: number;
    text: string;
    lessons: LessonViewModel[];
}