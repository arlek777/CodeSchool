using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IAnswerSubTaskOptionService
    {
        Task UpdateOptions(SubTask dbSubTask, ICollection<AnswerSubTaskOption> answerSubTaskOptions);
        Task RemoveOption(int id);
    }
}