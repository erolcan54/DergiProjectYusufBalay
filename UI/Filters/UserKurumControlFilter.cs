using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UI.Filters
{
    public class UserKurumControlFilter: ActionFilterAttribute
    {
        private IKullaniciService _kullaniciService;

        public UserKurumControlFilter(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var kullaniciId = context.HttpContext.Session.GetString("KullaniciId");
            var kurumId = context.HttpContext.Session.GetString("KurumId");
            var kullanici = _kullaniciService.GetById(int.Parse(kullaniciId));

            if (kullanici.Data.OkulId == int.Parse(kurumId))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                        {{"controller","Kurum"},{"action","Index"}});
                return;
            }

        }
    }
}
