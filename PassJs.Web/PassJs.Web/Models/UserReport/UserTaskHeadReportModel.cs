using System;

namespace PassJs.Web.Models.UserReport
{
    public class UserTaskHeadReportModel
    {
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public DateTime LinkSentDt { get; set; }
        public DateTime? StartedDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public int PassedSubTasksCount { get; set; }
        public int TotalSubTasksCount { get; set; }
        public bool Cheating { get; set; }
        public int UnfocusCount { get; set; }
        public int CopyPasteCount { get; set; }

        public string Progress
        {
            get { return $"{PassedSubTasksCount} ({TotalSubTasksCount}"; }
        }

        public string FullName
        {
            get { return $"{UserName} - {UserEmail}"; }
        }

        public bool IsStarted
        {
            get { return StartedDt.HasValue; }
        }

        public bool IsFinished
        {
            get { return FinishedDt.HasValue; }
        }

        public bool IsLinkExpired
        {
            get { return LinkSentDt < DateTime.UtcNow; }
        }

        public bool IsAllPassed
        {
            get { return PassedSubTasksCount == TotalSubTasksCount; }
        }
    }
}