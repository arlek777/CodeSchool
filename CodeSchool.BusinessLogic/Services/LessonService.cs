using System.Collections.Generic;
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
        private readonly IChapterService _chapterService;

        public LessonService(IGenericRepository repository, IChapterService chapterService)
        {
            _repository = repository;
            _chapterService = chapterService;
        }

        public async Task<Lesson> GetById(int id)
        {
            return await _repository.Find<Lesson>(l => l.Id == id);
        }

        public async Task<Lesson> AddOrUpdate(Lesson model)
        {
            var dbLesson = await GetById(model.Id);
            if (dbLesson == null)
            {
                dbLesson = new Lesson()
                {
                    Type = model.Type,
                    ChapterId = model.ChapterId,
                    Published = model.Published
                };

                await FillDbLessonForType(model, dbLesson);
                dbLesson.Order = await GetNextOrder(model.ChapterId);
                _repository.Add(dbLesson);
            }
            else
            {
                await FillDbLessonForType(model, dbLesson);
            }
           
            await _repository.SaveChanges();
            return dbLesson;
        }

        public async Task ChangeOrder(int currentLessonId, int toSwapLessonId)
        {
            var currentLesson = await GetById(currentLessonId);
            var toSwapLesson = await GetById(toSwapLessonId);

            var toSwapOrder = toSwapLesson.Order;
            toSwapLesson.Order = currentLesson.Order;
            currentLesson.Order = toSwapOrder;

            await _repository.SaveChanges();
        }

        public async Task Remove(int id)
        {
            var lesson = await GetById(id);
            var answerOptions = lesson.AnswerLessonOptions.ToList();
            for (int i = 0; i < answerOptions.Count; i++)
            {
                _repository.Remove(answerOptions[i]);
                await _repository.SaveChanges();
            }
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

            if (model.Type == LessonType.Test)
            {
                await UpdateAnswerLessonModelOption(dbLesson, model.AnswerLessonOptions);
            }
            else if (model.Type == LessonType.Code)
            {
                dbLesson.ReporterCode = model.ReporterCode;
                dbLesson.UnitTestsCode = model.UnitTestsCode;
            }
        }

        // Create, remove and updates answer lesson options.
        private async Task UpdateAnswerLessonModelOption(Lesson dbLesson, ICollection<AnswerLessonOption> modelAnswerLessonOptions)
        {
            var newOptions = modelAnswerLessonOptions.Where(opt => opt.Id == 0);
            foreach (var newOption in newOptions)
            {
                dbLesson.AnswerLessonOptions.Add(newOption);
                await _repository.SaveChanges();
            }

            var notNewOptions = modelAnswerLessonOptions.Where(opt => opt.Id != 0).ToList();
            foreach (var modelAnswerOption in notNewOptions)
            {
                var answerLesson = dbLesson.AnswerLessonOptions.FirstOrDefault(a => a.Id == modelAnswerOption.Id);
                if (answerLesson != null)
                {
                    answerLesson.Text = modelAnswerOption.Text;
                    answerLesson.IsCorrect = modelAnswerOption.IsCorrect;
                }
            }

            var optionIds = notNewOptions.Select(opt => opt.Id).ToList();
            if (optionIds.Any())
            {
                var toRemoveOptions = dbLesson.AnswerLessonOptions.Where(opt => !optionIds.Contains(opt.Id)).ToList();
                for (int i = 0; i < toRemoveOptions.Count; i++)
                {
                    _repository.Remove(toRemoveOptions[i]);
                    await _repository.SaveChanges();
                }
            }
        }

        private async Task<int> GetNextOrder(int chapterId)
        {
            var chapter = await _chapterService.GetById(chapterId);
            var lastLesson = chapter.Lessons.LastOrDefault();
            return lastLesson?.Order + 1 ?? 0;
        }
    }
}