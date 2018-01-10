using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Models
{
    public class FilterUserChapterModel
    {
        public Guid UserId { get; set; }
        public int? CategoryId { get; set; }
        public ChapterType? Type { get; set; }

        public IEnumerable<Expression<Func<UserChapter, bool>>> GetFilters()
        {
            var filters = new List<Expression<Func<UserChapter, bool>>>() { c => c.UserId == UserId };

            if(CategoryId.HasValue) filters.Add(c => c.Chapter.CategoryId == CategoryId);
            if(Type.HasValue) filters.Add(c => c.Chapter.Type == Type);

            return filters;
        }
    }
}