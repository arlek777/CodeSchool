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
            var chapters = await _dbContext.Set<Chapter>()
                .OrderBy(c => c.Order)
                .ToListAsync();

            var result = chapters.Select(c => new Chapter()
            {
                Id = c.Id,
                Title = c.Title,
                Order = c.Order,
                Lessons = c.Lessons.Select(l => new Lesson()
                {
                    Id = l.Id,
                    ChapterId = l.ChapterId,
                    Title = l.Title,
                    Order = l.Order
                }).OrderBy(l => l.Order).ToList()
            });

            return result;
        }

        public async Task Remove(int id)
        {
            var chapter = await _dbContext.Set<Chapter>().FirstOrDefaultAsync(c => c.Id == id);
            _dbContext.Set<Chapter>().Remove(chapter);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Chapter> AddOrUpdate(Chapter model)
        {
            var chapter = await _dbContext.Set<Chapter>().FirstOrDefaultAsync(c => c.Id == model.Id);
            if (chapter == null)
            {
                var nextOrder = await this.GetNextOrder();
                model.Order = nextOrder;
                chapter = _dbContext.Set<Chapter>().Add(model);
            }
            else
            {
                chapter.Title = model.Title;
            }

            await _dbContext.SaveChangesAsync();
            return chapter;
        }

        public async Task<Chapter> Get(int chapterId)
        {
            var chapter = await _dbContext.Set<Chapter>().FirstOrDefaultAsync(c => c.Id == chapterId);
            return chapter;
        }

        public async Task ChangeOrder(int currentChapterId, int toSwapChapterId)
        {
            var currentChapter = await Get(currentChapterId);
            var toSwapChapter = await Get(toSwapChapterId);

            var currentOrder = currentChapter.Order;
            currentChapter.Order = toSwapChapter.Order;
            toSwapChapter.Order = currentOrder;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Chapter> GetNext(int chapterId)
        {
            var chapters = await _dbContext.Set<Chapter>()
                .OrderBy(c => c.Order).ToListAsync();

            var chapterIndex = chapters.FindIndex(c => c.Id == chapterId);

            var nextIndex = ++chapterIndex;
            return nextIndex == chapters.Count 
                ? chapters[chapterIndex] 
                : chapters[nextIndex];
        }

        private async Task<int> GetNextOrder()
        {
            var lastChapter = (await _dbContext.Set<Chapter>()
                .OrderBy(c => c.Order).ToListAsync()).LastOrDefault();

            return lastChapter == null ? 0 : ++lastChapter.Order;
        }
    }
}
