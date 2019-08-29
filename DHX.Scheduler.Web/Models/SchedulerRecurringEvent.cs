using System;

namespace DHX.Scheduler.Web.Models
{
    public class SchedulerRecurringEvent
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int EventPID { get; set; }
        public string RecType { get; set; }
        public long EventLength { get; set; }

    }
}