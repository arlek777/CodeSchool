import { Mapper } from "../utils/helpers";
import { UserLessonModel } from "./userlesson";
import { ChapterViewModel } from "./chapter";

export class UserChapterModel {
    constructor(model?: UserChapterModel) {
        if (model) {
            Mapper.map(model, this);

            this.userLessons = model.userLessons.map(l => new UserLessonModel(l));
        }
    }

    id: string;
    userId: string;
    chapterId: number;
    chapter: ChapterViewModel;
    userLessons: UserLessonModel[];
    isPassed: boolean;
}