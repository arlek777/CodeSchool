using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CodeSchool.Domain;

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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CodeSnapshot> CodeSnapshots { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<TaskHead> TaskHeads { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTaskHead> UserTaskHeads{ get; set; }
        public DbSet<UserSubTask> UserSubTasks { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AnswerSubTaskOption> AnswerSubTaskOptions { get; set; }
    }
}
