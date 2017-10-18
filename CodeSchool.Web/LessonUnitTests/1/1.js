describe("1 Lesson Tests", function () {
    it("should show alert Test", function () {
        spyOn(window, 'alert');
        expect(window.alert).toHaveBeenCalledWith('Test');
    });
});

window.runJasmine();