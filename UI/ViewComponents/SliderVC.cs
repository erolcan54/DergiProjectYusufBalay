using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class SliderVC:ViewComponent
    {
        private ISliderService _sliderService;

        public SliderVC(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _sliderService.GetAll();
            return View(result.Data);
        }
    }
}
