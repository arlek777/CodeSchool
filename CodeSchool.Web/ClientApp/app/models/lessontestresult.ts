export class LessonTestResult {
    constructor(model?: any) {
        if (model) {
            this.messages = model.messages;
            this.isSucceeded = model.isSucceeded;
        }
    }

    messages: string[] = [];
    isSucceeded: boolean;
}