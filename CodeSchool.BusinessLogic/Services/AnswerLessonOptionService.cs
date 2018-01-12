using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class AnswerLessonOptionService : IAnswerLessonOptionService
    {
        private readonly IGenericRepository _repository;

        public AnswerLessonOptionService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task UpdateOptions(Lesson dbLesson, ICollection<AnswerLessonOption> answerLessonOptions)
        {
            var newOptions = answerLessonOptions.Where(opt => opt.Id == 0);
            foreach (var newOption in newOptions)
            {
                dbLesson.AnswerLessonOptions.Add(newOption);
                await _repository.SaveChanges();
            }

            var notNewOptions = answerLessonOptions.Where(opt => opt.Id != 0).ToList();
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

        public async Task RemoveOption(int id)
        {
            var option = await _repository.Find<AnswerLessonOption>(o => o.Id == id);
            _repository.Remove(option);
            await _repository.SaveChanges();
        }
    }
}