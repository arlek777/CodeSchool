﻿using AutoMapper;
using CodeSchool.Domain;
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
                c.CreateMap<Chapter, ChapterShortcutRequestModel>();
                c.CreateMap<Lesson, LessonShortcutRequestModel>();
                c.CreateMap<Lesson, LessonRequestModel>();

                c.CreateMap<UserChapter, UserChapterShortcutResponseModel>()
                .ForMember(ch => ch.ChapterTitle, opts => opts.MapFrom(ch => ch.Chapter.Title))
                .ForMember(ch => ch.ChapterOrder, opts => opts.MapFrom(ch => ch.Chapter.Order));

                c.CreateMap<UserLesson, UserLessonShortcutResponseModel>()
                    .ForMember(ch => ch.LessonTitle, opts => opts.MapFrom(ch => ch.Lesson.Title))
                    .ForMember(ch => ch.LessonOrder, opts => opts.MapFrom(ch => ch.Lesson.Order));

                c.CreateMap<UserLesson, UserLessonResponseModel>();
            });
        }
    }
}
