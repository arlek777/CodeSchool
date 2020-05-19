using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PassJs.DomainModels;

namespace PassJs.Core.Models
{
    public class FilterUserTaskHeadModel
    {
        public Guid UserId { get; set; }
        public int? CategoryId { get; set; }
        public TaskType? Type { get; set; }

        public IEnumerable<Expression<Func<UserTaskHead, bool>>> GetFilters()
        {
            var filters = new List<Expression<Func<UserTaskHead, bool>>>() { c => c.UserId == UserId };

            if(CategoryId.HasValue) filters.Add(c => c.TaskHead.CategoryId == CategoryId);
            if(Type.HasValue) filters.Add(c => c.TaskHead.Type == Type);

            return filters;
        }
    }
}