import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { LessonViewModel } from "../models/lesson";
import { ChapterViewModel, ChapterType } from "../models/chapter";
import { LoginViewModel } from "../models/auth/login";
import { UserStatisticModel } from "../models/userstatistic";
import { RegistrationViewModel } from "../models/auth/registration";
import { User } from "../models/user";
import { JWTTokens } from "../models/auth/jwttokens";
import 'rxjs/add/operator/toPromise';
import { UserLessonModel } from "../models/userlesson";
import { UserChapterModel } from "../models/userchapter";
import { CategoryViewModel } from "../models/category";

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

    getUserChapters(userId: string, filterModel?: { categoryId?: number, type?: ChapterType}): Promise<UserChapterModel[]> {
        let params = new URLSearchParams();
        params.set("userId", userId);
        if (filterModel) {
            for (let key in filterModel) {
                if (filterModel[key] !== null || filterModel[key] !== undefined) {
                    params.set(key, filterModel[key]);
                }
            }
        }
        var url = `/api/userchapter/get?${params.toString()}`;
        console.log(url);
        return this.http.get(url).toPromise().then((response) => {
            return response.json().map(c => new UserChapterModel(c));
        });
    }

    getUserLessonIds(userId: string, userChapterId: number): Promise<number[]> {
        return this.http.get(`/api/userlesson/getuserlessonids/${userId}/${userChapterId}`).toPromise().then((response) => {
            return response.json();
        });
    }

    getUserLesson(userId: string, userLessonId: number): Promise<UserLessonModel> {
        return this.http.get(`/api/userlesson/getbyid/${userId}/${userLessonId}`).toPromise().then((response) => {
            return new UserLessonModel(response.json());
        });
    }

    updateUserLesson(model): Promise<void> {
        return this.http.post("/api/userlesson/update", model).toPromise()
            .then((response) => {
            });
    }

    getUserStatistics(): Promise<UserStatisticModel[]> {
        return this.http.get("/api/userstatistic/get/").toPromise().then((response) => {
            return response.json();
        });
    }

    canOpenChapter(userId, userChapterId): Promise<boolean> {
        return this.http.post("/api/userchapter/canopen/", { userId: userId, userChapterId: userChapterId })
            .toPromise().then((response) => {
                return response.json();
            });
    }

    canOpenLesson(userId, userChapterId, userLessonId): Promise<boolean> {
        return this.http.post("/api/userlesson/canopen/", { userId: userId, userChapterId: userChapterId, userLessonId: userLessonId })
            .toPromise().then((response) => {
                return response.json();
            });
    }

    publishLesson(chapterId, lessonId): Promise<void> {
        return this.http.post("/api/lesson/publish/", { chapterId: chapterId, lessonId: lessonId })
            .toPromise().then(() => {
            });
    }

    getCategories(): Promise<CategoryViewModel[]> {
        return this.http.get("/api/category/get/").toPromise().then((response) => {
            return response.json();
        });
    }
}