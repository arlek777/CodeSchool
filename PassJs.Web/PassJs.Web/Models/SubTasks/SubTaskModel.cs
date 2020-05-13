using System.ComponentModel.DataAnnotations;
using CodeSchool.Domain;
using PassJs.Web.Attributes;

namespace PassJs.Web.Models.SubTasks
{
    public class SubTaskModel: SubTaskShortcutModel
    {
        [RequiredForSubTaskType(SubTaskType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationResultMessages.RequiredField)]
        public string TaskText { get; set; }

        [RequiredForSubTaskType(SubTaskType.Code, SubTaskType.LongAnswer, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string Answer { get; set; }

        [RequiredForSubTaskType(SubTaskType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string UnitTestsCode { get; set; }

        [RequiredForSubTaskType(SubTaskType.Code, ErrorMessage = ValidationResultMessages.RequiredField)]
        public string ReporterCode { get; set; }

        [RequiredForSubTaskType(SubTaskType.Test, ErrorMessage = ValidationResultMessages.RequiredField)]
        public AnswerSubTaskOptionModel[] AnswerSubTaskOptions { get; set; }

        public bool PublishNow { get; set; }
    }
}