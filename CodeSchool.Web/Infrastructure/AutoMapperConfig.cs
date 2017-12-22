using System.Linq;
using AutoMapper;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using CodeSchool.Web.Models.Chapters;
using CodeSchool.Web.Models.Lessons;

namespace CodeSchool.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<Chapter, ChapterShortcutModel>().ReverseMap();
                c.CreateMap<Lesson, LessonShortcutModel>().ReverseMap();
                c.CreateMap<Lesson, LessonModel>().ReverseMap();

                c.CreateMap<UserChapter, UserChapterShortcutModel>()
                .ForMember(ch => ch.ChapterTitle, opts => opts.MapFrom(ch => ch.Chapter.Title))
                .ForMember(ch => ch.ChapterOrder, opts => opts.MapFrom(ch => ch.Chapter.Order));

                c.CreateMap<UserLesson, UserLessonShortcutModel>()
                    .ForMember(ch => ch.LessonTitle, opts => opts.MapFrom(ch => ch.Lesson.Title))
                    .ForMember(ch => ch.LessonOrder, opts => opts.MapFrom(ch => ch.Lesson.Order));

                c.CreateMap<UserLesson, UserLessonModel>();

                c.CreateMap<User, UserStatisticModel>()
                    .ForMember(u => u.PassedLessons, opts => 
                        opts.MapFrom(u => u.UserLessons.Where(l => l.IsPassed).Select(l => l.Lesson.Title)));
            });
        }
    }
}
