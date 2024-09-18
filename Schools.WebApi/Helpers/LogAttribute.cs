using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace SchoolWebApi.Helpers
{
    public class LogAttribute : ActionFilterAttribute
    {
        public LogAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Trace.WriteLine(string.Format("Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:")), "Web API");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Trace.WriteLine(string.Format("Method {0} executed at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:")), "Web API");
        }
    }
}
