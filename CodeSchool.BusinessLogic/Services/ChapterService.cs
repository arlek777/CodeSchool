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

        public async Task<IEnumerable<Chapter>> GetChapters(int? categoryId = null)
        {
            List<Chapter> chapters;
            if (categoryId.HasValue)
            {
                chapters = (await _repository.Where<Chapter>(c => c.CategoryId == categoryId))
                    .OrderBy(c => c.Order)
                    .ToList();
            }
            else
            {
                chapters = (await _repository.GetAll<Chapter>())
                    .OrderBy(c => c.Order)
                    .ToList();
            }

            chapters.ForEach(c =>
            {
                c.Lessons = c.Lessons.OrderBy(l => l.Order).ToList();
            });

            return chapters;
        }

        public async Task<Chapter> GetById(int chapterId)
        {
            var chapter = (await GetChapters()).FirstOrDefault(c => c.Id == chapterId);
            return chapter;
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
                chapter.CategoryId = model.CategoryId;
            }
            await _repository.SaveChanges();
            return chapter;
        }

        public async Task Remove(int id)
        {
            var chapter = await _repository.Find<Chapter>(c => c.Id == id);
            _repository.Remove(chapter);
            await _repository.SaveChanges();
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
