import { UserSubTaskAnswerScore } from "./user-sub-task";
import Helpers = require("../utils/helpers");
import Mapper = Helpers.Mapper;

export class UserSubTaskShortcutModel {
    constructor(model?: UserSubTaskShortcutModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    userTaskHeadId: number;
    selectedAnswerOptionId?: number;
    score?: UserSubTaskAnswerScore;
    userId: string;
    isPassed: boolean;
    code: string;
}