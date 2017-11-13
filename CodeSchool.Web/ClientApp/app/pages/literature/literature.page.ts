import { Component } from '@angular/core';

@Component({
    templateUrl: './literature.page.html'
})
export class LiteraturePage {
    literatures: [{ title: string, link: string }];

    constructor() {
        this.literatures = [{
            title: 'JavaScript.RU',
            link: 'https://learn.javascript.ru/'
        }, {
            title: 'Веб-учебник JavaScript',
            link: 'http://theory.phphtml.net/books/javascript/'
        }, {
            title: 'Книга JavaScript. Подробное руководство',
            link: 'http://www.ozon.ru/context/detail/id/19677670/?partner=iliakan'
        }, {
            title: 'Доп. литература',
            link: 'https://learn.javascript.ru/books'
        }];
    }
}
