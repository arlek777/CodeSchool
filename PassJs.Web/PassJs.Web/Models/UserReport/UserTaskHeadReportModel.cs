using System;
using IdentityModel;

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
        public int UnfocusCount { get; set; }
        public int CopyPasteCount { get; set; }

        // Helper getter fields

        public long LinkSent
        {
            get { return DateTimeToUnixMilisecs(LinkSentDt); }
        }

        public long Started
        {
            get { return StartedDt.HasValue ? DateTimeToUnixMilisecs(StartedDt.Value) : 0; }
        }

        public long Finished
        {
            get { return FinishedDt.HasValue ? DateTimeToUnixMilisecs(FinishedDt.Value) : 0; }
        }

        public bool Cheating
        {
            get { return UnfocusCount > 0 || CopyPasteCount > 0; }
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

        public long DateTimeToUnixMilisecs(DateTime MyDateTime)
        {
            TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)timeSpan.TotalSeconds * 1000;
        }
    }
}