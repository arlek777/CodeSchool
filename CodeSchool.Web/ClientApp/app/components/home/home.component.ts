import { Component } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    text: string = "";
    options: any = { printMargin: false };

    private testFn = `function test() { return 1; }`;
    private test = `

  describe("A suite is just a function", function() {
  it("and so is a spec", function() {
    expect(test()).toBe(2);
  });
});

window.runJasmine();
setTimeout(function () { window.location.reload(); }, 100);

`;

    handle() {
        (<any>window).resultsReceived = function(result) {
            console.log(result);
        }

        var iframe = <any>document.getElementById("testCode");

        var code = iframe.contentDocument.createElement("script");
        code.innerHTML = this.testFn;
        var testEl = iframe.contentDocument.createElement("script");
        testEl.innerHTML = this.test;

        iframe.contentDocument.body.appendChild(code);
        iframe.contentDocument.body.appendChild(testEl);
    }
}
