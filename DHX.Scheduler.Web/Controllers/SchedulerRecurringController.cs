using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using DHX.Scheduler.Web.App_Start;
using DHX.Scheduler.Web.Models;

namespace DHX.Scheduler.Web.Controllers
{
    [SchedulerAPIExceptionFilter]
    public class SchedulerRecurringController : ApiController
    {
        private SchedulerContext db = new SchedulerContext();

        // GET: api/schedulerrecurring
        public IEnumerable<WebAPIRecurringEvent> Get(DateTime from, DateTime to)
        {
            return db.SchedulerRecurringEvents
                .Where(e => e.StartDate < to && e.EndDate >= from)
                .ToList()
                .Select(e => (WebAPIRecurringEvent)e);
        }

        // GET: api/schedulerrecurring/5
        public WebAPIRecurringEvent Get(int id)
        {
            return (WebAPIRecurringEvent)db.SchedulerRecurringEvents.Find(id);
        }

        // PUT: api/schedulerrecurring/5
        [HttpPut]
        public IHttpActionResult EditSchedulerEvent(int id, WebAPIRecurringEvent webAPIEvent)
        {
            var updatedSchedulerEvent = (SchedulerRecurringEvent)webAPIEvent;
            var dbEvent = db.SchedulerRecurringEvents.Find(id);
            if (dbEvent == null)
            {
                return NotFound();
            }
            dbEvent.Text = updatedSchedulerEvent.Text;
            dbEvent.StartDate = updatedSchedulerEvent.StartDate;
            dbEvent.EndDate = updatedSchedulerEvent.EndDate;
            dbEvent.Duration = updatedSchedulerEvent.Duration;
            dbEvent.RRule = updatedSchedulerEvent.RRule;
            dbEvent.RecurringEventId = updatedSchedulerEvent.RecurringEventId;
            dbEvent.OriginalStart = updatedSchedulerEvent.OriginalStart;
            if (!string.IsNullOrEmpty(dbEvent.RRule) && dbEvent.RecurringEventId == null)
            {
                // all modified occurrences must be deleted when we update recurring series
                // https://docs.dhtmlx.com/scheduler/server_integration.html#savingrecurringevents
                var eventsToDelete = db.SchedulerRecurringEvents
                    .Where(e => !string.IsNullOrEmpty(e.RecurringEventId) && e.RecurringEventId == dbEvent.Id.ToString())
                    .ToList();
                db.SchedulerRecurringEvents.RemoveRange(eventsToDelete);
            }

            db.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // POST: api/schedulerrecurring/5
        [HttpPost]
        public IHttpActionResult CreateSchedulerEvent(WebAPIRecurringEvent webAPIEvent)
        {
            var newSchedulerEvent = (SchedulerRecurringEvent)webAPIEvent;
            var action = "inserted";
            if (webAPIEvent.deleted == true)
            {
                // delete a single occurrence from  recurring series
                action = "deleted";
            }
            db.SchedulerRecurringEvents.Add(newSchedulerEvent);
            db.SaveChanges();

            return Ok(new
            {
                tid = newSchedulerEvent.Id,
                action
            });
        }

        // DELETE: api/schedulerrecurring/5
        [HttpDelete]
        public IHttpActionResult DeleteSchedulerEvent(int id)
        {
            var schedulerEvent = db.SchedulerRecurringEvents.Find(id);
            if (schedulerEvent != null)
            {
                if (schedulerEvent.RecurringEventId != null)
                {
                    // deleting modified occurrence from recurring series
                    // If an event with the recurring_event_id value was deleted - it needs updating with .deleted = true instead of deleting.
                    schedulerEvent.Deleted = true;
                }
                else
                {
                    if (schedulerEvent.RRule != null)
                    {
                        // if a recurring series was deleted - delete all modified occurrences of the series
                        var eventsToDelete = db.SchedulerRecurringEvents
                            .Where(e => !string.IsNullOrEmpty(e.RecurringEventId) && e.RecurringEventId == schedulerEvent.Id.ToString())
                            .ToList();
                        db.SchedulerRecurringEvents.RemoveRange(eventsToDelete);
                    }
                    db.SchedulerRecurringEvents.Remove(schedulerEvent);
                }
                db.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}