export class Constants {
    static defaultLessonReporter = ` 
var myReporter = {
    specDone: function (result) {
        window.parent.resultsReceived(result);
        window.location.reload();
    }
};

jasmine.getEnv().clearReporters();
jasmine.getEnv().addReporter(myReporter);`;

    static startUnitTest = `
describe("A suite", function() {
it("contains spec with an expectation", function() {
    expect(true).toBe(true);
  });
});`;
}