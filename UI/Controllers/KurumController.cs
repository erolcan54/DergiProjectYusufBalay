using Business.Abstract;
using Core.Utilities.Results;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.Filters;

namespace UI.Controllers
{
    [Authorize]
    [TypeFilter(typeof(UserAuthFilter))]
    public class KurumController : Controller
    {
        private IOgretmenService _ogretmenService;
        private IOkulService _okulService;
        private IilService _ilService;
        private IilceService _ilceService;
        private IOkulTurService _okulTurService;
        private IKullaniciService _kullaniciService;
        private IBransService _bransService;
        private IEgitimModeliService _egitimModeliService;
        private IEgitimModeliResimService _egitimModeliResimService;
        private IBasariService _basariService;
        private IHttpContextAccessor _contextAccessor;

        public KurumController(IOgretmenService ogretmenService, IHttpContextAccessor contextAccessor, IOkulService okulService, IilService ilService, IilceService ilceService, IOkulTurService okulTurService, IKullaniciService kullaniciService, IBransService bransService, IEgitimModeliService egitimModeliService, IEgitimModeliResimService egitimModeliResimService, IBasariService basariService)
        {
            _ogretmenService = ogretmenService;
            _contextAccessor = contextAccessor;
            _okulService = okulService;
            _ilService = ilService;
            _ilceService = ilceService;
            _okulTurService = okulTurService;
            _kullaniciService = kullaniciService;
            _bransService = bransService;
            _egitimModeliService = egitimModeliService;
            _egitimModeliResimService = egitimModeliResimService;
            _basariService = basariService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CikisYap()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<List<SelectListItem>> GetSelectListilce(int ilId)
        {
            var result = _ilceService.GetByilIdToList(ilId);
            List<SelectListItem> ilceListe = (from i in result.Data
                                              select new SelectListItem
                                              {
                                                  Text = i.Ad.ToUpper(),
                                                  Value = i.Id.ToString()
                                              }).ToList();
            return ilceListe;
        }

        public IActionResult KurumDetay()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var kurum = _okulService.GetByIdDisplay(int.Parse(kurumId));
            return View(kurum.Data);
        }
        public IActionResult KurumGuncelle()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var kurum = _okulService.GetById(int.Parse(kurumId));
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

            var resultilce = _ilceService.GetByilIdToList(kurum.Data.ilId);
            List<SelectListItem> ilceListe = (from i in resultilce.Data
                                              select new SelectListItem
                                              {
                                                  Text = i.Ad.ToUpper(),
                                                  Value = i.Id.ToString()
                                              }).ToList();
            ViewData["ilceler"] = ilceListe;
            return View(kurum.Data);
        }

        [HttpPost]
        public IActionResult KurumGuncelle(Okul model, IFormFile Resim)
        {
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
            }
            else
            {
                var result = _okulService.GetById(model.Id);
                model.Resim = result.Data.Resim;
            }
            var res = _okulService.Update(model);
            return Json(res);
        }

        public IActionResult KullaniciListesi()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var result = _kullaniciService.GetAllByIdKurum(int.Parse(kurumId));
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult KullaniciSil(int id)
        {
            var result = _kullaniciService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult KullaniciEkle(Kullanici model)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.OkulId = int.Parse(kurumId);
            var result = _kullaniciService.Add(model);
            return Json(result);
        }

        public IActionResult KullaniciGuncelle(int kullaniciId)
        {
            var result = _kullaniciService.GetById(kullaniciId);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult KullaniciGuncelle(Kullanici model)
        {
            var result = _kullaniciService.Update(model);
            return Json(result);
        }

        public IActionResult OgretmenListesi()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var result = _ogretmenService.GetAllGetByKurumId(int.Parse(kurumId));
            List<SelectListItem> branss = (from i in _bransService.GetAll().Data
                                           select new SelectListItem
                                           {
                                               Text = i.Ad.ToUpper(),
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewData["branslar"] = branss;
            return View(result.Data);
        }


        [HttpPost]
        public IActionResult OgretmenEkle(Ogretmen model, IFormFile Resim)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            using (var stream = new MemoryStream())
            {
                Resim.CopyTo(stream);
                model.Resim = stream.ToArray();
            }

            var result = _ogretmenService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult OgretmenSil(int id)
        {
            var result = _ogretmenService.Delete(id);
            return Json(result);
        }

        public IActionResult OgretmenGuncelle(int ogretmenId)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var result = _ogretmenService.GetById(ogretmenId);
            List<SelectListItem> branss = (from i in _bransService.GetAll().Data
                                           select new SelectListItem
                                           {
                                               Text = i.Ad.ToUpper(),
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewData["branslar"] = branss;
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult OgretmenGuncelle(Ogretmen model, IFormFile Resim)
        {
            var ogr = _ogretmenService.GetById(model.Id);
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
            }
            else
                model.Resim = ogr.Data.Resim;
            var result = _ogretmenService.Update(model);
            return Json(result);
        }

        public IActionResult EgitimModeli()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var egitimModel = _egitimModeliService.GetByKurumId(int.Parse(kurumId));
            ViewData["EgitimModeli"] = egitimModel.Data;
            var resimler = _egitimModeliResimService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Resimler"] = resimler.Data;
            return View();
        }

        [HttpPost]
        public IActionResult EgitimModeliResimEkle(KurumEgitimModeliResim model, IFormFile Resim)
        {
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
                var result = _egitimModeliResimService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Resim seçmediniz."));

        }

        [HttpPost]
        public IActionResult EgitimModeliIcerikEkle(KurumEgitimModeli model)
        {
            if (String.IsNullOrEmpty(model.Icerik))
                return Json(new ErrorResult("İçerikl boş bırakılamaz."));
            var result = _egitimModeliService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult EgitimModeliIcerikGuncelle(string Icerik1)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var model = _egitimModeliService.GetByKurumId(int.Parse(kurumId));
            model.Data.Icerik = Icerik1;
            var result = _egitimModeliService.Update(model.Data);
            return Json(result);
        }

        [HttpPost]
        public IActionResult ResimSil(int id)
        {
            var result = _egitimModeliResimService.Delete(id);
            return Json(result);
        }

        public IActionResult Basarilar()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var basariListe = _basariService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Basarilar"] = basariListe.Data;
            return View();
        }

        [HttpPost]
        public IActionResult BasariEkle(Basari model, IFormFile Resim)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            using (var stream = new MemoryStream())
            {
                Resim.CopyTo(stream);
                model.Resim = stream.ToArray();
            }
            var result = _basariService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult BasariSil(int id)
        {
            var result = _basariService.Delete(id);
            return Json(result);
        }

        public IActionResult BasariGuncelle(int id)
        {
            var basari = _basariService.GetById(id);
            ViewData["Basari"] = basari.Data;
            var kurum = _okulService.GetById(basari.Data.KurumId);
            ViewData["Kurum"] = kurum.Data;
            return View();
        }

        [HttpPost]
        public IActionResult BasariGuncelle(Basari model, IFormFile Resim)
        {
            var basari = _basariService.GetById(model.Id);
            basari.Data.Baslik = model.Baslik;
            basari.Data.Icerik = model.Icerik;
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    basari.Data.Resim = stream.ToArray();
                }
            }
            var result = _basariService.Update(basari.Data);
            return Json(result);
        }

    }
}
