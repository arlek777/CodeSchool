using System.Collections.Generic;
using CodeSchool.Web.Models.SubTasks;

namespace CodeSchool.Web.Models.UserReport
{
    public class UserTaskHeadDetailedReportModel : UserTaskHeadReportModel
    {
        public IEnumerable<UserSubTaskReportModel> UserSubTasks { get; set; }
    }
}