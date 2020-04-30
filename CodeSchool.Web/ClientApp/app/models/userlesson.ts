import { Mapper } from "../utils/helpers";
import { LessonViewModel } from "./lesson";

export enum UserLessonAnswerScore {
    DontKnow = 0,
    HardToRemember,
    KnowIt
}

export class UserLessonModel {
    constructor(model?: UserLessonModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    lessonId: number;
    userChapterId: number;
    selectedAnswerOptionId?: number;
    score?: UserLessonAnswerScore;
    userId: string;
    isPassed: boolean;
    code: string;
    createdDt: any;
    lessonTitle: string;
    lessonOrder: number;
    lesson: LessonViewModel;
}