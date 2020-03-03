import { Mapper } from "../utils/helpers";
import { LessonViewModel } from "./lesson";

export enum ChapterType {
    Code = 0,
    Test
}

export class ChapterViewModel {
    constructor(model?: ChapterViewModel) {
        if (model) {
            Mapper.map(model, this);

            this.lessons = model.lessons.map(l => new LessonViewModel(l));
        }
    }

    id: number;
    text: string;
    companyId: string;
    lessons: LessonViewModel[];
    categoryId: number;
    order: number;
    type: ChapterType;
}