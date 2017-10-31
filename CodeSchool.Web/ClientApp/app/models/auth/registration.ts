import { Mapper } from "../../utils/helpers";

export class RegistrationViewModel {
    constructor(model?: RegistrationViewModel) {
        if(model) {
            Mapper.map(model, this);
        }
    }

    public email: string;
    public userName: string;
    public password: string;
}