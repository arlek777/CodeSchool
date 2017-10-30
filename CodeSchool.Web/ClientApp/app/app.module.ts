import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
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

import { BackendService } from "./services/backend.service";
import { PopupService } from "./services/popup.service";
import { LessonTesterDirective } from "./directives/lesson-tester.directive";

export class CustomToastOption extends ToastOptions {
    maxShown = 1;
    toastLife = 3000;
    showCloseButton = true;
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
        AdminChaptersPage
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'chapters', pathMatch: 'full' },
            { path: 'chapters', component: ChaptersPage },
            { path: 'lesson/:chapterId/:id', component: LessonPage },
            { path: 'adminchapters', component: AdminChaptersPage },
            { path: 'adminlesson/:chapterId/:id', component: AdminLessonPage },
            { path: 'adminlesson/:chapterId', component: AdminLessonPage },
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
        BackendService,
        PopupService
    ]
})
export class AppModule {
}
