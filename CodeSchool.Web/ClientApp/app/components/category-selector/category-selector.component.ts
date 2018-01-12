import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { BackendService } from "../../services/backend.service";
import { CategoryViewModel } from '../../models/category';

@Component({
    selector: "category-selector",
    templateUrl: './category-selector.component.html'
})
export class CategorySelectorComponent implements OnInit, OnChanges  {
    private selectedCategoryId: number;

    @Output()
    onCategoryChanged: EventEmitter<number> = new EventEmitter<number>();

    @Input("local-storage-key")
    localStorageKey: string;

    categories: CategoryViewModel[] = [];

    constructor(private backendService: BackendService) {
    }

    ngOnInit(): void {
        this.backendService.getCategories().then(categories => {
            this.categories = categories;
            this.categoryChanged(this.getSavedSelectedCategoryId());
        });
    }

    ngOnChanges(changes: SimpleChanges): void {
        var localStorageKeyChange = changes["localStorageKey"];
        if (localStorageKeyChange &&
            localStorageKeyChange.currentValue !== localStorageKeyChange.previousValue &&
            !localStorageKeyChange.isFirstChange()) {
            this.categoryChanged(this.getSavedSelectedCategoryId());
        }
    }

    categoryChanged(categoryId) {
        this.selectedCategoryId = categoryId;
        this.saveSelectedCategoryId();

        this.onCategoryChanged.emit(categoryId);
    }

    private saveSelectedCategoryId() {
        localStorage[this.localStorageKey] = this.selectedCategoryId;
    }

    private getSavedSelectedCategoryId(): number {
        var savedCategoryId = localStorage[this.localStorageKey];
        if (!savedCategoryId) {
            localStorage[this.localStorageKey] = this.categories[0].id;
            savedCategoryId = this.categories[0].id;
        }

        return parseInt(savedCategoryId);
    }
}
