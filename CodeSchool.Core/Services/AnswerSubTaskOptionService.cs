using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassJs.Core.Interfaces;
using PassJs.DataAccess;
using PassJs.DomainModels;

namespace PassJs.Core.Services
{
    public class AnswerSubTaskOptionService : IAnswerSubTaskOptionService
    {
        private readonly IGenericRepository _repository;

        public AnswerSubTaskOptionService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task UpdateOptions(SubTask dbSubTask, ICollection<AnswerSubTaskOption> answerSubTaskOptions)
        {
            var newOptions = answerSubTaskOptions.Where(opt => opt.Id == 0);
            foreach (var newOption in newOptions)
            {
                dbSubTask.AnswerSubTaskOptions.Add(newOption);
                await _repository.SaveChanges();
            }

            var notNewOptions = answerSubTaskOptions.Where(opt => opt.Id != 0).ToList();
            foreach (var modelAnswerOption in notNewOptions)
            {
                var answerSubTask = dbSubTask.AnswerSubTaskOptions.FirstOrDefault(a => a.Id == modelAnswerOption.Id);
                if (answerSubTask != null)
                {
                    answerSubTask.Text = modelAnswerOption.Text;
                    answerSubTask.IsCorrect = modelAnswerOption.IsCorrect;
                }
            }

            var optionIds = notNewOptions.Select(opt => opt.Id).ToList();
            if (optionIds.Any())
            {
                var toRemoveOptions = dbSubTask.AnswerSubTaskOptions.Where(opt => !optionIds.Contains(opt.Id)).ToList();
                for (int i = 0; i < toRemoveOptions.Count; i++)
                {
                    _repository.Remove(toRemoveOptions[i]);
                    await _repository.SaveChanges();
                }
            }
        }

        public async Task RemoveOption(int id)
        {
            var option = await _repository.Find<AnswerSubTaskOption>(o => o.Id == id);
            _repository.Remove(option);
            await _repository.SaveChanges();
        }
    }
}