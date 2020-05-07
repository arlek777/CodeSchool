using System;
using System.Collections.Generic;
using CodeSchool.Domain;
using CodeSchool.Web.Models.SubTasks;

namespace CodeSchool.Web.Models.UserReport
{
    public class UserSubTaskReportModel : UserSubTaskModel
    {
        public IEnumerable<CodeSnapshot> CodeSnapshots { get; set; }
    }
}