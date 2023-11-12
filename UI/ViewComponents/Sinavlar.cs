using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class Sinavlar:ViewComponent
    {
        private readonly IBurslulukSinavService _burslulukSinavService;

        public Sinavlar(IBurslulukSinavService burslulukSinavService)
        {
            _burslulukSinavService = burslulukSinavService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _burslulukSinavService.GetAllDisplay4Take();
            return View(result.Data);
        }
    }
}
