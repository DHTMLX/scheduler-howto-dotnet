using DHX.Scheduler.Web.Models;
using System;
using System.Text.Encodings.Web;

namespace DHX.Scheduler.Web.Models
{
    public class WebAPIRecurringEvent
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int? duration { get; set; }
        public string rrule { get; set; }
        public string recurring_event_id { get; set; }
        public string original_start { get; set; }
        public bool? deleted { get; set; }


        public static explicit operator WebAPIRecurringEvent(SchedulerRecurringEvent schedulerEvent)
        {
            return new WebAPIRecurringEvent
            {
                id = schedulerEvent.Id,
                text = schedulerEvent.Text,
                start_date = schedulerEvent.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = schedulerEvent.EndDate.ToString("yyyy-MM-dd HH:mm"),
                duration = schedulerEvent.Duration,
                rrule = schedulerEvent.Rrule,
                recurring_event_id = schedulerEvent.RecurringEventId,
                original_start = schedulerEvent.OriginalStart,
                deleted = schedulerEvent.Deleted,
            };
        }

        public static explicit operator SchedulerRecurringEvent(WebAPIRecurringEvent schedulerEvent)
        {
            return new SchedulerRecurringEvent
            {
                Id = schedulerEvent.id,
                Text = schedulerEvent.text,
                StartDate = DateTime.Parse(
                    schedulerEvent.start_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                EndDate = DateTime.Parse(
                    schedulerEvent.end_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                Duration = schedulerEvent.duration,
                Rrule = schedulerEvent.rrule,
                RecurringEventId = schedulerEvent.recurring_event_id,
                OriginalStart = schedulerEvent.original_start,
                Deleted = schedulerEvent.deleted

            };
        }

    }
}