using System.Data.Entity;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess
{
    public class CodeSchoolDbContext: DbContext
    {
        public CodeSchoolDbContext(string connString): base(connString)
        {
        }

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
    }
}
