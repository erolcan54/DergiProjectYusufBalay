using Business.Abstract;
using Core.Utilities.Results;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
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
        public AdminController(IBlogService blogService, IYoneticiService yoneticiService, IOzelDersOgretmenService ozelDersOgretmenService, IilceService ilceService, IilService ilService, IBransService bransService, IOzelDersVeliBasvuruService ozelDersVeliBasvuruService, IOzelOgretmenYorumService ozelOgretmenYorumService, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService, IOkulTurService okulTurService, IOkulService okulService, IEgitimTurService egitimTurService)
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

        #region Okul İşlemleri
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

    }
}
