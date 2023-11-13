using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class LayoutFooter:ViewComponent
    {
        private IKurumsalService _kurumsalService;

        public LayoutFooter(IKurumsalService kurumsalService)
        {
            _kurumsalService = kurumsalService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _kurumsalService.GetAll();
            return View(result.Data);
        }
    }
}
