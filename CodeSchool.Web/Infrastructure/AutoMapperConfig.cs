using System.Linq;
using AutoMapper;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.TaskHeads;
using CodeSchool.Web.Models.SubTasks;
using CodeSchool.Web.Models.UserReport;

namespace CodeSchool.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<TaskHead, TaskHeadShortcutModel>().ReverseMap();
                c.CreateMap<SubTask, SubTaskShortcutModel>().ReverseMap();
                c.CreateMap<SubTask, SubTaskModel>().ReverseMap();
                c.CreateMap<AnswerSubTaskOption, AnswerSubTaskOptionModel>().ReverseMap();

                c.CreateMap<UserTaskHead, UserTaskHeadShortcutModel>()
                .ForMember(ch => ch.TaskHeadTitle, opts => opts.MapFrom(ch => ch.TaskHead.Title))
                .ForMember(ch => ch.TaskHeadOrder, opts => opts.MapFrom(ch => ch.TaskHead.Order));

                c.CreateMap<UserSubTask, UserSubTaskShortcutModel>()
                    .ForMember(ch => ch.SubTaskTitle, opts => opts.MapFrom(ch => ch.SubTask.Title))
                    .ForMember(ch => ch.SubTaskOrder, opts => opts.MapFrom(ch => ch.SubTask.Order));

                c.CreateMap<UserSubTask, UserSubTaskModel>();
                c.CreateMap<UserSubTask, UserSubTaskReportModel>();

                c.CreateMap<UserTaskHead, UserTaskHeadReportModel>()
                    .ForMember(u => u.LinkSentDt, opts =>
                        opts.MapFrom(u => u.CreatedDt))
                    .ForMember(u => u.UserName, opts =>
                        opts.MapFrom(u => u.User.UserName))
                    .ForMember(u => u.UserEmail, opts =>
                        opts.MapFrom(u => u.User.Email))
                    .ForMember(u => u.TotalSubTasksCount, opts =>
                        opts.MapFrom(u => u.UserSubTasks.Count))
                    .ForMember(u => u.PassedSubTasksCount, opts =>
                        opts.MapFrom(u => u.UserSubTasks.Count(cu => cu.IsPassed)));

                c.CreateMap<UserTaskHead, UserTaskHeadDetailedReportModel>();
            });
        }
    }
}
