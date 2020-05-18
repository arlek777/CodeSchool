using System.Collections.Generic;
using PassJs.DomainModels;
using PassJs.Web.Models.SubTasks;

namespace PassJs.Web.Models.UserReport
{
    public class UserSubTaskReportModel : UserSubTaskModel
    {
        public IEnumerable<CodeSnapshot> CodeSnapshots { get; set; }
    }
}