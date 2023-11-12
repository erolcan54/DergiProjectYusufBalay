using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class indirimler:ViewComponent
    {
        private readonly IindirimService _indirimService;

        public indirimler(IindirimService indirimService)
        {
            _indirimService = indirimService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _indirimService.GetAllDisplay4Take();
            return View(result.Data);
        }
    }
}
