describe("1 Lesson Tests", function () {
    it("test", function () {
        spyOn(window, 'alert');

        test();

        expect(window.alert).toHaveBeenCalledWith("Hello World");
    });
});

window.runJasmine();