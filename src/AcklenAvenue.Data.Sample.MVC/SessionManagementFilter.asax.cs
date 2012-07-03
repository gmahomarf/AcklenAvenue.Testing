using System.Web.Mvc;
using AcklenAvenue.Data.NHibernate;

namespace AcklenAvenue.Data.Sample.MVC
{
    public class SessionManagementFilter : ActionFilterAttribute
    {
        readonly ISessionContainer _sessionContainer;

        public SessionManagementFilter(ISessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _sessionContainer.OpenSession();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _sessionContainer.CloseSession();
            base.OnActionExecuted(filterContext);
        }
    }
}