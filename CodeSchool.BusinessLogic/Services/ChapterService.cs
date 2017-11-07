using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IGenericRepository _repository;

        public ChapterService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Chapter>> GetChapters()
        {
            var chapters = (await _repository.GetAll<Chapter>())
                .Where(c => c.IsRemoved)
                .OrderBy(c => c.Order).ToList();

            chapters.ForEach(c => c.Lessons = c.Lessons
                .Where(l => !l.IsRemoved)
                .OrderBy(l => l.Order).ToList());

            return chapters;
        }

        public async Task Remove(int id)
        {
            var chapter = await _repository.Find<Chapter>(c => c.Id == id && !c.IsRemoved);
            chapter.IsRemoved = true;
            chapter.Lessons
            await _repository.SaveChanges();
        }

        public async Task<Chapter> AddOrUpdate(Chapter model)
        {
            var chapter = await GetById(model.Id);
            if (chapter == null)
            {
                model.Order = await GetNextOrder();
                _repository.Add(model);
                chapter = model;
            }
            else
            {
                chapter.Title = model.Title;
            }
            await _repository.SaveChanges();
            return chapter;
        }

        public async Task<Chapter> GetById(int chapterId)
        {
            var chapter = await _repository.Find<Chapter>(c => c.Id == chapterId);
            chapter.Lessons = chapter.Lessons.OrderBy(l => l.Order).ToList();
            return chapter;
        }

        public async Task ChangeOrder(int currentChapterId, int toSwapChapterId)
        {
            var currentChapter = await GetById(currentChapterId);
            var toSwapChapter = await GetById(toSwapChapterId);

            var currentOrder = currentChapter.Order;
            currentChapter.Order = toSwapChapter.Order;
            toSwapChapter.Order = currentOrder;

            await _repository.SaveChanges();
        }

        private async Task<int> GetNextOrder()
        {
            var chapters = await GetChapters();
            var lastChapter = chapters.LastOrDefault();
            return lastChapter?.Order + 1 ?? 0;
        }
    }
}
