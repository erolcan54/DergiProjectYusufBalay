using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class OzelDersOgretmenler:ViewComponent
    {
        private readonly IOzelDersOgretmenService _ozelDersOgretmenService;

        public OzelDersOgretmenler(IOzelDersOgretmenService ozelDersOgretmenService)
        {
            _ozelDersOgretmenService = ozelDersOgretmenService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _ozelDersOgretmenService.GetAllDisplay4Take();
            return View(result.Data);
        }
    }
}
