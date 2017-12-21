using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CodeSchool.Domain;
using CodeSchool.Domain.Lessons;
using CodeSchool.Domain.Tests;

namespace CodeSchool.DataAccess
{
    public class CodeSchoolDbContext: DbContext
    {
        static CodeSchoolDbContext()
        {
            //Database.SetInitializer(new DbInitializer());
        }

        public CodeSchoolDbContext(string connString): base(connString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserChapter> UserChapters{ get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TestTheme> TestThemes { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestQuestionOption> TestQuestionOptions { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
