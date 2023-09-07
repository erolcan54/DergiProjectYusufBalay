using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UI.Filters
{
    public class UserAuthFilter : ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var kullaniciId = context.HttpContext.Session.GetString("KullaniciId");
            if (kullaniciId == null)
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
