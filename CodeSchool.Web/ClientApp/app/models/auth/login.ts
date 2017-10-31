import { Mapper } from "../../utils/helpers";

export class LoginViewModel {
    constructor(model?: LoginViewModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    public email;
    public password;
}