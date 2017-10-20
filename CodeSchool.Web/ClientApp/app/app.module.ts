import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AceEditorModule } from 'ng2-ace-editor';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';

import { LessonPage } from './pages/lesson/lesson.page';
import { ChaptersPage } from './pages/chapters/chapters.page';
import { BackendService } from "./services/backend.service";


@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LessonPage,
        ChaptersPage
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'chapters', pathMatch: 'full' },
            { path: 'chapters', component: ChaptersPage },
            { path: 'lesson/:chapterId/:id', component: LessonPage },
            { path: '**', redirectTo: 'chapters' }
        ]),
        AceEditorModule
    ],
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        BackendService
    ]
})
export class AppModule {
}
