import { Mapper } from "../utils/helpers";
import { LessonViewModel } from "./lesson";

export class UserLessonModel {
    constructor(model?: UserLessonModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    lessonId: number;
    userChapterId: number;
    userId: string;
    isPassed: boolean;
    code: string;
    updatedDt: any;
    lessonTitle: string;
    lessonOrder: number;
    lesson: LessonViewModel;
}