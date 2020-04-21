using System;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class LessonService : ILessonService
    {
        private readonly IGenericRepository _repository;
        private readonly IAnswerLessonOptionService _answerLessonOptionService;
        private readonly IChapterService _chapterService;

        public LessonService(IGenericRepository repository, IAnswerLessonOptionService answerLessonOptionService,
            IChapterService chapterService)
        {
            _repository = repository;
            _answerLessonOptionService = answerLessonOptionService;
            _chapterService = chapterService;
        }

        public async Task<Lesson> GetById(Guid companyId, int id)
        {
            return await _repository.Find<Lesson>(l => l.Id == id && l.CompanyId == companyId);
        }

        public async Task<Lesson> AddOrUpdate(Lesson model)
        {
            var dbLesson = await GetById(model.CompanyId, model.Id);
            if (dbLesson == null)
            {
                var chapter = await _repository.Find<Chapter>(c => c.Id == model.ChapterId && c.CompanyId == model.CompanyId);
                if (!chapter.Lessons.Any())
                {
                    chapter.Type = model.Type == LessonType.Code ? ChapterType.Code : ChapterType.Test;
                }

                dbLesson = new Lesson()
                {
                    CompanyId = model.CompanyId,
                    Type = model.Type,
                    ChapterId = model.ChapterId,
                    Published = model.Published
                };

                await FillDbLessonForType(model, dbLesson);
                dbLesson.Order = await GetNextOrder(model.CompanyId, model.ChapterId);
                _repository.Add(dbLesson);
            }
            else
            {
                await FillDbLessonForType(model, dbLesson);
            }
           
            await _repository.SaveChanges();
            return dbLesson;
        }

        public async Task ChangeOrder(Guid companyId, int currentLessonId, int toSwapLessonId)
        {
            var currentLesson = await GetById(companyId, currentLessonId);
            var toSwapLesson = await GetById(companyId, toSwapLessonId);

            var toSwapOrder = toSwapLesson.Order;
            toSwapLesson.Order = currentLesson.Order;
            currentLesson.Order = toSwapOrder;

            await _repository.SaveChanges();
        }

        public async Task Remove(Guid companyId, int id)
        {
            var lesson = await GetById(companyId, id);
            _repository.Remove(lesson);
            await _repository.SaveChanges();
        }

        private async Task FillDbLessonForType(Lesson model, Lesson dbLesson)
        {
            dbLesson.Answer = model.Answer;
            dbLesson.TaskText = model.TaskText;
            dbLesson.Text = model.Text;
            dbLesson.Title = model.Title;
            dbLesson.Level = model.Level;
            dbLesson.CompanyId = model.CompanyId;

            if (model.Type == LessonType.Test)
            {
                await _answerLessonOptionService.UpdateOptions(dbLesson, model.AnswerLessonOptions);
            }
            else if (model.Type == LessonType.Code)
            {
                dbLesson.ReporterCode = model.ReporterCode;
                dbLesson.UnitTestsCode = model.UnitTestsCode;
            }
        }

        private async Task<int> GetNextOrder(Guid companyId, int chapterId)
        {
            var chapter = await _chapterService.GetById(companyId, chapterId);
            var lastLesson = chapter.Lessons.LastOrDefault();
            return lastLesson?.Order + 1 ?? 0;
        }
    }
}