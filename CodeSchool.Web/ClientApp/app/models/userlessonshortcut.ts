import Userlesson = require("./userlesson");
import UserLessonAnswerScore = Userlesson.UserLessonAnswerScore;
import Helpers = require("../utils/helpers");
import Mapper = Helpers.Mapper;

export class UserLessonShortcutModel {
    constructor(model?: UserLessonShortcutModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    userChapterId: number;
    selectedAnswerOptionId?: number;
    score?: UserLessonAnswerScore;
    userId: string;
    isPassed: boolean;
    code: string;
}