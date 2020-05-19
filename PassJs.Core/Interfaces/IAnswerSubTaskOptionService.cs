using System.Collections.Generic;
using System.Threading.Tasks;
using PassJs.DomainModels;

namespace PassJs.Core.Interfaces
{
    public interface IAnswerSubTaskOptionService
    {
        Task UpdateOptions(SubTask dbSubTask, ICollection<AnswerSubTaskOption> answerSubTaskOptions);
        Task RemoveOption(int id);
    }
}