import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { LessonViewModel } from "../models/lesson";
import 'rxjs/add/operator/toPromise';

@Injectable()
export class BackendService {
    constructor(private http: Http) {
    }

    getLesson(): Promise<LessonViewModel> {
        return this.http.get("/api/lesson/get").toPromise().then((response) => {
            return new LessonViewModel(response.json());
        });
    }
}