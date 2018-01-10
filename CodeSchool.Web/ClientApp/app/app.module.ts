import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, XHRBackend, RequestOptions, Http } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AceEditorModule } from 'ng2-ace-editor';
import { ToastModule, ToastOptions } from 'ng2-toastr/ng2-toastr';
import { TinymceModule } from 'angular2-tinymce';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { EditorComponent } from "./components/editor/editor.component";
import { HeaderTemplateDirective } from "./components/editor/editor.component";
import { UserLessonCodeComponent } from './components/user-lesson-code/user-lesson-code.component';
import { UserLessonTestComponent } from './components/user-lesson-test/user-lesson-test.component';

import { UserLessonPage } from './pages/user-lesson/user-lesson.page';
import { UserCodeChaptersPage } from './pages/user-code-chapters/user-code-chapters.page';
import { UserTestChaptersPage } from './pages/user-test-chapters/user-test-chapters.page';
import { AdminLessonPage } from './pages/admin-lesson/admin-lesson.page';
import { AdminChaptersPage } from './pages/admin-chapters/admin-chapters.page';
import { LoginPage } from "./pages/login/login.page";
import { RegisterPage } from "./pages/register/register.page";
import { LiteraturePage } from "./pages/literature/literature.page";
import { UserStatisticPage } from "./pages/user-statistic/user-statistic.page";

import { ToggleMobileNavbarDirective } from "./directives/toggle-mobile-navbar.directive";
import { LessonTesterDirective } from "./directives/lesson-tester.directive";
import { TrustHtmlDirective } from "./directives/trust-html.directive";

import { BackendService } from "./services/backend.service";
import { PopupService } from "./services/popup.service";

import { AuthService } from "./services/auth.service";
import { AdminAuthGuard, AuthGuard } from "./services/auth-guard.service";
import { GlobalErrorHandler } from "./services/global-error-handler.service";
import { InterceptedHttp } from "./http.interceptor";


export class CustomToastOption extends ToastOptions {
    maxShown = 1;
    toastLife = 3000;
    showCloseButton = true;
}

function httpFactory(xhrBackend: XHRBackend, requestOptions: RequestOptions): Http {
    return new InterceptedHttp(xhrBackend, requestOptions);
}

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        HeaderTemplateDirective,
        AppComponent,
        NavMenuComponent,
        EditorComponent,
        UserLessonCodeComponent,
        UserLessonTestComponent,
        LessonTesterDirective,
        ToggleMobileNavbarDirective,
        TrustHtmlDirective,
        UserLessonPage,
        UserCodeChaptersPage,
        UserTestChaptersPage,
        AdminLessonPage,
        AdminChaptersPage,
        LoginPage,
        RegisterPage,
        LiteraturePage,
        UserStatisticPage
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'code-chapters', pathMatch: 'full' },
            { path: 'home', redirectTo: 'code-chapters' },
            { path: 'login', component: LoginPage },
            { path: 'register', component: RegisterPage },
            { path: 'literature', component: LiteraturePage },
            { path: 'code-chapters', component: UserCodeChaptersPage, canActivate: [AuthGuard] },
            { path: 'test-chapters', component: UserTestChaptersPage, canActivate: [AuthGuard] },
            { path: 'user-lesson/:userChapterId/:userLessonId', component: UserLessonPage, canActivate: [AuthGuard] },
            { path: 'admin-chapters', component: AdminChaptersPage, canActivate: [AdminAuthGuard] },
            { path: 'admin-lesson/:chapterId/:lessonId', component: AdminLessonPage, canActivate: [AdminAuthGuard] },
            { path: 'admin-lesson/:chapterId', component: AdminLessonPage, canActivate: [AdminAuthGuard] },
            { path: 'users-tatistic', component: UserStatisticPage, canActivate: [AdminAuthGuard] },
            { path: '**', redirectTo: 'code-chapters' }
        ]),
        AceEditorModule,
        BrowserAnimationsModule,
        ToastModule.forRoot(),
        TinymceModule.withConfig({
            browser_spellcheck: true
        })
    ],
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        { provide: ToastOptions, useClass: CustomToastOption },
        {
            provide: Http,
            useFactory: httpFactory,
            deps: [XHRBackend, RequestOptions]
        },
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandler
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
