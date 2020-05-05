import { Mapper } from "../utils/helpers";
import { UserLessonShortcutModel } from "./userlessonshortcut";

export class UserLessonAutoSaveModel {
    constructor(model?: UserLessonAutoSaveModel) {
        if (model) {
            Mapper.map(model, this);
        } else {
            this.UF = 0;
            this.CPC = 0;
        }
    }

    userLesson: UserLessonShortcutModel;
    UF: number;
    CPC: number;
}