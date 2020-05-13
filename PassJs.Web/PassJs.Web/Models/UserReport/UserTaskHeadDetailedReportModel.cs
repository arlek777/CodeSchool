using System.Collections.Generic;

namespace PassJs.Web.Models.UserReport
{
    public class UserTaskHeadDetailedReportModel : UserTaskHeadReportModel
    {
        public IEnumerable<UserSubTaskReportModel> UserSubTasks { get; set; }
    }
}