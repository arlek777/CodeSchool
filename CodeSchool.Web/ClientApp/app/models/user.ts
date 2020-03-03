import { Mapper } from "../utils/helpers";

export class User {
    constructor(user?: User) {
        Mapper.map(user, this);
    }

    id: string;
    userName: string;
    companyId: string;
    email: string;
    isAdmin: boolean;
}