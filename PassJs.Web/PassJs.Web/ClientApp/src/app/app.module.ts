import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CountdownTimerModule } from 'angular-countdown-timer';
import { ToastaModule } from 'ngx-toasta';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { EditorComponent } from "./components/editor/editor.component";
import { HeaderTemplateDirective } from "./components/editor/editor.component";
import { UserSubTaskCodeComponent } from './components/user-sub-task-code/user-sub-task-code.component';
import { UserSubTaskTestComponent } from './components/user-sub-task-test/user-sub-task-test.component';
import { CategorySelectorComponent } from './components/category-selector/category-selector.component';

import { UserSubTaskPage } from './pages/user-task/user-task.page';
import { UserTaskHeadsPage } from './pages/user-task-heads/user-task-heads.page';
import { AdminSubTaskPage } from './pages/admin-task/admin-task.page';
import { AdminTaskHeadsPage } from './pages/admin-task-heads/admin-task-heads.page';
import { LoginPage } from "./pages/login/login.page";
import { RegisterPage } from "./pages/register/register.page";
import { UserReportsPage } from "./pages/user-reports/user-reports.page";
import { UserTaskReportPage } from "./pages/user-task-report/user-task-report.page";
import { SharePage } from "./pages/share/share.page";
import { InvitationPage } from "./pages/invitation/invitation.page";
import { HomePage } from "./pages/home/home.page";

import { ToggleMobileNavbarDirective } from "./directives/toggle-mobile-navbar.directive";
import { SubTaskTesterDirective } from "./directives/task-tester.directive";
import { TrustHtmlDirective } from "./directives/trust-html.directive";
import { SpinnerDirective } from "./directives/spinner.directive";

import { BackendService } from "./services/backend.service";
import { PopupService } from "./services/popup.service";

import { AuthService } from "./services/auth.service";
import { AdminAuthGuard, AuthGuard } from "./services/auth-guard.service";
import { AppErrorHandler } from "./app-error.handler";
import { TokenInterceptor } from "./http.interceptor";


const DEFAULT_ROUTE: string = "home";

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        HeaderTemplateDirective,
        AppComponent,
        NavMenuComponent,
        EditorComponent,
        UserSubTaskCodeComponent,
        UserSubTaskTestComponent,
        CategorySelectorComponent,
        SubTaskTesterDirective,
        ToggleMobileNavbarDirective,
        TrustHtmlDirective,
        SpinnerDirective,
        UserSubTaskPage,
        UserTaskHeadsPage,
        AdminSubTaskPage,
        AdminTaskHeadsPage,
        LoginPage,
        RegisterPage,
        UserReportsPage,
        SharePage,
        InvitationPage,
        HomePage,
        UserTaskReportPage
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot([
            { path: '', redirectTo: DEFAULT_ROUTE, pathMatch: 'full' },
            { path: 'home', component: HomePage },
            { path: 'login', component: LoginPage },
            { path: 'register', component: RegisterPage },
            //{ path: 'user-TaskHeads/:TaskHeadType', component: UserTaskHeadsPage, canActivate: [AuthGuard] },
            { path: 'invitation/:token', component: InvitationPage },
            { path: 'task/:userTaskHeadId/:userSubTaskId', component: UserSubTaskPage, canActivate: [AuthGuard] },
            { path: 'admin-task-heads', component: AdminTaskHeadsPage, canActivate: [AdminAuthGuard] },
            { path: 'admin-sub-task/:taskHeadId/:subTaskId', component: AdminSubTaskPage, canActivate: [AdminAuthGuard] },
            { path: 'admin-sub-task/:taskHeadId', component: AdminSubTaskPage, canActivate: [AdminAuthGuard] },
            { path: 'user-task-reports', component: UserReportsPage, canActivate: [AdminAuthGuard] },
            { path: 'user-task-report/:userEmail', component: UserTaskReportPage, canActivate: [AdminAuthGuard] },
            { path: 'share/:taskHeadId', component: SharePage, canActivate: [AdminAuthGuard] },
            { path: 'share/:taskHeadId/:subTaskId', component: SharePage, canActivate: [AdminAuthGuard] },
            { path: '**', redirectTo: DEFAULT_ROUTE }
        ]),
        BrowserAnimationsModule,
        ToastaModule.forRoot(),
        CountdownTimerModule
    ],
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: TokenInterceptor,
          multi: true
        },
        {
            provide: ErrorHandler,
            useClass: AppErrorHandler
        },
        BackendService,
        PopupService,
        AuthService,
        AuthGuard,
        AdminAuthGuard
    ]
})
export class AppModule {
}
