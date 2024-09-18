using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SchoolWebApi.Helpers
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}
