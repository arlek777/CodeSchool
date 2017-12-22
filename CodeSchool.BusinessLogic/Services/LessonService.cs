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
                
                lesson.ReporterCode = model.ReporterCode;
                lesson.UnitTestsCode = model.UnitTestsCode;
                lesson.Answer = model.Answer;
                lesson.TaskText = model.TaskText;
                lesson.Text = model.Text;
                lesson.Title = model.Title;

                if (model.Type == LessonType.Test)
                {
                    UpdateAnswerLessonModelOption(model.AnswerLessonOptions, lesson.AnswerLessonOptions);
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
            _repository.Remove(lesson);
            await _repository.SaveChanges();
        }

        private void UpdateAnswerLessonModelOption(IEnumerable<AnswerLessonOption> modelAnswerLessonOptions, IEnumerable<AnswerLessonOption> dbAnswerLessonOptions)
        {
            foreach (var modelAnswerOption in modelAnswerLessonOptions)
            {
                var answerLesson = dbAnswerLessonOptions.FirstOrDefault(a => a.Id == modelAnswerOption.Id);
                if (answerLesson != null)
                {
                    answerLesson.Text = modelAnswerOption.Text;
                    answerLesson.IsCorrect = modelAnswerOption.IsCorrect;
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