import { Mapper } from "../utils/helpers";
import { LessonViewModel } from "./lesson";

export class UserLessonModel {
    constructor(model?: UserLessonModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: string;
    lessonId: number;
    userChapterId: string;
    userId: string;
    isPassed: boolean;
    code: string;
    updatedDt: any;
    lessonTitle: string;
    lessonOrder: number;
    lesson: LessonViewModel;
}