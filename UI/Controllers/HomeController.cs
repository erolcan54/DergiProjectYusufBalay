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
        private IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IilService ilService, IilceService ilceService, IOkulTurService okulTurService, IOkulService okulService, IKullaniciService kullaniciService, IYoneticiService yoneticiService, IBlogService blogService, IOzelDersOgretmenService ozelDersOgretmenService, IOzelDersVeliBasvuruService ozelDersVeliBasvuruService, IOzelOgretmenYorumService ozelOgretmenYorumService, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService, IHttpContextAccessor contextAccessor)
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

            return View();
        }

        [HttpPost]
        public IActionResult OkulAra(OkulAraDto model)
        {
            if (model.il1 == 0)
                return Json(new ErrorResult("Lütfen il seçiniz."));
            if (model.ilce1 == 0)
                return Json(new ErrorResult("Lütfen ilçe seçiniz."));
            if (model.okulTurId == 0)
                return Json(new ErrorResult("Lütfen okul türü seçiniz."));
            var result = _okulService.GetOkulListFilter(model);
            if (result.Success && result.Data.Count != 0)
                return RedirectToAction("KurumListesi", "Home", result.Data);
            else
                return Json(new ErrorResult("Aranan kriterlerde okul listesi bulunamadı."));
        }

        [HttpPost]
        public IActionResult KursAra(KursAraDto model)
        {
            if (model.il2 == 0)
                return Json(new ErrorResult("Lütfen il seçiniz."));
            if (model.ilce2 == 0)
                return Json(new ErrorResult("Lütfen ilçe seçiniz."));
            var result = _okulService.GetKursListFilter(model);
            if (result.Success && result.Data.Count != 0)
                return RedirectToAction("KurumListesi", "Home", result.Data);
            else
                return Json(new ErrorResult("Aranan kriterlerde kurs listesi bulunamadı."));
        }

        public IActionResult KurumListesi(List<KurumDisplayDto> liste)
        {
            return View(liste);
        }

        public IActionResult KullaniciGiris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KullaniciGiris(GirisModelDto model)
        {
            var result = _kullaniciService.GirisKontrol(model);
            if (!result.Success)
                return Json(new ErrorResult(result.Message));
            HttpContext.Session.SetString("KullaniciId", result.Data.Id.ToString());
            return RedirectToAction("Index", "Kurum");
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
            model.Begeni= deger;
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

    }
}