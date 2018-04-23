using System.Web.Http.Filters;

namespace FeedbackCba.Handler
{
    public class WebApiGlobalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}