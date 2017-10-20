import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { LessonViewModel } from "../models/lesson";
import { ChapterViewModel } from "../models/chapter";
import 'rxjs/add/operator/toPromise';

@Injectable()
export class BackendService {
    constructor(private http: Http) {
    }

    getLesson(id: number): Promise<LessonViewModel> {
        return this.http.get(`/api/lesson/get/${id}`).toPromise().then((response) => {
            return new LessonViewModel(response.json());
        });
    }

    getNextLesson(chapterId: number, id: number): Promise<LessonViewModel> {
        return this.http.get(`/api/lesson/getnext/${chapterId}/${id}`).toPromise().then((response) => {
            return new LessonViewModel(response.json());
        });
    }

    getChapters(): Promise<ChapterViewModel[]> {
        return this.http.get("/api/chapter/get").toPromise().then((response) => {
            return response.json().map(c => new ChapterViewModel(c));
        });
    }
}