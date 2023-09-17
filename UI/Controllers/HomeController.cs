using Business.Abstract;
using Core.Utilities.Results;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;
using UI.Filters;
using X.PagedList;
using X.PagedList.Mvc.Core;
using UI.Utilities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace UI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IilService _ilService;
        private IilceService _ilceService;
        private IOkulTurService _okulTurService;
        private IOkulService _okulService;
        private IKullaniciService _kullaniciService;
        private IYoneticiService _yoneticiService;
        private IBlogService _blogService;
        private IOzelDersOgretmenService _ozelDersOgretmenService;
        private IOzelDersVeliBasvuruService _ozelDersVeliBasvuruService;
        private IOzelOgretmenYorumService _ozelOgretmenYorumService;
        private IOzelOgretmenYorumBegeniService _ozelOgretmenYorumBegeniService;
        private IindirimService _indirimService;
        private IOgretmenService _ogretmenService;
        private IKurumBeniAraService _kurumBeniAraService;
        private IKurumYorumService _kurumYorumService;
        private IKurumYorumBegeniService _kurumYorumBegeniService;
        private IBurslulukSinavService _burslulukSinavService;
        private IBurslulukSinavBasvuruService _burslulukSinavBasvuruService;
        private IEgitimModeliService _egitimModeliService;
        private IEgitimModeliResimService _egitimModeliResimService;
        private IBasariService _basariService;
        private IKatalogService _katalogService;
        private IIcGorselService _icGorselService;
        private IDisGorselService _disGorselService;
        private IEtkinlikService _etkinlikService;
        private IEtkinlikResimService _etkinlikResimService;
        private IKulupService _kulupService;
        private IBransService _bransService;
        private IisBasvuruService _basvuruService;
        private IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IilService ilService, IilceService ilceService, IOkulTurService okulTurService, IOkulService okulService, IKullaniciService kullaniciService, IYoneticiService yoneticiService, IBlogService blogService, IOzelDersOgretmenService ozelDersOgretmenService, IOzelDersVeliBasvuruService ozelDersVeliBasvuruService, IOzelOgretmenYorumService ozelOgretmenYorumService, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService, IHttpContextAccessor contextAccessor, IindirimService indirimService, IOgretmenService ogretmenService, IKurumBeniAraService kurumBeniAraService, IKurumYorumService kurumYorumService, IKurumYorumBegeniService kurumYorumBegeniService, IBurslulukSinavService burslulukSinavService, IBurslulukSinavBasvuruService burslulukSinavBasvuruService, IEgitimModeliResimService egitimModeliResimService, IEgitimModeliService egitimModeliService, IBasariService basariService, IKatalogService katalogService, IIcGorselService icGorselService, IDisGorselService disGorselService, IEtkinlikService etkinlikService, IEtkinlikResimService etkinlikResimService, IKulupService kulupService, IBransService bransService, IisBasvuruService basvuruService)
        {
            _logger = logger;
            _ilService = ilService;
            _ilceService = ilceService;
            _okulTurService = okulTurService;
            _okulService = okulService;
            _kullaniciService = kullaniciService;
            _yoneticiService = yoneticiService;
            _blogService = blogService;
            _ozelDersOgretmenService = ozelDersOgretmenService;
            _ozelDersVeliBasvuruService = ozelDersVeliBasvuruService;
            _ozelOgretmenYorumService = ozelOgretmenYorumService;
            _ozelOgretmenYorumBegeniService = ozelOgretmenYorumBegeniService;
            _contextAccessor = contextAccessor;
            _indirimService = indirimService;
            _ogretmenService = ogretmenService;
            _kurumBeniAraService = kurumBeniAraService;
            _kurumYorumService = kurumYorumService;
            _kurumYorumBegeniService = kurumYorumBegeniService;
            _burslulukSinavService = burslulukSinavService;
            _burslulukSinavBasvuruService = burslulukSinavBasvuruService;
            _egitimModeliResimService = egitimModeliResimService;
            _egitimModeliService = egitimModeliService;
            _basariService = basariService;
            _katalogService = katalogService;
            _icGorselService = icGorselService;
            _disGorselService = disGorselService;
            _etkinlikService = etkinlikService;
            _etkinlikResimService = etkinlikResimService;
            _kulupService = kulupService;
            _bransService = bransService;
            _basvuruService = basvuruService;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
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

            var blogs = _blogService.Get4LastList();
            ViewData["Bloglar"] = blogs.Data;

            var ozeldersogretmen = _ozelDersOgretmenService.GetAllDisplay4Take();
            ViewData["OzelDersOgretmen"] = ozeldersogretmen.Data;

            var indirimler = _indirimService.GetAllDisplay4Take();
            ViewData["indirimler"] = indirimler.Data;

            var encokgorunenler = _okulService.GetAllKurum().Data.OrderByDescending(a => a.TikSayisi).Take(4).ToList();
            ViewData["EnCokTiklananlar"] = encokgorunenler;

            var sinavlar = _burslulukSinavService.GetAllDisplay4Take();
            ViewData["Sinavlar"] = sinavlar.Data;

            return View();
        }

        [HttpPost]
        public IActionResult KurumAra(KurumAraDto model)
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
            List<KurumDisplayDto>? liste = new List<KurumDisplayDto>();
            if (model.OkulArama && !model.KursArama)
            {
                if (model.il1 == 0)
                    TempData["Uyari"] = "İl seçmediniz.";
                if (model.ilce1 == 0)
                    TempData["Uyari"] = "İlçe seçmediniz.";
                if (model.okulTurId == 0)
                    TempData["Uyari"] = "Okul türü seçmediniz.";
                OkulAraDto okara = new OkulAraDto();
                okara.okulTurId = model.okulTurId;
                okara.il1 = model.il1;
                okara.ilce1 = model.ilce1;
                liste = _okulService.GetOkulListFilter(okara).Data;
            }
            else if (!model.OkulArama && model.KursArama)
            {
                if (model.il2 == 0)
                    TempData["Uyari"] = "İl seçmediniz.";
                if (model.ilce2 == 0)
                    TempData["Uyari"] = "İlçe seçmediniz.";
                KursAraDto kursAraDto = new KursAraDto();
                kursAraDto.il2 = model.il2;
                kursAraDto.ilce2 = model.ilce2;
                liste = _okulService.GetKursListFilter(kursAraDto).Data;
            }
            ViewData["Liste"] = liste.OrderBy(a => a.Ad).ToList();
            return View();
            //return RedirectToAction("KurumListesi", "Home", new { liste = liste });
        }

        public IActionResult KurumListesi()
        {
            var result = _okulService.GetAllKurum().Data.OrderByDescending(a => a.TikSayisi).ToList();
            return View(result);
        }

        public IActionResult KurumDetay(int id)
        {
            var result = _okulService.GetByIdDisplay(id);
           
            var kurumYorum = _kurumYorumService.GetAllByKurumId(id);
            ViewData["Yorumlar"] = kurumYorum.Data.OrderByDescending(a => a.Id).ToList();
            ViewData["yorumSayisi"] = _kurumYorumService.GetCountByKurumId(id).Data;
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if(kurumId == null)
            {
                var kurum = _okulService.GetById(id);
                kurum.Data.TikSayisi++;
                var resultupt = _okulService.Update(kurum.Data);
                HttpContext.Session.SetString("HomeKurumId", result.Data.Id.ToString());
            }
                
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult KurumYorumPuanla(int deger, int id)
        {
            string address = _contextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            KurumYorumBegeni model = new KurumYorumBegeni();
            model.YorumId = id;
            model.IPAddress = address;
            model.Begeni = deger;
            var result = _kurumYorumBegeniService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult KurumYorumEkle(KurumYorum model)
        {
            var result = _kurumYorumService.Add(model);
            return RedirectToAction("KurumDetay", "Home", new { id = model.KurumId });
        }

        [HttpPost]
        public IActionResult KurumBeniArasin(KurumBeniAra model)
        {
            var result = _kurumBeniAraService.Add(model);
            return Json(result);
        }

        //public IActionResult OgretmenKadrosu(int id)
        //{
        //    var result = _ogretmenService.GetAllGetByKurumId(id);
        //    return View(result.Data);
        //}

        public IActionResult KullaniciGiris()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciGiris(GirisModelDto model)
        {
            var result = _kullaniciService.GirisKontrol(model);
            if (!result.Success)
                return Json(new ErrorResult(result.Message));

            var userClaims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            userClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));
            userClaims.AddClaim(new Claim(ClaimTypes.Name, result.Data.KullaniciAdi));
            var claimPrincipal = new ClaimsPrincipal(userClaims);
            var authProperties = new AuthenticationProperties() { IsPersistent = true };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);
            HttpContext.Session.SetString("KullaniciId", result.Data.Id.ToString());
            HttpContext.Session.SetString("KurumId", result.Data.OkulId.ToString());
            return Json(result);
        }

        public IActionResult YoneticiGiris()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> YoneticiGiris(GirisModelDto model)
        {
            var result = _yoneticiService.GirisKontrol(model);
            if (!result.Success)
                return Json(new ErrorResult(result.Message));
            HttpContext.Session.SetString("YoneticiId", result.Data.Id.ToString());

            var userClaims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            userClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));
            userClaims.AddClaim(new Claim(ClaimTypes.Name, result.Data.KullaniciAdi));
            var claimPrincipal = new ClaimsPrincipal(userClaims);
            var authProperties = new AuthenticationProperties() { IsPersistent = true };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);
            return Json(result);
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

        public IActionResult BlogDetail(int id)
        {
            var result = _blogService.GetById(id);
            result.Data.Hit++;
            _blogService.Update(result.Data);
            return View(result.Data);
        }

        public IActionResult BlogListesi(int page = 1)
        {
            var result = _blogService.GetAll();
            var data = result.Data.Where(a => a.Status).OrderByDescending(a => a.Id).ToPagedList(page, 12);
            return View(data);
        }

        public IActionResult indirimListesi(int page = 1)
        {
            var result = _indirimService.GetAllDisplay();
            var data = result.Data.Where(a => a.Status).OrderByDescending(a => a.Id).ToPagedList(page, 12);
            return View(data);
        }

        public IActionResult OzelDersOgretmenListesi(int page = 1)
        {
            var result = _ozelDersOgretmenService.GetAllDisplay();
            var data = result.Data.Where(a => a.Status).OrderByDescending(a => a.Id).ToPagedList(page, 12);
            return View(data);
        }

        public IActionResult ProfileDetail(int id)
        {
            var yorums = _ozelOgretmenYorumService.GetAllByOgretmenId(id);
            ViewData["yorumlar"] = yorums.Data.OrderByDescending(a => a.Id).ToList();
            ViewData["yorumSayisi"] = _ozelOgretmenYorumService.GetCountByOgretmenId(id).Data;
            var result = _ozelDersOgretmenService.GetByIdDisplay(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult YorumPuanla(int deger, int id)
        {
            string address = _contextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            OzelOgretmenYorumBegeni model = new OzelOgretmenYorumBegeni();
            model.YorumId = id;
            model.IPAddress = address;
            model.Begeni = deger;
            var result = _ozelOgretmenYorumBegeniService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult YorumEkle(OzelOgretmenYorum model)
        {
            var result = _ozelOgretmenYorumService.Add(model);
            return RedirectToAction("ProfileDetail", "Home", new { id = model.OzelOgretmenId });
        }
        public IActionResult OzelDersBasvuru(int id)
        {
            var result = _ozelDersOgretmenService.GetByIdDisplay(id);
            ViewData["OzelOgretmen"] = result.Data;

            OzelDersVeliBasvuru model = new OzelDersVeliBasvuru();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOzelBasvuru(OzelDersVeliBasvuru model)
        {
            if (model.Sinif == "0" || model.Sinif == null)
                return Json(new ErrorResult("Sınıf seçimi yapılmadı."));
            if (!model.Online && !model.Yuzyuze)
                return Json(new ErrorResult("Online veya Yüzyüze eğitimlerden biri seçilmeli."));
            var result = _ozelDersVeliBasvuruService.Add(model);
            return Json(result);
        }

        public IActionResult SinavDetay(int id)
        {
            var result = _burslulukSinavService.GetByIdDisplay(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult SinavBasvuru(BurslulukSinavBasvuru model)
        {
            var result = _burslulukSinavBasvuruService.Add(model);
            return Json(result);
        }

        public IActionResult SinavListesi(int page = 1)
        {
            var result = _burslulukSinavService.GetAllDisplay();
            var data = result.Data.Where(a => a.Status).ToPagedList(page, 2);
            return View(data);
        }

        public IActionResult OgretmenKadrosu()
        {
            var kurumId= HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var ogretmenKadrosu = _ogretmenService.GetAllGetByKurumId(int.Parse(kurumId));
            return View(ogretmenKadrosu.Data.OrderBy(a=>a.Ad).ToList());

        }

        public IActionResult EgitimModeli()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var model = _egitimModeliService.GetByKurumId(int.Parse(kurumId));
            var resimler = _egitimModeliResimService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["Model"] = model.Data;
            ViewData["ModelResimler"] = resimler.Data;

            return View();
        }

        public IActionResult Basarilar()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var model = _basariService.GetAllByKurumId(int.Parse(kurumId));
            return View(model.Data);
        }

        public IActionResult Kataloglar()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var katalogliste = _katalogService.GetAllByKurumId(int.Parse(kurumId));
            return View(katalogliste.Data);
        }

        public IActionResult KatalogIndir(Guid seriNo)
        {
            var result = _katalogService.GetBySeriNo(seriNo);
            return File(result.Data.KatalogPDF, "application/pdf", result.Data.KatalogAdi + ".pdf");
        }

        public IActionResult IcGorseller()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var liste = _icGorselService.GetAllByKurumId(int.Parse(kurumId));
            return View(liste.Data);
        }

        public IActionResult DisGorseller()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var liste = _disGorselService.GetAllByKurumId(int.Parse(kurumId));
            return View(liste.Data);
        }

        public IActionResult Etkinlikler()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var etkinlikler = _etkinlikService.GetAllByKurumId(int.Parse(kurumId));
            return View(etkinlikler.Data);
        }

        public IActionResult EtkinlikDetay(int id)
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");
            var etkinlik=_etkinlikService.GetById(id);
            var resimler = _etkinlikResimService.GetAllByEtkinlikId(id);
            ViewData["Resimler"] = resimler.Data;
            return View(etkinlik.Data);
        }

        public IActionResult Kulupler()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var kulupler = _kulupService.GetAllByKurumId(int.Parse(kurumId));
            return View(kulupler.Data);
        }

        public IActionResult isBasvuru()
        {
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;

            var bransListe = _bransService.GetAll();
            var bransSelectList = (from i in bransListe.Data
                                   select new SelectListItem
                                   {
                                       Text = i.Ad.ToUpper(),
                                       Value = i.Id.ToString()
                                   }).ToList();
            ViewData["branslar"] = bransSelectList;
            isBasvuru model=new isBasvuru();
            return View(model);
        }

        public IActionResult AddisBasvuru(isBasvuru model)
        {
            var result = _basvuruService.Add(model);
            return Json(result);
        }
    }
}