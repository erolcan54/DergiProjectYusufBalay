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
        private IKatalogService _katalogService;
        private IIcGorselService _icGorselService;
        private IDisGorselService _disGorselService;
        private IEtkinlikService _etkinlikService;
        private IEtkinlikResimService _etkinlikResimService;
        private IKulupService _kulupService;
        private IKurumYorumService _kurumYorumService;
        private IKurumYorumBegeniService _kurumYorumBegeniService;
        private IKurumBeniAraService _kurumBeniAraService;
        private IindirimService _indirimService;
        private IBurslulukSinavBasvuruService _burslulukSinavBasvuruService;
        private IBurslulukSinavService _burslulukSinavService;
        private IHttpContextAccessor _contextAccessor;

        public KurumController(IOgretmenService ogretmenService, IHttpContextAccessor contextAccessor, IOkulService okulService, IilService ilService, IilceService ilceService, IOkulTurService okulTurService, IKullaniciService kullaniciService, IBransService bransService, IEgitimModeliService egitimModeliService, IEgitimModeliResimService egitimModeliResimService, IBasariService basariService, IKatalogService katalogService, IIcGorselService icGorselService, IDisGorselService disGorselService, IEtkinlikResimService etkinlikResimService, IEtkinlikService etkinlikService, IKulupService kulupService, IKurumYorumService kurumYorumService, IKurumYorumBegeniService kurumYorumBegeniService, IKurumBeniAraService kurumBeniAraService, IindirimService indirimService, IBurslulukSinavBasvuruService burslulukSinavBasvuruService, IBurslulukSinavService burslulukSinavService)
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
            _katalogService = katalogService;
            _icGorselService = icGorselService;
            _disGorselService = disGorselService;
            _etkinlikResimService = etkinlikResimService;
            _etkinlikService = etkinlikService;
            _kulupService = kulupService;
            _kurumYorumService = kurumYorumService;
            _kurumYorumBegeniService = kurumYorumBegeniService;
            _kurumBeniAraService = kurumBeniAraService;
            _indirimService = indirimService;
            _burslulukSinavBasvuruService = burslulukSinavBasvuruService;
            _burslulukSinavService = burslulukSinavService;
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

        public IActionResult Kataloglar()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var katalogliste = _katalogService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Kataloglar"] = katalogliste.Data;
            return View();
        }

        [HttpPost]
        public IActionResult KatalogEkle(Katalog model, IFormFile katalogpdf)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            if (katalogpdf != null)
            {
                using (var stream = new MemoryStream())
                {
                    katalogpdf.CopyTo(stream);
                    model.KatalogPDF = stream.ToArray();
                }
                model.SeriNo = Guid.NewGuid();
                var result = _katalogService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Katalog için pdf seçiniz..."));
        }

        public IActionResult KatalogIndir(Guid seriNo)
        {
            var result = _katalogService.GetBySeriNo(seriNo);
            return File(result.Data.KatalogPDF, "application/pdf", result.Data.KatalogAdi + ".pdf");
        }

        [HttpPost]
        public IActionResult KatalogSil(int id)
        {
            var result = _katalogService.Delete(id);
            return Json(result);
        }

        #region İç görseller
        public IActionResult IcGorseller()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var gorseller = _icGorselService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Resimler"] = gorseller.Data;
            return View();
        }

        [HttpPost]
        public IActionResult IcGorselResimSil(int id)
        {
            var result = _icGorselService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult IcGorselResimEkle(IcGorsel model, IFormFile Resim)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
                var result = _icGorselService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Resim seçmediniz."));
        }
        #endregion

        #region Dış Görseller
        public IActionResult DisGorseller(int id)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var gorseller = _disGorselService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Resimler"] = gorseller.Data;
            return View();
        }

        [HttpPost]
        public IActionResult DisGorselResimSil(int id)
        {
            var result = _disGorselService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DisGorselResimEkle(DisGorsel model, IFormFile Resim)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
                var result = _disGorselService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Resim seçmediniz."));
        }
        #endregion

        #region Etkinlik-Etkinlik Resim
        public IActionResult Etkinlikler()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var etkinlikler = _etkinlikService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Etkinlikler"] = etkinlikler.Data;
            return View();
        }

        [HttpPost]
        public IActionResult EtkinlikEkle(Etkinlik model)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            var result = _etkinlikService.Add(model);
            DataResult<Etkinlik> etk;
            if (result.Success)
            {
                etk = new SuccessDataResult<Etkinlik>(model, result.Message);
            }
            else
            {
                etk = new ErrorDataResult<Etkinlik>(null, result.Message);
            }
            return Json(etk);
        }

        public IActionResult EtkinlikDetay(int id)
        {
            var result = _etkinlikService.GetById(id);
            ViewData["Etkinlik"] = result.Data;
            var resimler = _etkinlikResimService.GetAllByEtkinlikId(result.Data.Id);
            ViewData["Resimler"] = resimler.Data;
            return View();
        }

        [HttpPost]
        public IActionResult EtkinlikSil(int id)
        {
            var result = _etkinlikService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult EtkinlikGuncelle(Etkinlik model)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            var result = _etkinlikService.Update(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult EtkinlikResimEkle(EtkinlikResim model, IFormFile Resim)
        {
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
                var result = _etkinlikResimService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Resim seçmediniz."));
        }

        [HttpPost]
        public IActionResult EtkinlikResimSil(int id)
        {
            var result = _etkinlikResimService.Delete(id);
            return Json(result);
        }
        #endregion

        #region Kulüp İşlemleri
        public IActionResult Kulupler()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var kulupler = _kulupService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Kulupler"] = kulupler.Data.OrderByDescending(a=>a.Id).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult KulupEkle(Kulup model, IFormFile Resim)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            using (var stream = new MemoryStream())
            {
                Resim.CopyTo(stream);
                model.Resim = stream.ToArray();
            }
            var result = _kulupService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult KulupSil(int id)
        {
            var result = _kulupService.Delete(id);
            return Json(result);
        }

        public IActionResult KulupGuncelle(int id)
        {
            var kulup = _kulupService.GetById(id);
            ViewData["Kulup"] = kulup.Data;
            return View();
        }

        [HttpPost]
        public IActionResult KulupGuncelle(Kulup model, IFormFile Resim)
        {
            var kulup = _kulupService.GetById(model.Id);
            kulup.Data.Ad = model.Ad;
            kulup.Data.Icerik = model.Icerik;
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    kulup.Data.Resim = stream.ToArray();
                }
            }
            var result = _kulupService.Update(kulup.Data);
            return Json(result);
        }

        #endregion

        #region Yorumlar
        public IActionResult Yorumlar()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var yorums = _kurumYorumService.GetAllByKurumId(int.Parse(kurumId)); 
            ViewData["yorumlar"] = yorums.Data.OrderByDescending(a => a.Id).ToList();
            ViewData["yorumSayisi"] = _kurumYorumService.GetCountByKurumId(int.Parse(kurumId)).Data;
            return View();
        }

        [HttpPost]
        public IActionResult KurumYorumSil(int id)
        {
            var result = _kurumYorumService.Delete(id);
            return Json(result);

        }
        #endregion

        #region İndirimler
        public IActionResult indirimler()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var indirims = _indirimService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["indirimler"] = indirims.Data;
            return View();
        }

        [HttpPost]
        public IActionResult indirimEkle(indirim model)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId = int.Parse(kurumId);
            var result = _indirimService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult indirimSil(int id)
        {
            var result = _indirimService.Delete(id);
            return Json(result);
        }
        #endregion

        #region Bursluluk Sınavları
        public IActionResult BurslulukSinavlari()
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            var sinavlar = _burslulukSinavService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["sinavlar"] = sinavlar.Data;
            return View();
        }

        [HttpPost]
        public IActionResult BurslulukSinavEkle(BurslulukSinav model)
        {
            var kurumId = _contextAccessor.HttpContext.Session.GetString("KurumId");
            model.KurumId=int.Parse(kurumId);
            var result = _burslulukSinavService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult BurslulukSinavSil(int id)
        {
            var result = _burslulukSinavService.Delete(id);
            return Json(result);
        }
        #endregion

        #region Bursluluk Başvuru
        public IActionResult Basvurular(int id)
        {
            var result = _burslulukSinavBasvuruService.GetAllBySinavId(id);
            ViewData["Basvurular"] = result.Data;
            var sinav = _burslulukSinavService.GetByIdDisplay(id);
            ViewData["Sinav"] = sinav.Data;
            return View();
        }

        [HttpPost]
        public IActionResult SinavBasvuru(BurslulukSinavBasvuru model)
        {
            var result = _burslulukSinavBasvuruService.Add(model);
            return Json(result);
        }
        #endregion

    }
}
