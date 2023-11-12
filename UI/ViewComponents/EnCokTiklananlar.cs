using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class EnCokTiklananlar:ViewComponent
    {
        private readonly IOkulService _okulService;

        public EnCokTiklananlar(IOkulService okulService)
        {
            _okulService = okulService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _okulService.GetTikKurum4Take();
            return View(result.Data);
        }
    }
}
