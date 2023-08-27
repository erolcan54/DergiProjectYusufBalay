using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class KurumMenu:ViewComponent
    {
        private IOkulService _okulService;

        public KurumMenu(IOkulService okulService)
        {
            _okulService = okulService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var result=_okulService.GetById(id);
            ViewData["Kurum"] = result.Data;
            return View();
        }
    }
}
