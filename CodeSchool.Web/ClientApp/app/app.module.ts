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
import { HomeComponent } from './components/home/home.component';

import { LessonPage } from './pages/lesson/lesson.page';
import { ChaptersPage } from './pages/chapters/chapters.page';
import { AdminLessonPage } from './pages/adminLesson/admin-lesson.page';
import { AdminChaptersPage } from './pages/adminChapters/admin-chapters.page';
import { LoginPage } from "./pages/login/login.page";
import { RegisterPage } from "./pages/register/register.page";

import { BackendService } from "./services/backend.service";
import { PopupService } from "./services/popup.service";
import { LessonTesterDirective } from "./directives/lesson-tester.directive";
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
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LessonTesterDirective,
        LessonPage,
        ChaptersPage,
        AdminLessonPage,
        AdminChaptersPage,
        LoginPage,
        RegisterPage
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'chapters', pathMatch: 'full' },
            { path: 'home', redirectTo: 'chapters' },
            { path: 'login', component: LoginPage },
            { path: 'register', component: RegisterPage },
            { path: 'chapters', component: ChaptersPage, canActivate: [AuthGuard] },
            { path: 'lesson/:chapterId/:id', component: LessonPage, canActivate: [AuthGuard] },
            { path: 'adminchapters', component: AdminChaptersPage, canActivate: [AdminAuthGuard] },
            { path: 'adminlesson/:chapterId/:id', component: AdminLessonPage, canActivate: [AdminAuthGuard] },
            { path: 'adminlesson/:chapterId', component: AdminLessonPage, canActivate: [AdminAuthGuard] },
            { path: '**', redirectTo: 'chapters' }
        ]),
        AceEditorModule,
        BrowserAnimationsModule,
        ToastModule.forRoot(),
        TinymceModule.withConfig({})
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
