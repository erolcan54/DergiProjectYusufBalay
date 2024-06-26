﻿using Business.Abstract;
using Core.Utilities.Results;
using Entities;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net.WebSockets;
using UI.Filters;

namespace UI.Controllers
{
    [Authorize]
    [TypeFilter(typeof(AdminAuthFilter))]
    public class AdminController : Controller
    {
        private IBlogService _blogService;
        private IYoneticiService _yoneticiService;
        private IOzelDersOgretmenService _ozelDersOgretmenService;
        private IilceService _ilceService;
        private IilService _ilService;
        private IBransService _bransService;
        private IOzelDersVeliBasvuruService _ozelDersVeliBasvuruService;
        private IOzelOgretmenYorumService _ozelOgretmenYorumService;
        private IOzelOgretmenYorumBegeniService _ozelOgretmenYorumBegeniService;
        private IOkulTurService _okulTurService;
        private IOkulService _okulService;
        private IEgitimTurService _egitimTurService;
        private IKullaniciService _kullaniciService;
        private IOgretmenService _ogretmenService;
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
        private IindirimService _indirimService;
        private IBurslulukSinavService _burslulukSinavService;
        private IBurslulukSinavBasvuruService _burslulukSinavBasvuruService;
        private IisBasvuruService _isBasvuruService;
        private ISliderService _sliderService;
        private IKurumsalService _kurumsalService;
        private IPopupModalService _popupModalService;
        private IUcretsizDanismanService _ucretsizDanismanService;
        public AdminController(IBlogService blogService, IYoneticiService yoneticiService, IOzelDersOgretmenService ozelDersOgretmenService, IilceService ilceService, IilService ilService, IBransService bransService, IOzelDersVeliBasvuruService ozelDersVeliBasvuruService, IOzelOgretmenYorumService ozelOgretmenYorumService, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService, IOkulTurService okulTurService, IOkulService okulService, IEgitimTurService egitimTurService, IKullaniciService kullaniciService, IOgretmenService ogretmenService, IEgitimModeliService egitimModeliService, IEgitimModeliResimService egitimModeliResimService, IBasariService basariService, IKatalogService katalogService, IIcGorselService icGorselService, IDisGorselService disGorselService, IEtkinlikService etkinlikService, IEtkinlikResimService etkinlikResimService, IKulupService kulupService, IKurumYorumService kurumYorumService, IKurumYorumBegeniService kurumYorumBegeniService, IindirimService indirimService, IBurslulukSinavService burslulukSinavService, IBurslulukSinavBasvuruService burslulukSinavBasvuruService, IisBasvuruService isBasvuruService, ISliderService sliderService, IKurumsalService kurumsalService, IPopupModalService popupModalService, IUcretsizDanismanService ucretsizDanismanService)
        {
            _blogService = blogService;
            _yoneticiService = yoneticiService;
            _ozelDersOgretmenService = ozelDersOgretmenService;
            _ilceService = ilceService;
            _ilService = ilService;
            _bransService = bransService;
            _ozelDersVeliBasvuruService = ozelDersVeliBasvuruService;
            _ozelOgretmenYorumService = ozelOgretmenYorumService;
            _ozelOgretmenYorumBegeniService = ozelOgretmenYorumBegeniService;
            _okulTurService = okulTurService;
            _okulService = okulService;
            _egitimTurService = egitimTurService;
            _kullaniciService = kullaniciService;
            _ogretmenService = ogretmenService;
            _egitimModeliService = egitimModeliService;
            _egitimModeliResimService = egitimModeliResimService;
            _basariService = basariService;
            _katalogService = katalogService;
            _icGorselService = icGorselService;
            _disGorselService = disGorselService;
            _etkinlikService = etkinlikService;
            _etkinlikResimService = etkinlikResimService;
            _kulupService = kulupService;
            _kurumYorumService = kurumYorumService;
            _kurumYorumBegeniService = kurumYorumBegeniService;
            _indirimService = indirimService;
            _burslulukSinavService = burslulukSinavService;
            _burslulukSinavBasvuruService = burslulukSinavBasvuruService;
            _isBasvuruService = isBasvuruService;
            _sliderService = sliderService;
            _kurumsalService = kurumsalService;
            _popupModalService = popupModalService;
            _ucretsizDanismanService = ucretsizDanismanService;
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

        #region Blog İşlemleri
        public IActionResult Blog()
        {
            var result = _blogService.GetAll();
            return View(result.Data.Where(a => a.Status).OrderByDescending(a => a.Id).ToList());
        }

        public IActionResult AddBlog()
        {
            Blog blog = new Blog();
            return View(blog);
        }

        [HttpPost]
        public IActionResult AddBlog(Blog model, IFormFile Resim)
        {
            using (var stream = new MemoryStream())
            {
                Resim.CopyTo(stream);
                model.Resim = stream.ToArray();
            }
            var result = _blogService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult BlogSil(int id)
        {
            var result = _blogService.Delete(id);
            return Json(result);
        }

        public IActionResult BlogGuncelle(int id)
        {
            var result = _blogService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult BlogGuncelle(Blog model, IFormFile? Resim)
        {
            var result = _blogService.GetById(model.Id);
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
                model.Resim = result.Data.Resim;
            }
            model.CreatedDate = result.Data.CreatedDate;
            model.UpdatedDate = result.Data.UpdatedDate;
            model.DeletedDate = result.Data.DeletedDate;
            model.Hit = result.Data.Hit;
            model.Status = result.Data.Status;
            var upt = _blogService.Update(model);
            return Json(upt);
        }

        public IActionResult BlogDetail(int id)
        {
            var result = _blogService.GetById(id);
            return View(result.Data);
        }
        #endregion

        #region Yönetici İşlemleri
        public IActionResult YoneticiListe()
        {
            var result = _yoneticiService.GetAll();
            return View(result.Data.OrderBy(a => a.Ad).ToList());
        }

        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAdmin(Yonetici model)
        {
            var result = _yoneticiService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult AdminSil(int id)
        {
            var result = _yoneticiService.Delete(id);
            return Json(result);
        }

        public IActionResult UpdateAdmin(int id)
        {
            var result = _yoneticiService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateAdmin(Yonetici model)
        {
            model.Status = true;
            model.UpdatedDate = DateTime.Now;
            model.DeletedDate = null;
            var result = _yoneticiService.Update(model);
            return Json(result);
        }
        #endregion

        #region Özel Öğretmen Bölümü

        public IActionResult OzelOgretmenListesi()
        {
            var result = _ozelDersOgretmenService.GetAllDisplay();
            return View(result.Data);
        }

        public IActionResult AddOzelOgretmen()
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
            OzelDersOgretmen model = new OzelDersOgretmen();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOzelOgretmen(OzelDersOgretmen model, IFormFile Resim)
        {
            if (!model.Online && !model.Yuzyuze)
                return Json(new ErrorResult("Online veya Yüzyüze eğitimden en az biri seçilmeli."));
            if (model.ilId == 0)
                return Json(new ErrorResult("İl seçilmedi."));
            if (model.BransId == 0)
                return Json(new ErrorResult("Branş seçilmedi."));
            if (ModelState.IsValid)
                return Json(new ErrorResult("Formu eksiksiz doldurunuz."));

            using (var stream = new MemoryStream())
            {
                Resim.CopyTo(stream);
                model.Resim = stream.ToArray();
            }
            var result = _ozelDersOgretmenService.Add(model);
            return Json(result);
        }

        public IActionResult ProfileDetail(int id)
        {
            var basvuru = _ozelDersVeliBasvuruService.GetListByOgretmenId(id);
            ViewData["Basvurular"] = basvuru.Data;
            var yorums = _ozelOgretmenYorumService.GetAllByOgretmenId(id);
            ViewData["yorumlar"] = yorums.Data.OrderByDescending(a => a.Id).ToList();
            ViewData["yorumSayisi"] = _ozelOgretmenYorumService.GetCountByOgretmenId(id).Data;
            var result = _ozelDersOgretmenService.GetByIdDisplay(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult YorumSil(int id)
        {
            var result = _ozelOgretmenYorumService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult OzelOgretmenSil(int id)
        {
            var result = _ozelDersOgretmenService.Delete(id);
            return Json(result);
        }

        public IActionResult OzelOgretmenGuncelle(int id)
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
            OzelDersOgretmen model = new OzelDersOgretmen();
            var result = _ozelDersOgretmenService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult OzelOgretmenGuncelle(OzelDersOgretmen model, IFormFile Resim)
        {
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
            }
            var result = _ozelDersOgretmenService.Update(model);
            return Json(result);
        }
        #endregion

        #region Özel Ders Veli Başvuru
        public IActionResult OzelDersBasvuruSil(int id)
        {
            var result = _ozelDersVeliBasvuruService.Delete(id);
            return Json(result);
        }
        #endregion

        #region Branş İşemleri

        public IActionResult Branslar()
        {
            var result = _bransService.GetAll();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult BransSil(int id)
        {
            var result = _bransService.Delete(id);
            return Json(result);
        }

        public IActionResult AddBrans()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBrans(Brans model)
        {
            var result = _bransService.Add(model);
            return Json(result);
        }

        public IActionResult BransGuncelle(int id)
        {
            var result = _bransService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateBrans(Brans model)
        {
            var result = _bransService.Update(model);
            return Json(result);
        }

        #endregion

        #region OkulTurleri
        public IActionResult OkulTur()
        {
            var result = _okulTurService.GetAll();
            return View(result.Data);
        }


        [HttpPost]
        public IActionResult AddOkulTur(OkulTur model)
        {
            var result = _okulTurService.Add(model);
            return Json(result);
        }

        public IActionResult OkulTurGuncelle(int id)
        {
            var result = _okulTurService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateOkulTur(OkulTur model)
        {
            var result = _okulTurService.Update(model);
            return Json(result);
        }

        #endregion

        #region Kurum İşlemleri
        public IActionResult KurumListesi()
        {
            var result = _okulService.GetAllKurum();
            return View(result.Data);
        }

        public IActionResult AddKurum()
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

            var egitimTurleri = _egitimTurService.GetAll();
            var egitimTurSelectList = (from i in egitimTurleri.Data
                                       select new SelectListItem
                                       {
                                           Text = i.Ad.ToUpper(),
                                           Value = i.Id.ToString()
                                       }).ToList();
            ViewData["egitimTurleri"] = egitimTurSelectList;

            Okul model = new Okul();
            return View(model);
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

        [HttpPost]
        public IActionResult AddKurum(Okul model, IFormFile Resim)
        {
            if (model.ilId == 0)
                return Json(new ErrorResult("İl seçmediniz."));
            if (model.EgitimTurId == 0)
                return Json(new ErrorResult("Kurum Türüni seçmediniz."));
            if (model.EgitimTurId != 1)
                model.OkulTurId = 0;
            if (model.EgitimTurId == 1 && model.OkulTurId == 0)
                return Json(new ErrorResult("Okul Türü seçmediniz."));


            using (var stream = new MemoryStream())
            {
                Resim.CopyTo(stream);
                model.Resim = stream.ToArray();
            }
            var result = _okulService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public IActionResult KurumSil(int id)
        {
            var result = _okulService.Delete(id);
            return Json(result);
        }

        public IActionResult KurumGuncelle(int id)
        {
            var result = _okulService.GetById(id);
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
            var resultilce = _ilceService.GetByilIdToList(result.Data.ilId);
            List<SelectListItem> ilceListe = (from i in resultilce.Data
                                              select new SelectListItem
                                              {
                                                  Text = i.Ad.ToUpper(),
                                                  Value = i.Id.ToString()
                                              }).ToList();
            ViewData["ilceler"] = ilceListe;

            return View(result.Data);
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

        public IActionResult KurumDetay(int id)
        {
            var result = _okulService.GetByIdDisplay(id);
            return View(result.Data);
        }
        #endregion

        #region Kullanıcı İşlemleri
        public IActionResult KullaniciListesi(int id)
        {
            var result = _kullaniciService.GetAllByIdKurum(id);
            ViewData["Kullanicilar"] = result.Data;
            var kurum = _okulService.GetByIdDisplay(id);
            return View(kurum.Data);
        }

        [HttpPost]
        public IActionResult KullaniciSil(int id)
        {
            var result = _kullaniciService.Delete(id);
            return Json(result);
        }

        public IActionResult KullaniciEkle(int id)
        {
            var result = _okulService.GetById(id);
            ViewData["Kurum"] = result.Data;
            Kullanici k = new Kullanici();
            return View(k);
        }

        [HttpPost]
        public IActionResult KullaniciEkle(Kullanici model)
        {
            var result = _kullaniciService.Add(model);
            return Json(result);
        }

        public IActionResult KullaniciGuncelle(int kurumId, int kullaniciId)
        {
            var kurum = _okulService.GetById(kurumId);
            ViewData["Kurum"] = kurum.Data;
            var result = _kullaniciService.GetById(kullaniciId);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult KullaniciGuncelle(Kullanici model)
        {
            var result = _kullaniciService.Update(model);
            return Json(result);
        }
        #endregion

        #region Öğretmen İşlemleri
        public IActionResult OgretmenListesi(int id)
        {
            var result = _ogretmenService.GetAllGetByKurumId(id);
            ViewData["Ogretmenler"] = result.Data;
            var kurum = _okulService.GetByIdDisplay(id);
            return View(kurum.Data);
        }

        public IActionResult OgretmenEkle(int id)
        {
            var result = _okulService.GetById(id);
            ViewData["Kurum"] = result.Data;
            List<SelectListItem> branss = (from i in _bransService.GetAll().Data
                                           select new SelectListItem
                                           {
                                               Text = i.Ad.ToUpper(),
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewData["branslar"] = branss;
            Ogretmen ogretmen = new Ogretmen();
            return View(ogretmen);

        }

        [HttpPost]
        public IActionResult OgretmenEkle(Ogretmen model, IFormFile Resim)
        {
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

        public IActionResult OgretmenGuncelle(int kurumId, int ogretmenId)
        {
            var result = _ogretmenService.GetById(ogretmenId);
            var kurum = _okulService.GetById(kurumId);
            ViewData["Kurum"] = kurum.Data;
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
        #endregion

        #region EgitimModeli
        public IActionResult EgitimModeli(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var egitimModel = _egitimModeliService.GetByKurumId(id);
            ViewData["EgitimModeli"] = egitimModel.Data;
            var resimler = _egitimModeliResimService.GetAllByKurumId(id);
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
        public IActionResult EgitimModeliIcerikGuncelle(int kurumId, string Icerik1)
        {
            var model = _egitimModeliService.GetByKurumId(kurumId);
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
        #endregion

        #region Başarı
        public IActionResult Basarilar(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var basariListe = _basariService.GetAllByKurumId(id);
            ViewData["Basarilar"] = basariListe.Data;
            return View();
        }

        [HttpPost]
        public IActionResult BasariEkle(Basari model, IFormFile Resim)
        {
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
        #endregion

        #region Kataloglar
        public IActionResult Kataloglar(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var katalogliste = _katalogService.GetAllByKurumId(id);
            ViewData["Kataloglar"] = katalogliste.Data;
            return View();
        }

        [HttpPost]
        public IActionResult KatalogEkle(Katalog model, IFormFile katalogpdf,IFormFile Resim)
        {

            if (katalogpdf != null || Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    katalogpdf.CopyTo(stream);
                    model.KatalogPDF = stream.ToArray();
                }
                using (var streamResim = new MemoryStream())
                {
                    Resim.CopyTo(streamResim);
                    model.Resim = streamResim.ToArray();
                }
                model.SeriNo = Guid.NewGuid();
                var result = _katalogService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Katalog için pdf veya resim seçiniz..."));
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
        #endregion

        #region İç görseller
        public IActionResult IcGorseller(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var gorseller = _icGorselService.GetAllByKurumId(id);
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
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var gorseller = _disGorselService.GetAllByKurumId(id);
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
        public IActionResult Etkinlikler(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var etkinlikler = _etkinlikService.GetAllByKurumId(id);
            ViewData["Etkinlikler"] = etkinlikler.Data;
            return View();
        }

        [HttpPost]
        public IActionResult EtkinlikEkle(Etkinlik model,IFormFile Resim)
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
                return Json(new ErrorResult("Resim seçmediniz."));

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
            var kurum = _okulService.GetById(result.Data.KurumId);
            ViewData["Kurum"] = kurum.Data;
            return View();
        }

        [HttpPost]
        public IActionResult EtkinlikSil(int id)
        {
            var result = _etkinlikService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult EtkinlikGuncelle(Etkinlik model, IFormFile Resim)
        {
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
            }
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
        public IActionResult Kulupler(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var kulupler = _kulupService.GetAllByKurumId(id);
            ViewData["Kulupler"] = kulupler.Data;
            return View();
        }

        [HttpPost]
        public IActionResult KulupEkle(Kulup model, IFormFile Resim)
        {
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
            var kurum = _okulService.GetById(kulup.Data.KurumId);
            ViewData["Kurum"] = kurum.Data;
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

        #region TanıtımVideosu
        public IActionResult TanitimVideo(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            return View();
        }

        [HttpPost]
        public IActionResult VideoEkle(int kurumId, string TanitimVideo)
        {
            var result = _okulService.GetById(kurumId);
            result.Data.TanitimVideo = TanitimVideo;
            var guncelle = _okulService.Update(result.Data);
            return Json(guncelle);
        }

        [HttpPost]
        public IActionResult VideoGuncelle(int kurumId, string TanitimVideo)
        {
            var result = _okulService.GetById(kurumId);
            result.Data.TanitimVideo = TanitimVideo;
            var guncelle = _okulService.Update(result.Data);
            return Json(guncelle);
        }
        #endregion

        #region Yorumlar
        public IActionResult Yorumlar(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var yorums = _kurumYorumService.GetAllByKurumId(id);
            ViewData["yorumlar"] = yorums.Data.OrderByDescending(a => a.Id).ToList();
            ViewData["yorumSayisi"] = _kurumYorumService.GetCountByKurumId(id).Data;
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
        public IActionResult indirimler(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var indirims = _indirimService.GetAllByKurumId(id);
            ViewData["indirimler"] = indirims.Data;
            return View();
        }

        [HttpPost]
        public IActionResult indirimEkle(indirim model)
        {
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
        public IActionResult BurslulukSinavlari(int id)
        {
            var kurum = _okulService.GetById(id);
            ViewData["Kurum"] = kurum.Data;
            var sinavlar = _burslulukSinavService.GetAllByKurumId(id);
            ViewData["sinavlar"] = sinavlar.Data;
            return View();
        }

        [HttpPost]
        public IActionResult BurslulukSinavEkle(BurslulukSinav model)
        {
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
            var kurum = _okulService.GetById(sinav.Data.KurumId);
            ViewData["Kurum"] = kurum.Data;
            return View();
        }

        [HttpPost]
        public IActionResult SinavBasvuru(BurslulukSinavBasvuru model)
        {
            var result = _burslulukSinavBasvuruService.Add(model);
            return Json(result);
        }
        #endregion

        #region İş Başvuru
        public IActionResult isBasvuru()
        {
            var result = _isBasvuruService.GetAllDisplay();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult isBasvuruSil(int id)
        {
            var result = _isBasvuruService.Delete(id);
            return Json(result);
        }

        public IActionResult isBasvuruGuncelle(int id)
        {
            var result = _isBasvuruService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult isBasvuruGuncelle(isBasvuru model, IFormFile CvPDF)
        {
            var isb = _isBasvuruService.GetById(model.Id);
            if (CvPDF != null)
            {
                using (var stream = new MemoryStream())
                {
                    CvPDF.CopyTo(stream);
                    model.CvPDF = stream.ToArray();
                }
            }
            else
            {
                model.CvPDF = isb.Data.CvPDF;
            }
            var result = _isBasvuruService.Update(model);

            return Json(result);
        }

        public IActionResult CvIndir(int Id)
        {
            var result = _isBasvuruService.GetById(Id);
            return File(result.Data.CvPDF, "application/pdf", result.Data.Ad+" "+result.Data.Soyad+".pdf");
        }
        public IActionResult OgretmenDetay(int id)
        {
            var result = _isBasvuruService.GetByIdDisplay(id);
            return View(result.Data);
        }
        #endregion

        #region Slider
        public IActionResult Slider()
        {
            var sliders = _sliderService.GetAll();
            return View(sliders.Data);
        }

        [HttpPost]
        public IActionResult SliderResimEkle(IFormFile Resim)
        {
            Slider s = new Slider();
            s.Status = true;
            s.CreatedDate = DateTime.Now;
            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    s.Resim = stream.ToArray();
                }
                var result = _sliderService.Add(s);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Resim seçmediniz."));
        }

        [HttpPost]
        public IActionResult SliderResimSil(int id)
        {
            var result = _sliderService.Delete(id);
            return Json(result);
        }
        #endregion

        #region Kurumsal
        public IActionResult Kurumsal()
        {
            var result = _kurumsalService.GetAll();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult KurumsalBilgiGuncelle(Kurumsal model)
        {
            var result = _kurumsalService.Update(model);
            return Json(result);
        }
        #endregion

        #region Popup
        public IActionResult PopupModal()
        {
            var result = _popupModalService.GetAll();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult PopupModalEkle(PopupModal model, IFormFile Resim)
        {
            if (String.IsNullOrEmpty(model.SonTarih.ToString()))
                return Json(new ErrorResult("Son gösterim tarihi seçmediniz."));

            if (Resim != null)
            {
                using (var stream = new MemoryStream())
                {
                    Resim.CopyTo(stream);
                    model.Resim = stream.ToArray();
                }
                var result = _popupModalService.Add(model);
                return Json(result);
            }
            else
                return Json(new ErrorResult("Resim seçmediniz."));
        }

        [HttpPost]
        public IActionResult PopupModalSil(int id)
        {
            var result = _popupModalService.Delete(id);
            return Json(result);
        }
        #endregion

        #region UcretsizDanisman
        public IActionResult UcretsizDanisman()
        {
            var result = _ucretsizDanismanService.GetAllDisplay();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UcretsizDanismanBasvuruSil(int id)
        {
            var result=_ucretsizDanismanService.Delete(id);
            return Json(result);
        }
        #endregion
    }
}
