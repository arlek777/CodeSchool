import { Mapper } from "../utils/helpers";

export class ShareModel {
    constructor(user?: ShareModel) {
        Mapper.map(user, this);
    }

    userFullName: string;
    userEmail: string;
    chapterId: number;
    lessonId?: number;
    linkLifetimeInDays: number;
    taskDurationTimeLimit: string;
}