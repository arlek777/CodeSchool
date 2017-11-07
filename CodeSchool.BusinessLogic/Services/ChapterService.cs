using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Chapter>> Get()
        {
            return (await _repository.Where<Chapter>(c => !c.IsRemoved && c.Lessons.All(l => !l.IsRemoved)));
        }

        public async Task<IEnumerable<Chapter>> Get(Func<Chapter, bool> predicate)
        {
            return (await _repository.Where<Chapter>(c => !c.IsRemoved && c.Lessons.All(l => !l.IsRemoved)))
                .Where(predicate);
        }

        public async Task<IEnumerable<Chapter>> GetOrdered()
        {
            var chapters = await Get();
            var ordererdChapters = chapters.OrderBy(c => c.Order).ToList();

            ordererdChapters.ForEach(c =>
            {
                c.Lessons = c.Lessons.OrderBy(l => l.Order).ToList();
            });

            return ordererdChapters;
        }

        //public async Task<IEnumerable<Chapter>> GetChapters(Expression<Func<Chapter, bool>> predicate = null)
        //{
        //    var chapters = predicate != null
        //        ? (await _repository.Where<Chapter>(predicate)).Where(c => !c.IsRemoved)
        //        : await _repository.Where<Chapter>(c => !c.IsRemoved);

        //    var ordererdChapters = chapters.OrderBy(c => c.Order).ToList();

        //    ordererdChapters.ForEach(c =>
        //    {
        //        c.Lessons = c.Lessons
        //            .Where(l => !l.IsRemoved)
        //            .OrderBy(l => l.Order).ToList();
        //    });

        //    return ordererdChapters;
        //}

        public async Task<Chapter> GetById(int chapterId)
        {
            var chapter = (await Get(c => c.Id == chapterId)).FirstOrDefault();
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
            }
            await _repository.SaveChanges();
            return chapter;
        }

        public async Task Remove(int id)
        {
            var chapter = await GetById(id);
            chapter.IsRemoved = true;
            await _repository.SaveChanges();

            foreach (var lesson in chapter.Lessons)
            {
                lesson.IsRemoved = true;
            }
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
            var chapters = await GetOrdered();
            var lastChapter = chapters.LastOrDefault();
            return lastChapter?.Order + 1 ?? 0;
        }
    }
}
