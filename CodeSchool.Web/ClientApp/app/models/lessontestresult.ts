export class LessonTestResult {
    constructor(model?: any) {
        if (model) {
            this.messages = model.messages;
            this.isSucceeded = model.isSucceeded;
            this.isException = model.isException;
        }
    }

    messages: string[] = [];
    isSucceeded: boolean;
    isException?: boolean;
}