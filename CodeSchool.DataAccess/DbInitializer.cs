using System.Collections.Generic;
using System.Data.Entity;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess
{
    public class DbInitializer: DropCreateDatabaseIfModelChanges<CodeSchoolDbContext>
    {
        protected override void Seed(CodeSchoolDbContext context)
        {
            //var SubTasks1 = new List<SubTask>()
            //{
            //    new SubTask()
            //    {
            //        Title = "Test 1",
            //        StartCode = "alert('hello')",
            //        Text = "This is a test SubTask",
            //        UnitTestsCode = @"describe('1 SubTask Tests', function () {
            //        it('test', function () {
            //        spyOn(window, 'alert');

            //        test();

            //        expect(window.alert).toHaveBeenCalledWith('Hello World');
            //    });
            //});

            //window.runJasmine();",
            //        ReporterCode = @"var myReporter = {
            //        specDone: function(result) {
            //        window.parent.resultsReceived(result);
            //        window.location.reload();
            //    }
            //};

            //jasmine.getEnv().clearReporters();
            //jasmine.getEnv().addReporter(myReporter);"
            //    },
            //    new SubTask()
            //    {
            //        Title = "Test 2",
            //        Text = "This is a test SubTask",
            //        ReporterCode = "fdf",
            //        UnitTestsCode = "dfdf",
            //        StartCode = "dsfdf"
            //    }
            //};

            //var TaskHeads = new List<TaskHead>()
            //{
            //    new TaskHead() { Title = "TaskHead 1"},
            //    new TaskHead() { Title = "TaskHead 2"}
            //};

            //foreach (var TaskHead in TaskHeads)
            //{
            //    var TaskHeadAdded = context.Set<TaskHead>().Add(TaskHead);
            //    context.SaveChanges();

            //    foreach (var SubTask in SubTasks1)
            //    {
            //        TaskHeadAdded.SubTasks.Add(SubTask);
            //        context.SaveChanges();
            //    }
            //}

            base.Seed(context);
        }
    }
}
