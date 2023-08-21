using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UI.Filters
{
    public class AdminAuthFilter: ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var yoneticiId = context.HttpContext.Session.GetString("YoneticiId");

            if (yoneticiId == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                        {{"controller","Home"},{"action","Index"}});
                return;
                //await context.Result.ExecuteResultAsync(context);
            }

        }
    }
}
