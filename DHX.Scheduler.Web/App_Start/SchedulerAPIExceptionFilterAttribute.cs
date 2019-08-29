using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace DHX.Scheduler.Web.App_Start
{
    public class SchedulerAPIExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, new
            {
                action = "error",
                message = context.Exception.Message
            });
        }
    }
}