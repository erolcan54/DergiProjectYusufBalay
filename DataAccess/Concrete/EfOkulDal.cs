using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfOkulDal : EfEntityRepositoryBase<Okul, EfContext>, IOkulDal
    {
        public List<KurumDisplayDto> GetAllKurum()
        {
            using (EfContext context = new EfContext())
            {
                var result = from k in context.Okul
                             join il in context.il on k.ilId equals il.Id
                             join ilce in context.ilce on k.ilceId equals ilce.Id
                             join egitimTur in context.EgitimTur on k.EgitimTurId equals egitimTur.Id
                             select new KurumDisplayDto
                             {
                                 Id = k.Id,
                                 Ad = k.Ad,
                                 Adres = k.Adres,
                                 EgitimTurAdi = egitimTur.Ad,
                                 EgitimTurId = k.EgitimTurId,
                                 Email = k.Email,
                                 ilAdi = il.Ad,
                                 ilceAdi = ilce.Ad,
                                 ilceId = k.ilceId,
                                 ilId = k.ilId,
                                 Resim = k.Resim,
                                 Telefon = k.Telefon,
                                 TikSayisi = k.TikSayisi,
                                 Web = k.Web,
                                 CreatedDate=k.CreatedDate,
                                 DeletedDate=k.DeletedDate,
                                 Status= k.Status,
                                 UpdatedDate=k.UpdatedDate,
                                 Hakkinda=k.Hakkinda,
                                 TanitimVideo=k.TanitimVideo,
                                 Facebook=k.Facebook,
                                 Instagram=k.Instagram,
                                 Twitter=k.Twitter,
                                 Whatsapp = k.Whatsapp,
                                 KVKK=k.KVKK,
                             };
                return result.ToList();

            }
        }

        public List<KurumDisplayDto> GetTikKurum4Take()
        {
            using (EfContext context = new EfContext())
            {
                var result = from k in context.Okul.OrderByDescending(a => a.TikSayisi).Take(4)
                             join il in context.il on k.ilId equals il.Id
                             join ilce in context.ilce on k.ilceId equals ilce.Id
                             join egitimTur in context.EgitimTur on k.EgitimTurId equals egitimTur.Id
                             select new KurumDisplayDto
                             {
                                 Id = k.Id,
                                 Ad = k.Ad,
                                 Adres = k.Adres,
                                 EgitimTurAdi = egitimTur.Ad,
                                 EgitimTurId = k.EgitimTurId,
                                 Email = k.Email,
                                 ilAdi = il.Ad,
                                 ilceAdi = ilce.Ad,
                                 ilceId = k.ilceId,
                                 ilId = k.ilId,
                                 Resim = k.Resim,
                                 Telefon = k.Telefon,
                                 TikSayisi = k.TikSayisi,
                                 Web = k.Web,
                                 CreatedDate = k.CreatedDate,
                                 DeletedDate = k.DeletedDate,
                                 Status = k.Status,
                                 UpdatedDate = k.UpdatedDate,
                                 Hakkinda = k.Hakkinda,
                                 TanitimVideo = k.TanitimVideo,
                                 Facebook = k.Facebook,
                                 Instagram = k.Instagram,
                                 Twitter = k.Twitter,
                                 Whatsapp = k.Whatsapp,
                                 KVKK = k.KVKK,
                             };
                return result.ToList();

            }
        }

        //public List<KurumDisplayDto> GetAllOkul()
        //{
        //    using (EfContext context = new EfContext())
        //    {
        //        var result = from k in context.Okul.Where(a=> a.EgitimTurId == 1)
        //                     join il in context.il on k.ilId equals il.Id
        //                     join ilce in context.ilce on k.ilceId equals ilce.Id
        //                     join okulTur in context.OkulTur on k.OkulTurId equals okulTur.Id
        //                     join egitimTur in context.EgitimTur on k.EgitimTurId equals egitimTur.Id
        //                     select new KurumDisplayDto
        //                     {
        //                         Id = k.Id,
        //                         Ad = k.Ad,
        //                         Adres = k.Adres,
        //                         EgitimTurAdi = egitimTur.Ad,
        //                         EgitimTurId = k.EgitimTurId,
        //                         Email = k.Email,
        //                         ilAdi = il.Ad,
        //                         ilceAdi = ilce.Ad,
        //                         ilceId = k.ilceId,
        //                         ilId = k.ilId,
        //                         OkulTurAdi = okulTur.Tip,
        //                         OkulTurId = k.OkulTurId,
        //                         Resim = k.Resim,
        //                         Telefon = k.Telefon,
        //                         TikSayisi = k.TikSayisi,
        //                         Web = k.Web,
        //                         CeratedDate = k.CreatedDate,
        //                         DeletedDate = k.DeletedDate,
        //                         Status = k.Status,
        //                         UpdatedDate = k.UpdatedDate,
        //                     };
        //        return result.ToList();
        //    }
        //}


        public KurumDisplayDto GetByIdDisplay(int id)
        {
            using (EfContext context = new EfContext())
            {
                var result = from k in context.Okul.Where(a => a.Id==id)
                             join il in context.il on k.ilId equals il.Id
                             join ilce in context.ilce on k.ilceId equals ilce.Id
                             join egitimTur in context.EgitimTur on k.EgitimTurId equals egitimTur.Id
                             select new KurumDisplayDto
                             {
                                 Id = k.Id,
                                 Ad = k.Ad,
                                 Adres = k.Adres,
                                 EgitimTurAdi = egitimTur.Ad,
                                 EgitimTurId = k.EgitimTurId,
                                 Email = k.Email,
                                 ilAdi = il.Ad,
                                 ilceAdi = ilce.Ad,
                                 ilceId = k.ilceId,
                                 ilId = k.ilId,
                                 OkulTurId=k.OkulTurId,
                                 Resim = k.Resim,
                                 Telefon = k.Telefon,
                                 TikSayisi = k.TikSayisi,
                                 Web = k.Web,
                                 CreatedDate = k.CreatedDate,
                                 DeletedDate = k.DeletedDate,
                                 Status = k.Status,
                                 UpdatedDate = k.UpdatedDate,
                                 Hakkinda = k.Hakkinda,
                                 TanitimVideo = k.TanitimVideo,
                                 Facebook = k.Facebook,
                                 Instagram = k.Instagram,
                                 Twitter = k.Twitter,
                                 Whatsapp = k.Whatsapp,
                                 KVKK = k.KVKK,
                             };
                return result.FirstOrDefault();
            }
        }

        public List<KurumDisplayDto> GetKursListFilter(KursAraDto model)
        {
            using(EfContext context=new EfContext())
            {
                var result = from k in context.Okul.Where(a => a.OkulTurId == 0 && a.EgitimTurId == 2 && a.ilId == model.il2 && a.ilceId == model.ilce2)
                             join il in context.il on k.ilId equals il.Id
                             join ilce in context.ilce on k.ilceId equals ilce.Id
                             join egitimTur in context.EgitimTur on k.EgitimTurId equals egitimTur.Id
                             select new KurumDisplayDto
                             {
                                 Id=k.Id,
                                 Ad = k.Ad,
                                 Adres = k.Adres,
                                 EgitimTurAdi = egitimTur.Ad,
                                 EgitimTurId = k.EgitimTurId,
                                 Email = k.Email,
                                 ilAdi = il.Ad,
                                 ilceAdi = ilce.Ad,
                                 ilceId = k.ilceId,
                                 ilId = k.ilId,
                                 Resim = k.Resim,
                                 Telefon = k.Telefon,
                                 TikSayisi = k.TikSayisi,
                                 Web = k.Web,
                                 CreatedDate = k.CreatedDate,
                                 DeletedDate = k.DeletedDate,
                                 Status = k.Status,
                                 UpdatedDate = k.UpdatedDate,
                                 Hakkinda = k.Hakkinda,
                                 TanitimVideo = k.TanitimVideo,
                                 Facebook = k.Facebook,
                                 Instagram = k.Instagram,
                                 Twitter = k.Twitter,
                                 Whatsapp = k.Whatsapp,
                                 KVKK = k.KVKK,
                             };
                return result.ToList();

            }
        }

        public List<KurumDisplayDto> GetOkulListFilter(OkulAraDto model)
        {
            using (EfContext context = new EfContext())
            {
                var result = from k in context.Okul.Where(a => a.OkulTurId == model.okulTurId && a.EgitimTurId == 1 && a.ilId == model.il1 && a.ilceId == model.ilce1)
                             join il in context.il on k.ilId equals il.Id
                             join ilce in context.ilce on k.ilceId equals ilce.Id
                             join okulTur in context.OkulTur on k.OkulTurId equals okulTur.Id
                             join egitimTur in context.EgitimTur on k.EgitimTurId equals egitimTur.Id
                             select new KurumDisplayDto
                             {
                                 Id=k.Id,
                                 Ad = k.Ad,
                                 Adres = k.Adres,
                                 EgitimTurAdi = egitimTur.Ad,
                                 EgitimTurId = k.EgitimTurId,
                                 Email = k.Email,
                                 ilAdi = il.Ad,
                                 ilceAdi = ilce.Ad,
                                 ilceId = k.ilceId,
                                 ilId = k.ilId,
                                 OkulTurAdi = okulTur.Tip,
                                 OkulTurId = k.OkulTurId,
                                 Resim = k.Resim,
                                 Telefon = k.Telefon,
                                 TikSayisi = k.TikSayisi,
                                 Web = k.Web,
                                 CreatedDate = k.CreatedDate,
                                 DeletedDate = k.DeletedDate,
                                 Status = k.Status,
                                 UpdatedDate = k.UpdatedDate,
                                 Hakkinda = k.Hakkinda,
                                 TanitimVideo = k.TanitimVideo,
                                 KVKK = k.KVKK,
                             };
                return result.ToList();
            }
        }
    }
}
