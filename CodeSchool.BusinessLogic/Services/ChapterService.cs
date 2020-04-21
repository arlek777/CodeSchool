using System;
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

        public async Task<IEnumerable<Chapter>> GetChapters(Guid companyId, int? categoryId = null)
        {
            List<Chapter> chapters;
            if (categoryId.HasValue)
            {
                chapters = (await _repository.Where<Chapter>(c => c.CategoryId == categoryId && c.CompanyId == companyId))
                    .OrderBy(c => c.Order)
                    .ToList();
            }
            else
            {
                chapters = (await _repository.GetAll<Chapter>()).Where(c => c.CompanyId == companyId)
                    .OrderBy(c => c.Order)
                    .ToList();
            }

            chapters.ForEach(c =>
            {
                c.Lessons = c.Lessons.OrderBy(l => l.Order).ToList();
            });

            return chapters;
        }

        public async Task<Chapter> GetById(Guid companyId, int chapterId)
        {
            var chapter = (await GetChapters(companyId)).FirstOrDefault(c => c.Id == chapterId);
            return chapter;
        }

        public async Task<Chapter> AddOrUpdate(Chapter model)
        {
            var chapter = await GetById(model.CompanyId, model.Id);
            if (chapter == null)
            {
                model.Order = await GetNextOrder(model.CompanyId);
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

        public async Task Remove(Guid companyId, int id)
        {
            var chapter = await _repository.Find<Chapter>(c => c.Id == id && c.CompanyId == companyId);
            _repository.Remove(chapter);
            await _repository.SaveChanges();
        }

        public async Task ChangeOrder(Guid companyId, int currentChapterId, int toSwapChapterId)
        {
            var currentChapter = await GetById(companyId, currentChapterId);
            var toSwapChapter = await GetById(companyId, toSwapChapterId);

            var currentOrder = currentChapter.Order;
            currentChapter.Order = toSwapChapter.Order;
            toSwapChapter.Order = currentOrder;

            await _repository.SaveChanges();
        }

        private async Task<int> GetNextOrder(Guid companyId)
        {
            var chapters = await GetChapters(companyId);
            var lastChapter = chapters.LastOrDefault();
            return lastChapter?.Order + 1 ?? 0;
        }
    }
}
