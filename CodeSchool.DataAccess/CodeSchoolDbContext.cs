﻿using System.Data.Entity;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess
{
    public class CodeSchoolDbContext: DbContext
    {
        static CodeSchoolDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public CodeSchoolDbContext(string connString): base(connString)
        {
        }

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserChapterProgress> UserChapterProgresses { get; set; }
        public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }
    }
}
