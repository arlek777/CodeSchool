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

    static startLessonReporter = `
var myReporter = {
    specDone: function (result) {
        var testResult = {
            messages: [],
            isSucceeded: false
        };
        
        var fails = result.failedExpectations;
        if(result && fails.length > 0){
            for(var i=0;i<fails.length; i++){
                var message = fails[i].message;
                
            }
        }
        else{
            testResult.isSucceeded = true;
        }
        
        window.parent.resultsReceived(testResult);
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

    static accessTokenKey = "accessToken";
    static currentUserKey = "user";
    static userIdKey = "userId";
}