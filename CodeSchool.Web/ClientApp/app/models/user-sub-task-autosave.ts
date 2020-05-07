import { Mapper } from "../utils/helpers";
import { UserSubTaskShortcutModel } from "./user-sub-task-shortcut";

export class UserSubTaskAutoSaveModel {
    constructor(model?: UserSubTaskAutoSaveModel) {
        if (model) {
            Mapper.map(model, this);
        } else {
            this.UF = 0;
            this.CPC = 0;
        }
    }

    userSubTask: UserSubTaskShortcutModel;
    UF: number;
    CPC: number;
}