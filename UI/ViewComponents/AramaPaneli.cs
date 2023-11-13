using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.ViewComponents
{
    public class AramaPaneli : ViewComponent
    {
        private IilService _ilService;
        private IOkulTurService _okulTurService;
        public AramaPaneli(IilService ilService, IOkulTurService okulTurService)
        {
            _ilService = ilService;
            _okulTurService = okulTurService;
        }
        public IViewComponentResult Invoke()
        {
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;

            var okulTurleri = _okulTurService.GetAll();
            var okulTurSelectList = (from i in okulTurleri.Data
                                     select new SelectListItem
                                     {
                                         Text = i.Tip.ToUpper(),
                                         Value = i.Id.ToString()
                                     }).ToList();
            ViewData["okulTurleri"] = okulTurSelectList;
            KurumAraDto model = new KurumAraDto();
            return View(model);
        }

    }
}
