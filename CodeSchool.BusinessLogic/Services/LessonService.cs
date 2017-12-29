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
            var lesson = await GetById(model.Id);
            if (lesson == null)
            {
                model.Order = await GetNextOrder(model.ChapterId);
                _repository.Add(model);
                lesson = model;
            }
            else
            {
                lesson.Answer = model.Answer;
                lesson.TaskText = model.TaskText;
                lesson.Text = model.Text;
                lesson.Title = model.Title;

                if (model.Type == LessonType.Test)
                {
                    await UpdateAnswerLessonModelOption(lesson, model.AnswerLessonOptions);
                }
                else if (model.Type == LessonType.Code)
                {
                    lesson.ReporterCode = model.ReporterCode;
                    lesson.UnitTestsCode = model.UnitTestsCode;
                }
            }
           
            await _repository.SaveChanges();
            return lesson;
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

        // Create, remove and updates answer lesson options.
        private async Task UpdateAnswerLessonModelOption(Lesson lesson, ICollection<AnswerLessonOption> modelAnswerLessonOptions)
        {
            var newOptions = modelAnswerLessonOptions.Where(opt => opt.Id == 0);
            foreach (var newOption in newOptions)
            {
                lesson.AnswerLessonOptions.Add(newOption);
                await _repository.SaveChanges();
            }

            foreach (var modelAnswerOption in modelAnswerLessonOptions)
            {
                var answerLesson = lesson.AnswerLessonOptions.FirstOrDefault(a => a.Id == modelAnswerOption.Id);
                if (answerLesson != null)
                {
                    answerLesson.Text = modelAnswerOption.Text;
                    answerLesson.IsCorrect = modelAnswerOption.IsCorrect;
                }
            }

            var optionIds = modelAnswerLessonOptions.Where(opt => opt.Id != 0).Select(opt => opt.Id);
            var toRemoveOptions = lesson.AnswerLessonOptions.Where(opt => !optionIds.Contains(opt.Id)).ToList();
            for (int i = 0; i < toRemoveOptions.Count; i++)
            {
                _repository.Remove(toRemoveOptions[i]);
                await _repository.SaveChanges();
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