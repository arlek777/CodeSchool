import { Mapper } from "../utils/helpers";

export class UserStatisticModel {
    constructor(user?: UserStatisticModel) {
        Mapper.map(user, this);
    }

    userName: string;
    email: string;
    passedLessons: string[];
}