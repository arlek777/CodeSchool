using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IAnswerLessonOptionService
    {
        Task UpdateOptions(Lesson dbLesson, ICollection<AnswerLessonOption> answerLessonOptions);
        Task RemoveOption(int id);
    }
}