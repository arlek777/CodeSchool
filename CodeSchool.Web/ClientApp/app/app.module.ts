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
import { BackendService } from "./services/backend.service";


@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LessonPage
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'lesson', pathMatch: 'full' },
            { path: 'lesson', component: LessonPage },
            { path: '**', redirectTo: 'lesson' }
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
