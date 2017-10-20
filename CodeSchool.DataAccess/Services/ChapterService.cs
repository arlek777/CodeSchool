using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess.Services
{
    public class ChapterService : IChapterService
    {
        private readonly DbContext _dbContext;

        public ChapterService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Chapter>> GetShortcutChapters()
        {
            var chapters = await _dbContext.Set<Chapter>().ToListAsync();

            var result = chapters.Select(c => new Chapter()
            {
                Id = c.Id,
                Title = c.Title,
                Lessons = c.Lessons.Select(l => new Lesson()
                {
                    Id = l.Id,
                    ChapterId = l.ChapterId,
                    Title = l.Title
                }).ToList()
            });

            return result;
        }
    }
}
