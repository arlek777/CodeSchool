import { Mapper } from "../utils/helpers";

export class User {
    constructor(user?: User) {
        Mapper.map(user, this);
    }

    userName: string;
    email: string;
    isAdmin: boolean;
}