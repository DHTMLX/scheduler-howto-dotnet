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
            updatedSchedulerEvent.Id = id;
            db.Entry(updatedSchedulerEvent).State = EntityState.Modified;

            if (!string.IsNullOrEmpty(updatedSchedulerEvent.RecType) && updatedSchedulerEvent.RecType != "none")
            {
                //all modified occurrences must be deleted when we update a recurring series
                //https://docs.dhtmlx.com/scheduler/server_integration.html#savingrecurringevents

                db.SchedulerRecurringEvents.RemoveRange(
                    db.SchedulerRecurringEvents.Where(e => e.EventPID == id)
                );
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
            db.SchedulerRecurringEvents.Add(newSchedulerEvent);
            db.SaveChanges();

            // delete a single occurrence from a recurring series
            var resultAction = "inserted";
            if (newSchedulerEvent.RecType == "none")
            {
                resultAction = "deleted";
            }

            return Ok(new
            {
                tid = newSchedulerEvent.Id,
                action = resultAction
            });
        }

        // DELETE: api/schedulerrecurring/5
        [HttpDelete]
        public IHttpActionResult DeleteSchedulerEvent(int id)
        {
            var schedulerEvent = db.SchedulerRecurringEvents.Find(id);
            if (schedulerEvent != null)
            {
                //some logic specific to recurring events support
                //https://docs.dhtmlx.com/scheduler/server_integration.html#savingrecurringevents

                if (schedulerEvent.EventPID != default(int))
                {
                    // deleting a modified occurrence from a recurring series
                    // If an event with the event_pid value was deleted, it should be updated 
                    // with rec_type==none instead of deleting.

                    schedulerEvent.RecType = "none";
                }
                else
                {
                    //if a recurring series deleted, delete all modified occurrences of the series
                    if (!string.IsNullOrEmpty(schedulerEvent.RecType) && schedulerEvent.RecType != "none")
                    {
                        //all modified occurrences must be deleted when we update recurring series
                        //https://docs.dhtmlx.com/scheduler/server_integration.html#savingrecurringevents
                        db.SchedulerRecurringEvents.RemoveRange(
                            db.SchedulerRecurringEvents.Where(ev => ev.EventPID == id)
                        );
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