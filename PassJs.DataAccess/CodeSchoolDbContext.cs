using Microsoft.EntityFrameworkCore;
using PassJs.DomainModels;

namespace PassJs.DataAccess
{
    public class CodeSchoolDbContext: DbContext
    {
        private readonly string _connStr;

        public CodeSchoolDbContext(string connStr)
        {
            _connStr = connStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connStr);
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
