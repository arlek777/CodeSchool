export class LessonTestResult {
    constructor(model?: any) {
        if (model) {
            this.messages = model.messages;
            this.isPassed = model.isPassed;
        }
    }

    messages: string[] = [];
    isPassed: boolean;
}