import { Mapper } from "../utils/helpers";
import { UserLessonProgressModel } from "./userlessonprogress";

export class UserChapterProgressModel {
    constructor(model?: UserChapterProgressModel) {
        if (model) {
            Mapper.map(model, this);

            this.lessonProgresses = model.lessonProgresses.map(l => new UserLessonProgressModel(l));
        }
    }

    id: string;
    userId: string;
    lessonProgresses: UserLessonProgressModel[];
    isPassed: boolean;
}