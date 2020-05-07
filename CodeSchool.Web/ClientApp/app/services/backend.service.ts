import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { SubTaskViewModel } from "../models/sub-task";
import { TaskHeadViewModel, TaskHeadType } from "../models/task-head";
import { LoginViewModel } from "../models/auth/login";
import { RegistrationViewModel } from "../models/auth/registration";
import { JWTTokens } from "../models/auth/jwttokens";
import 'rxjs/add/operator/toPromise';
import { UserSubTaskModel } from "../models/userSubTask";
import { UserSubTaskAutoSaveModel } from "../models/userSubTaskautosave";
import { UserTaskHeadModel } from "../models/userTaskHead";
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

    loginByToken(token: string): Promise<JWTTokens> {
        return this.http.get("/api/auth/loginByToken?token=" + token).toPromise()
            .then((result) => { return new JWTTokens(result.json()); });
    }

    getInvitation(token: string): Promise<any> {
        return this.http.get("/api/auth/getInvitation?token=" + token).toPromise()
            .then((result) => { return result.json(); });
    }

    startUserTask(): Promise<any> {
        return this.http.post("/api/userTaskHead/startUserTask", {}).toPromise()
            .then((result) => { return result.json(); });
    }

    finishUserTask(): Promise<any> {
        return this.http.post("/api/userTaskHead/finishUserTask", {}).toPromise()
            .then((result) => { return result.json(); });
    }

    getFirstTaskHeadAndSubTask(): Promise<any> {
        return this.http.get("/api/userTaskHead/getFirstTaskHeadAndSubTask", {}).toPromise()
            .then((result) => { return result.json(); });
    }

    getSubTask(id: number): Promise<SubTaskViewModel> {
        return this.http.get(`/api/SubTask/get/${id}`).toPromise().then((response) => {
            return new SubTaskViewModel(response.json());
        });
    }
   
    getTaskHeads(): Promise<TaskHeadViewModel[]> {
        return this.http.get(`/api/TaskHead/get`).toPromise().then((response) => {
            return response.json().map(c => new TaskHeadViewModel(c));
        });
    }

    getTaskHeadsByCategoryId(categoryId: number): Promise<TaskHeadViewModel[]> {
        return this.http.get(`/api/TaskHead/getbycategoryid/${categoryId}`).toPromise().then((response) => {
            return response.json().map(c => new TaskHeadViewModel(c));
        });
    }

    addOrUpdateTaskHead(TaskHead:TaskHeadViewModel): Promise<TaskHeadViewModel> {
        return this.http.post("/api/TaskHead/addorupdate", TaskHead).toPromise().then((response) => {
            return new TaskHeadViewModel(response.json());
        });
    }

    removeTaskHead(id): Promise<void> {
        return this.http.post("/api/TaskHead/remove", { id: id }).toPromise().then((response) => {
        });
    }

    addOrUpdateSubTask(SubTask: SubTaskViewModel): Promise<SubTaskViewModel> {
        return this.http.post("/api/SubTask/addorupdate", SubTask).toPromise().then((response) => {
            return new SubTaskViewModel(response.json());
        });
    }

    removeSubTask(id): Promise<void> {
        return this.http.post("/api/SubTask/remove", { id: id }).toPromise().then((response) => {
        });
    }

    changeSubTaskOrder(currentId, toSwapId): Promise<void> {
        return this.http.post("/api/SubTask/changeorder", { currentId: currentId, toSwapId: toSwapId }).toPromise()
            .then((response) => {
        });
    }

    changeTaskHeadOrder(currentId, toSwapId): Promise<void> {
        return this.http.post("/api/TaskHead/changeorder", { currentId: currentId, toSwapId: toSwapId }).toPromise()
            .then((response) => {
        });
    }

    shareTaskHead(model: any): Promise<string> {
        return this.http.post("/api/share/shareTaskHead", model).toPromise()
            .then((response) => response.text());
    }

    shareSubTask(model: any): Promise<string> {
        return this.http.post("/api/share/shareSubTask", model).toPromise()
            .then((response) => {
                return response.text();
            });
    }

    getUserTaskHeads(filterModel?: { categoryId?: number, type?: TaskHeadType}): Promise<UserTaskHeadModel[]> {
        let params = new URLSearchParams();
        if (filterModel) {
            for (let key in filterModel) {
                if (filterModel[key] !== null || filterModel[key] !== undefined) {
                    params.set(key, filterModel[key]);
                }
            }
        }
        var url;
        if (params.toString()) {
            url = `/api/userTaskHead/get?${params.toString()}`;
        } else {
            url = `/api/userTaskHead/get`;
        }
        return this.http.get(url).toPromise().then((response) => {
            return response.json().map(c => new UserTaskHeadModel(c));
        });
    }

    getUserSubTaskIds(userTaskHeadId: number): Promise<number[]> {
        return this.http.get(`/api/userSubTask/getuserSubTaskids?userTaskHeadId=${userTaskHeadId}`).toPromise().then((response) => {
            return response.json();
        });
    }

    getUserSubTask(userSubTaskId: number): Promise<UserSubTaskModel> {
        return this.http.get(`/api/userSubTask/getbyid?userSubTaskId=${userSubTaskId}`).toPromise().then((response) => {
            return new UserSubTaskModel(response.json());
        });
    }

    updateUserSubTask(model): Promise<void> {
        return this.http.post("/api/userSubTask/update", model).toPromise()
            .then((response) => {
            });
    }
    
    autoSaveUserSubTask(model: UserSubTaskAutoSaveModel): Promise<void> {
        return this.http.post("/api/userSubTask/autoSave", model).toPromise()
            .then((response) => {
            });
    }

    getUserTaskReports(): Promise<any[]> {
        return this.http.get("/api/userSubTaskReport/getUserReports/").toPromise().then((response) => {
            return response.json();
        });
    }

    canOpenTaskHead(userTaskHeadId): Promise<boolean> {
        return this.http.post("/api/userTaskHead/canopen/", { userTaskHeadId: userTaskHeadId })
            .toPromise().then((response) => {
                return response.json();
            });
    }

    canOpenSubTask(userTaskHeadId, userSubTaskId): Promise<boolean> {
        return this.http.post("/api/userSubTask/canopen/", { userTaskHeadId: userTaskHeadId, userSubTaskId: userSubTaskId })
            .toPromise().then((response) => {
                return response.json();
            });
    }

    publishSubTask(TaskHeadId, SubTaskId): Promise<void> {
        return this.http.post("/api/SubTask/publish/", { TaskHeadId: TaskHeadId, SubTaskId: SubTaskId })
            .toPromise().then(() => {
            });
    }

    getCategories(): Promise<CategoryViewModel[]> {
        return this.http.get("/api/category/get/").toPromise().then((response) => {
            return response.json();
        });
    }

    getUserTaskReport(userEmail: string) {
        return this.http.get("/api/userSubTaskReport/getDetailedUserReport?userEmail=" + userEmail).toPromise().then((response) => {
            return response.json();
        });
    }
}