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
using Core.Utilities.Crypto;
using Core.Utilities.PasswordCreate;
using UI.Filters;
using X.PagedList;
using X.PagedList.Mvc.Core;
using UI.Utilities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static Core.Utilities.Crypto.CryptoClass;

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
        private ISliderService _sliderService;
        private IPopupModalService _popupModalService;
        private IUcretsizDanismanService _ucretsizDanismanService;
        private IUyeService _uyeService;
        private IKurumsalService _kurumsalService;
        private IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IilService ilService, IilceService ilceService, IOkulTurService okulTurService, IOkulService okulService, IKullaniciService kullaniciService, IYoneticiService yoneticiService, IBlogService blogService, IOzelDersOgretmenService ozelDersOgretmenService, IOzelDersVeliBasvuruService ozelDersVeliBasvuruService, IOzelOgretmenYorumService ozelOgretmenYorumService, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService, IHttpContextAccessor contextAccessor, IindirimService indirimService, IOgretmenService ogretmenService, IKurumBeniAraService kurumBeniAraService, IKurumYorumService kurumYorumService, IKurumYorumBegeniService kurumYorumBegeniService, IBurslulukSinavService burslulukSinavService, IBurslulukSinavBasvuruService burslulukSinavBasvuruService, IEgitimModeliResimService egitimModeliResimService, IEgitimModeliService egitimModeliService, IBasariService basariService, IKatalogService katalogService, IIcGorselService icGorselService, IDisGorselService disGorselService, IEtkinlikService etkinlikService, IEtkinlikResimService etkinlikResimService, IKulupService kulupService, IBransService bransService, IisBasvuruService basvuruService, ISliderService sliderService, IPopupModalService popupModalService, IUcretsizDanismanService ucretsizDanismanService, IUyeService uyeService, IKurumsalService kurumsalService)
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
            _sliderService = sliderService;
            _popupModalService = popupModalService;
            _ucretsizDanismanService = ucretsizDanismanService;
            _uyeService = uyeService;
            _kurumsalService = kurumsalService;
        }

        public async Task<IActionResult> Index()
        {

            var popup = _popupModalService.GetByStatus();
            ViewData["Popup"] = popup.Data;

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
            ViewData["AnasayfaMesaj"] = _kurumsalService.GetAll().Data.AnasayfaMesaj;
            return View(model);
        }

        [HttpGet]
        public IActionResult KurumAra(KurumAraDto model)
        {

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
                okara.KurumYorumSecenekOkul = model.KurumYorumSecenekOkul;
                liste = _okulService.GetOkulListFilter(okara).Data;

                var ilceListe = _ilceService.GetByilIdToList(model.il1);
                var ilceSelectList = (from i in ilceListe.Data
                                      select new SelectListItem
                                      {
                                          Text = i.Ad.ToUpper(),
                                          Value = i.Id.ToString()
                                      }).ToList();
                ViewData["ilceler"] = ilceSelectList;
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
                kursAraDto.KurumYorumSecenekKurs = model.KurumYorumSecenekKurs;
                liste = _okulService.GetKursListFilter(kursAraDto).Data;
                var ilceListe = _ilceService.GetByilIdToList(model.il2);
                var ilceSelectList = (from i in ilceListe.Data
                                      select new SelectListItem
                                      {
                                          Text = i.Ad.ToUpper(),
                                          Value = i.Id.ToString()
                                      }).ToList();
                ViewData["ilceler"] = ilceSelectList;
            }

            var okulTurleri = _okulTurService.GetAll();
            var okulTurSelectList = (from i in okulTurleri.Data
                                     select new SelectListItem
                                     {
                                         Text = i.Tip.ToUpper(),
                                         Value = i.Id.ToString()
                                     }).ToList();
            ViewData["okulTurleri"] = okulTurSelectList;

            ViewData["Liste"] = liste.OrderByDescending(a => a.TikSayisi).ToList();
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;
            return View(model);
            //return RedirectToAction("KurumListesi", "Home", new { liste = liste });
        }

        public IActionResult KurumListesi()
        {
            var result = _okulService.GetAllKurum().Data.OrderByDescending(a => a.TikSayisi).ToList();
            ViewData["Kurumlistesi"] = result;
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

        public IActionResult KurumDetay(int id)
        {
            var result = _okulService.GetByIdDisplay(id);

            var kurumYorum = _kurumYorumService.GetAllByKurumId(id);
            ViewData["Yorumlar"] = kurumYorum.Data.OrderByDescending(a => a.Id).ToList();
            ViewData["yorumSayisi"] = _kurumYorumService.GetCountByKurumId(id).Data;
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (kurumId != id.ToString())
            {
                var kurum = _okulService.GetById(id);
                kurum.Data.TikSayisi++;
                var resultupt = _okulService.Update(kurum.Data);
                HttpContext.Session.SetString("HomeKurumId", result.Data.Id.ToString());
            }
            ViewData["KurumDetay"] = result.Data;
            KurumBeniAra model = new KurumBeniAra();
            return View(model);
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
            if (!model.KVKK)
                return Json(new ErrorResult("KVKK metnini onaylamanız gerekmektedir."));
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
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;
            return View(data);
        }

        [HttpPost]
        public IActionResult indirimFiltre(indirimFiltreDto filtre)
        {
            var result = _indirimService.GetindirimFiltre(filtre);
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;
            return View(result.Data);
        }

        public IActionResult OzelDersOgretmenListesi(int page = 1)
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
            ViewData["BransListe"] = bransSelectList;
            var result = _ozelDersOgretmenService.GetAllDisplay();
            var data = result.Data.Where(a => a.Status).OrderByDescending(a => a.Id).ToPagedList(page, 12);
            return View(data);
        }

        [HttpPost]
        public IActionResult OzelOgretmenFiltre(OzelOgretmenFiltreDto filtre)
        {
            var result = _ozelDersOgretmenService.GetAllOzelOgretmenFiltre(filtre);
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
            ViewData["BransListe"] = bransSelectList;
            return View(result.Data);
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
            if (!model.KVKK)
                return Json(new ErrorResult("KVKK Aydunlatma Metni işaretlenmek zorundadır."));
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
            ViewData["SinavDetay"] = result.Data;
            BurslulukSinavBasvuru model = new BurslulukSinavBasvuru();
            return View(model);
        }

        [HttpPost]
        public IActionResult SinavBasvuru(BurslulukSinavBasvuru model)
        {
            if (!model.KVKK)
                return Json(new ErrorResult("KVKK Aydınlatma Metni işaretlenmek zorundadır."));
            var result = _burslulukSinavBasvuruService.Add(model);
            return Json(result);
        }

        public IActionResult SinavListesi(int page = 1)
        {
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;
            var result = _burslulukSinavService.GetAllDisplay();
            var data = result.Data.Where(a => a.Status).ToPagedList(page, 2);
            return View(data);
        }

        [HttpPost]
        public IActionResult BurslulukSinavFiltre(BurslulukSinavFiltreDto filtre)
        {
            var result = _burslulukSinavService.GetBurslulukSinavFiltre(filtre);
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;
            return View(result.Data);
        }

        public IActionResult OgretmenKadrosu()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var ogretmenKadrosu = _ogretmenService.GetAllGetByKurumId(int.Parse(kurumId));
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(ogretmenKadrosu.Data.OrderBy(a => a.Ad).ToList());

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
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View();
        }

        public IActionResult Basarilar()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var model = _basariService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(model.Data);
        }

        public IActionResult Kataloglar()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var katalogliste = _katalogService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(katalogliste.Data.OrderByDescending(a => a.Id).ToList());
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
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(liste.Data);
        }

        public IActionResult DisGorseller()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var liste = _disGorselService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(liste.Data);
        }

        public IActionResult Etkinlikler()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var etkinlikler = _etkinlikService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(etkinlikler.Data);
        }

        public IActionResult EtkinlikDetay(int id)
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");
            var etkinlik = _etkinlikService.GetById(id);
            var resimler = _etkinlikResimService.GetAllByEtkinlikId(id);
            ViewData["Resimler"] = resimler.Data;
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(etkinlik.Data);
        }

        public IActionResult Kulupler()
        {
            var kurumId = HttpContext.Session.GetString("HomeKurumId");
            if (String.IsNullOrEmpty(kurumId))
                return RedirectToAction("Index");

            var kulupler = _kulupService.GetAllByKurumId(int.Parse(kurumId));
            ViewData["KurumAdi"] = _okulService.GetById(int.Parse(kurumId)).Data.Ad;
            return View(kulupler.Data);
        }

        public IActionResult isBasvuru()
        {
            isBasvuru model = new isBasvuru();
            return View(model);
        }

        public IActionResult AddisBasvuru(isBasvuru model, IFormFile CvPDF)
        {
            if (!model.KVKK)
                return Json(new ErrorResult("KVKK Metni işaretlenmek zorundadır."));

            if (CvPDF != null)
            {
                using (var stream = new MemoryStream())
                {
                    CvPDF.CopyTo(stream);
                    model.CvPDF = stream.ToArray();
                }
            }
            else
                return Json(new ErrorResult("CV PDF alanı boş bırakılamaz."));
            var result = _basvuruService.Add(model);

            return Json(result);
        }

        public IActionResult UcretsizDanisman()
        {
            var ilListe = _ilService.GetAll();
            var ilSelectList = (from i in ilListe.Data
                                select new SelectListItem
                                {
                                    Text = i.Ad.ToUpper(),
                                    Value = i.Id.ToString()
                                }).ToList();
            ViewData["iller"] = ilSelectList;

            UcretsizDanisman model = new UcretsizDanisman();
            return View(model);
        }

        [HttpPost]
        public IActionResult UcretsizDanismanBasvuruEkle(UcretsizDanisman model)
        {
            if (!model.OkulMu && !model.KursMu)
                return Json(new ErrorResult("Okul veya Kurs tercihi yapmadınız."));
            if (model.Sinif == null || model.Sinif == 0)
                return Json(new ErrorResult("Sınıf tercihi yapmadınız."));
            if (model.ilId == null || model.ilId == 0)
                return Json(new ErrorResult("İl tercihi yapmadınız."));
            if (model.ilceId == null || model.ilceId == 0)
                return Json(new ErrorResult("İlçe tercihi yapmadınız."));
            if (!model.KVKK)
                return Json(new ErrorResult("KVKK tercihini işaretlemeniz gerekmektedir."));

            var result = _ucretsizDanismanService.Add(model);
            return Json(result);
        }

        public IActionResult KayitOl()
        {
            Uye model = new Uye();
            return View(model);
        }

        [HttpPost]
        public IActionResult KayitOl(Uye user,string SifreTekrar)
        {

            if(user.Sifre!=SifreTekrar)
                return Json(new ErrorResult("Yazdığınız şifreler aynı değildir. Lütfen kontrol ediniz."));
            if (!user.KVKK)
                return Json(new ErrorResult("KVKK tercihini işaretlemeniz gerekmektedir."));
            if (String.IsNullOrEmpty(user.Email) && String.IsNullOrEmpty(user.Telefon))
              return Json(new ErrorResult("Email ve telefon bilgilerinden en az biri yazılmalıdır."));

            user.Sifre = Kripto.Encrypt(user.Sifre);
            user.UyeGuid = Guid.NewGuid();
           user.CreatedDate = DateTime.Now;
           user.Status = false;
           var result=_uyeService.Add(user);
           HttpContext.Session.SetString("UyeGuid",user.UyeGuid.ToString());
           
           if (!result.Success)
               return Json(result);
           return Json(new SuccessDataResult<Uye>("Bilgileriniz kaydedildi."));
        }

        public IActionResult UserActivation()
        {
            var uye = CodeGuncelleGonder();
            if (uye!=null)
            {
                return View(uye);
            }
            else
                return RedirectToAction("KayitOl");
        }

        public Uye CodeGuncelleGonder()
        {
            string uyeGuid = HttpContext.Session.GetString("UyeGuid");
            var uye = _uyeService.GetByUyeGuid(uyeGuid);
            if (uye.Success)
            {
                string code = PasswordCr.CodeGenereate();
                if (!String.IsNullOrEmpty(uye.Data.Email))
                {
                    uye.Data.MailCode = code;
                    uye.Data.MailCodeActive = true;
                    var result = _uyeService.Update(uye.Data);
                    if (result.Success)
                    {
                        //mail gönderilecek..
                    }
                }

                return uye.Data;
            }
            else
                return null;

        }

    }
}