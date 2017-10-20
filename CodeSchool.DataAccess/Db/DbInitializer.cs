using System.Collections.Generic;
using System.Data.Entity;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess.Db
{
    public class DbInitializer: DropCreateDatabaseIfModelChanges<CodeSchoolDbContext>
    {
        protected override void Seed(CodeSchoolDbContext context)
        {
            var lessons1 = new List<Lesson>()
            {
                new Lesson()
                {
                    Title = "Test 1",
                    StartCode = "alert('hello')",
                    Text = "This is a test lesson",
                    UnitTestsCode = @"describe('1 Lesson Tests', function () {
                    it('test', function () {
                    spyOn(window, 'alert');

                    test();

                    expect(window.alert).toHaveBeenCalledWith('Hello World');
                });
            });

            window.runJasmine();",
                    ReporterCode = @"var myReporter = {
                    specDone: function(result) {
                    window.parent.resultsReceived(result);
                    window.location.reload();
                }
            };

            jasmine.getEnv().clearReporters();
            jasmine.getEnv().addReporter(myReporter);"
                },
                new Lesson()
                {
                    Title = "Test 2",
                    Text = "This is a test lesson",
                    ReporterCode = "fdf",
                    UnitTestsCode = "dfdf",
                    StartCode = "dsfdf"
                }
            };

            var chapters = new List<Chapter>()
            {
                new Chapter() { Title = "Chapter 1"},
                new Chapter() { Title = "Chapter 2"}
            };

            foreach (var chapter in chapters)
            {
                var chapterAdded = context.Set<Chapter>().Add(chapter);
                context.SaveChanges();

                foreach (var lesson in lessons1)
                {
                    chapterAdded.Lessons.Add(lesson);
                    context.SaveChanges();
                }
            }

            base.Seed(context);
        }
    }
}
