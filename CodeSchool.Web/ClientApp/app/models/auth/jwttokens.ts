import { Mapper } from '../../utils/helpers';

export class JWTTokens {
    constructor(token?: JWTTokens) {
        if (token) {
            Mapper.map(token, this);
        }
    }

    accessToken: string;
    idToken: string;
}