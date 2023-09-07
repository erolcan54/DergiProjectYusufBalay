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
        private IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IilService ilService, IilceService ilceService, IOkulTurService okulTurService, IOkulService okulService, IKullaniciService kullaniciService, IYoneticiService yoneticiService, IBlogService blogService, IOzelDersOgretmenService ozelDersOgretmenService, IOzelDersVeliBasvuruService ozelDersVeliBasvuruService, IOzelOgretmenYorumService ozelOgretmenYorumService, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService, IHttpContextAccessor contextAccessor, IindirimService indirimService, IOgretmenService ogretmenService, IKurumBeniAraService kurumBeniAraService, IKurumYorumService kurumYorumService, IKurumYorumBegeniService kurumYorumBegeniService, IBurslulukSinavService burslulukSinavService, IBurslulukSinavBasvuruService burslulukSinavBasvuruService)
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
        }

        public IActionResult Index()
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

            var blogs = _blogService.Get4LastList();
            ViewData["Bloglar"] = blogs.Data;

            var ozeldersogretmen = _ozelDersOgretmenService.GetAllDisplay4Take();
            ViewData["OzelDersOgretmen"] = ozeldersogretmen.Data;

            var indirimler = _indirimService.GetAllDisplay4Take();
            ViewData["indirimler"] = indirimler.Data;

            var encokgorunenler = _okulService.GetAllKurum().Data.OrderByDescending(a => a.TikSayisi).Take(4).ToList();
            ViewData["EnCokTiklananlar"] = encokgorunenler;

            var sinavlar = _burslulukSinavService.GetAllDisplay4Take();
            ViewData["Sinavlar"]=sinavlar.Data;

            return View();
        }

        [HttpPost]
        public IActionResult OkulAra(OkulAraDto model)
        {
            //if (model.il1 == 0)
            //    return Json(new ErrorResult("Lütfen il seçiniz."));
            //if (model.ilce1 == 0)
            //    return Json(new ErrorResult("Lütfen ilçe seçiniz."));
            //if (model.okulTurId == 0)
            //    return Json(new ErrorResult("Lütfen okul türü seçiniz."));
            var result = _okulService.GetOkulListFilter(model);
            if (result.Success && result.Data.Count != 0)
                return View("KurumListesi", result.Data);
            //return RedirectToAction("KurumListesi", "Home", result.Data);
            else
            {
                TempData["OkulAraUyari"] = "Aranan kriterlerde okul listesi bulunamadı.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult KursAra(KursAraDto model)
        {
            //if (model.il2 == 0)
            //    return Json(new ErrorResult("Lütfen il seçiniz."));
            //if (model.ilce2 == 0)
            //    return Json(new ErrorResult("Lütfen ilçe seçiniz."));
            var result = _okulService.GetKursListFilter(model);
            if (result.Success && result.Data.Count != 0)
                return View("KurumListesi", result.Data);

            else
            {
                TempData["KursAraUyari"] = "Aranan kriterlerde kurs listesi bulunamadı.";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult KurumListesi([FromBody] List<KurumDisplayDto> liste)
        {
            return View(liste.OrderByDescending(a=>a.Id).ToList());
        }

        public IActionResult KurumDetay(int id)
        {
            var result = _okulService.GetByIdDisplay(id);
            var kurum = _okulService.GetById(id);
            kurum.Data.TikSayisi++;
            var resultupt = _okulService.Update(kurum.Data);
            var kurumYorum = _kurumYorumService.GetAllByKurumId(id);
            ViewData["Yorumlar"] = kurumYorum.Data.OrderByDescending(a=>a.Id).ToList();
            ViewData["yorumSayisi"] = _kurumYorumService.GetCountByKurumId(id).Data;
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

        public IActionResult OgretmenKadrosu(int id)
        {
            var result = _ogretmenService.GetAllGetByKurumId(id);
            return View(result.Data);
        }

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
            var result=_burslulukSinavService.GetByIdDisplay(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult SinavBasvuru(BurslulukSinavBasvuru model)
        {
            var result = _burslulukSinavBasvuruService.Add(model);
            return Json(result);
        }

        public IActionResult SinavListesi(int page=1)
        {
            var result = _burslulukSinavService.GetAllDisplay();
            var data = result.Data.Where(a => a.Status).ToPagedList(page, 2);
            return View(data);
        }

    }
}