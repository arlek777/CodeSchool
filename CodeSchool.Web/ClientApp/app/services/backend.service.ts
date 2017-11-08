import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { LessonViewModel } from "../models/lesson";
import { ChapterViewModel } from "../models/chapter";
import { LoginViewModel } from "../models/auth/login";
import { RegistrationViewModel } from "../models/auth/registration";
import { User } from "../models/user";
import { JWTTokens } from "../models/auth/jwttokens";
import 'rxjs/add/operator/toPromise';
import { UserLessonModel } from "../models/userlesson";
import { UserChapterModel } from "../models/userchapter";

@Injectable()
export class BackendService {
    constructor(private http: Http) {
    }

    login(model: LoginViewModel): Promise<JWTTokens> {
        return this.http.post("/api/auth/login", model).toPromise()
            .then((result) => { return new JWTTokens(result.json()); });
    }

    register(model: RegistrationViewModel): Promise<JWTTokens> {
        return this.http.post("/api/auth/register", model).toPromise()
            .then((result) => { return new JWTTokens(result.json()); });
    }

    getLesson(id: number): Promise<LessonViewModel> {
        return this.http.get(`/api/lesson/get/${id}`).toPromise().then((response) => {
            return new LessonViewModel(response.json());
        });
    }

   
    getChapters(): Promise<ChapterViewModel[]> {
        return this.http.get("/api/chapter/get").toPromise().then((response) => {
            return response.json().map(c => new ChapterViewModel(c));
        });
    }

    addOrUpdateChapter(chapter:ChapterViewModel): Promise<ChapterViewModel> {
        return this.http.post("/api/chapter/addorupdate", chapter).toPromise().then((response) => {
            return new ChapterViewModel(response.json());
        });
    }

    removeChapter(id): Promise<void> {
        return this.http.post("/api/chapter/remove", { id: id }).toPromise().then((response) => {
        });
    }

    addOrUpdateLesson(lesson: LessonViewModel): Promise<LessonViewModel> {
        return this.http.post("/api/lesson/addorupdate", lesson).toPromise().then((response) => {
            return new LessonViewModel(response.json());
        });
    }

    removeLesson(id): Promise<void> {
        return this.http.post("/api/lesson/remove", { id: id }).toPromise().then((response) => {
        });
    }

    changeLessonOrder(currentId, toSwapId): Promise<void> {
        return this.http.post("/api/lesson/changeorder", { currentId: currentId, toSwapId: toSwapId }).toPromise()
            .then((response) => {
        });
    }

    changeChapterOrder(currentId, toSwapId): Promise<void> {
        return this.http.post("/api/chapter/changeorder", { currentId: currentId, toSwapId: toSwapId }).toPromise()
            .then((response) => {
        });
    }

    getUserChapters(userId: string): Promise<UserChapterModel[]> {
        return this.http.get(`/api/userchapter/get/${userId}`).toPromise().then((response) => {
            return response.json().map(c => new UserChapterModel(c));
        });
    }

    getUserLessonIds(userId: string, chapterId: number): Promise<number[]> {
        return this.http.get(`/api/userlesson/getidsbychapter/${userId}/${chapterId}`).toPromise().then((response) => {
            return response.json();
        });
    }

    getLatestUserLesson(userId: string, chapterId: number): Promise<UserLessonModel> {
        return this.http.get(`/api/userlesson/getlatestlesson/${userId}/${chapterId}`).toPromise().then((response) => {
            return new UserLessonModel(response.json());
        });
    }

    getUserLesson(userId: string, lessonId: number): Promise<UserLessonModel> {
        return this.http.get(`/api/userlesson/getbyid/${userId}/${lessonId}`).toPromise().then((response) => {
            return new UserLessonModel(response.json());
        });
    }

    updateUserLesson(model): Promise<void> {
        return this.http.post("/api/userlesson/update", model).toPromise()
            .then((response) => {
            });
    }

    getLogs(): Promise<any> {
        return this.http.get(`/api/log/get`).toPromise().then((response) => {
            return response.json();
        });
    }
}