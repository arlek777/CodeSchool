import { Mapper } from "../utils/helpers";

export class UserLessonProgressModel {
    constructor(model?: UserLessonProgressModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: string;
    lessonId: number;
    userChapterProgressId: string;
    userId: string;
    isPassed: boolean;
    code: string;
    updatedDt: any;
}