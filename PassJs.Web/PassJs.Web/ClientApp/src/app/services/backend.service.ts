import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SubTaskViewModel } from "../models/sub-task";
import { TaskHeadViewModel, TaskHeadType } from "../models/task-head";
import { LoginViewModel } from "../models/auth/login";
import { RegistrationViewModel } from "../models/auth/registration";
import { JWTTokens } from "../models/auth/jwttokens";
import { UserSubTaskModel } from "../models/user-sub-task";
import { UserSubTaskAutoSaveModel } from "../models/user-sub-task-autosave";
import { UserTaskHeadModel } from "../models/userTaskHead";
import { CategoryViewModel } from "../models/category";

@Injectable()
export class BackendService {
    constructor(private http: HttpClient) {
    }

    login(model: LoginViewModel): Promise<JWTTokens> {
        return this.http.post<any>("/api/auth/login", model).toPromise()
            .then((result) => { return new JWTTokens(result); });
    }

    register(model: RegistrationViewModel): Promise<JWTTokens> {
        return this.http.post<any>("/api/auth/register", model).toPromise()
            .then((result) => { return new JWTTokens(result); });
    }

    loginByToken(token: string): Promise<JWTTokens> {
        return this.http.get<any>("/api/auth/loginByToken?token=" + token).toPromise()
            .then((result) => { return new JWTTokens(result); });
    }

    getInvitation(token: string): Promise<any> {
        return this.http.get<any>("/api/auth/getInvitation?token=" + token).toPromise()
            .then((result) => { return result; });
    }

    startUserTask(): Promise<any> {
        return this.http.post<any>("/api/userTaskHead/startUserTask", {}).toPromise()
            .then((result) => { return result; });
    }

    finishUserTask(): Promise<any> {
        return this.http.post<any>("/api/userTaskHead/finishUserTask", {}).toPromise()
            .then((result) => { return result; });
    }

    getFirstTaskHeadAndSubTask(): Promise<any> {
        return this.http.get<any>("/api/userTaskHead/getFirstTaskHeadAndSubTask", {}).toPromise()
            .then((result) => { return result; });
    }

    getSubTask(id: number): Promise<SubTaskViewModel> {
        return this.http.get<any>(`/api/SubTask/get<any>/${id}`).toPromise().then((response) => {
            return new SubTaskViewModel(response);
        });
    }
   
    getTaskHeads(): Promise<TaskHeadViewModel[]> {
        return this.http.get<any>(`/api/TaskHead/get<any>`).toPromise().then((response) => {
            return response.map(c => new TaskHeadViewModel(c));
        });
    }

    getTaskHeadsByCategoryId(categoryId: number): Promise<TaskHeadViewModel[]> {
        return this.http.get<any>(`/api/TaskHead/getbycategoryid/${categoryId}`).toPromise().then((response) => {
            return response.map(c => new TaskHeadViewModel(c));
        });
    }

    addOrUpdateTaskHead(TaskHead:TaskHeadViewModel): Promise<TaskHeadViewModel> {
        return this.http.post<any>("/api/TaskHead/addorupdate", TaskHead).toPromise().then((response) => {
            return new TaskHeadViewModel(response);
        });
    }

    removeTaskHead(id): Promise<void> {
        return this.http.post<any>("/api/TaskHead/remove", { id: id }).toPromise().then((response) => {
        });
    }

    addOrUpdateSubTask(SubTask: SubTaskViewModel): Promise<SubTaskViewModel> {
        return this.http.post<any>("/api/SubTask/addorupdate", SubTask).toPromise().then((response) => {
            return new SubTaskViewModel(response);
        });
    }

    removeSubTask(id): Promise<void> {
        return this.http.post<any>("/api/SubTask/remove", { id: id }).toPromise().then((response) => {
        });
    }

    changeSubTaskOrder(currentId, toSwapId): Promise<void> {
        return this.http.post<any>("/api/SubTask/changeorder", { currentId: currentId, toSwapId: toSwapId }).toPromise()
            .then((response) => {
        });
    }

    changeTaskHeadOrder(currentId, toSwapId): Promise<void> {
        return this.http.post<any>("/api/TaskHead/changeorder", { currentId: currentId, toSwapId: toSwapId }).toPromise()
            .then((response) => {
        });
    }

    shareTaskHead(model: any): Promise<string> {
        return this.http.post<any>("/api/share/shareTaskHead", model).toPromise()
            .then((response) => response.text());
    }

    shareSubTask(model: any): Promise<string> {
        return this.http.post<any>("/api/share/shareSubTask", model).toPromise()
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
            url = `/api/userTaskHead/get<any>?${params.toString()}`;
        } else {
            url = `/api/userTaskHead/get<any>`;
        }
        return this.http.get<any>(url).toPromise().then((response) => {
            return response.map(c => new UserTaskHeadModel(c));
        });
    }

    getUserSubTaskIds(userTaskHeadId: number): Promise<number[]> {
        return this.http.get<any>(`/api/userSubTask/getuserSubTaskids?userTaskHeadId=${userTaskHeadId}`).toPromise().then((response) => {
            return response;
        });
    }

    getUserSubTask(userSubTaskId: number): Promise<UserSubTaskModel> {
        return this.http.get<any>(`/api/userSubTask/getbyid?userSubTaskId=${userSubTaskId}`).toPromise().then((response) => {
            return new UserSubTaskModel(response);
        });
    }

    updateUserSubTask(model): Promise<void> {
        return this.http.post<any>("/api/userSubTask/update", model).toPromise()
            .then((response) => {
            });
    }
    
    autoSaveUserSubTask(model: UserSubTaskAutoSaveModel): Promise<void> {
        return this.http.post<any>("/api/userSubTask/autoSave", model).toPromise()
            .then((response) => {
            });
    }

    getUserTaskReports(): Promise<any[]> {
        return this.http.get<any>("/api/userSubTaskReport/getUserReports/").toPromise().then((response) => {
            return response;
        });
    }

    canOpenTaskHead(userTaskHeadId): Promise<boolean> {
        return this.http.post<any>("/api/userTaskHead/canopen/", { userTaskHeadId: userTaskHeadId })
            .toPromise().then((response) => {
                return response;
            });
    }

    canOpenSubTask(userTaskHeadId, userSubTaskId): Promise<boolean> {
        return this.http.post<any>("/api/userSubTask/canopen/", { userTaskHeadId: userTaskHeadId, userSubTaskId: userSubTaskId })
            .toPromise().then((response) => {
                return response;
            });
    }

    publishSubTask(TaskHeadId, SubTaskId): Promise<void> {
        return this.http.post<any>("/api/SubTask/publish/", { TaskHeadId: TaskHeadId, SubTaskId: SubTaskId })
            .toPromise().then(() => {
            });
    }

    getCategories(): Promise<CategoryViewModel[]> {
        return this.http.get<any>("/api/category/get<any>/").toPromise().then((response) => {
            return response;
        });
    }

    getUserTaskReport(userEmail: string) {
        return this.http.get<any>("/api/userSubTaskReport/getDetailedUserReport?userEmail=" + userEmail).toPromise().then((response) => {
            return response;
        });
    }
}
