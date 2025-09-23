using System;

namespace DHX.Scheduler.Web.Models
{
    public class SchedulerRecurringEvent
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Duration { get; set; }
        public string Rrule { get; set; }
        public string RecurringEventId { get; set; }
        public string OriginalStart { get; set; }
        public bool? Deleted { get; set; }
    }
}