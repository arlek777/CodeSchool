import { Mapper } from "../utils/helpers";

export class CategoryViewModel {
    constructor(model?: CategoryViewModel) {
        if (model) {
            Mapper.map(model, this);
        }
    }

    id: number;
    title: string;
}