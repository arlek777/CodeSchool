export class LessonTestResult {
    constructor(model?: any) {
        if (model) {
            this.tips = model.tips;
            this.isSucceeded = model.isSucceeded;
        }
    }

    tips: string[] = [];
    isSucceeded: boolean;
}