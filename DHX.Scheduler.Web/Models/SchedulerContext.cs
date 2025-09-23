using System.Data.Entity;

namespace DHX.Scheduler.Web.Models
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext() : base("name=SchedulerContext")
        {
        }

        public DbSet<SchedulerEvent> SchedulerEvents { get; set; }
        public DbSet<SchedulerRecurringEvent> SchedulerRecurringEvents { get; set; }
    }
}