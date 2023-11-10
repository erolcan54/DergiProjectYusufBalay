using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfindirimDal : EfEntityRepositoryBase<indirim, EfContext>, IindirimDal
    {
        public List<indirimDisplayDto> GetAllByKurumId(int id)
        {
            using (var context=new EfContext())
            {
                var result = from ind in context.indirim
                             where ind.KurumId == id && ind.Status
                             from kurum in context.Okul
                             where kurum.Id == id
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new indirimDisplayDto
                             {
                                 CreatedDate=ind.CreatedDate,
                                 DeletedDate=ind.DeletedDate,
                                 Id=ind.Id,
                                 ilAdi=ils.Ad,
                                 ilceAdi=ilces.Ad,
                                 indirimOrani= ind.indirimOrani,
                                 KurumId=ind.KurumId,
                                 SonTarih = ind.SonTarih,
                                 Status= ind.Status,
                                UpdatedDate = ind.UpdatedDate,
                                KurumAdi=kurum.Ad,
                                Resim=kurum.Resim
                             };
                return result.ToList();
            }
        }

        public List<indirimDisplayDto> GetAllDisplay4Take()
        {
            using (var context = new EfContext())
            {
                var result = from ind in context.indirim.OrderByDescending(a=>a.Id).Take(4)
                             where ind.Status && ind.SonTarih>DateTime.Now
                             join kurum in context.Okul on ind.KurumId equals kurum.Id
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new indirimDisplayDto
                             {
                                 CreatedDate = ind.CreatedDate,
                                 DeletedDate = ind.DeletedDate,
                                 Id = ind.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 indirimOrani = ind.indirimOrani,
                                 KurumId = ind.KurumId,
                                 SonTarih = ind.SonTarih,
                                 Status = ind.Status,
                                 UpdatedDate = ind.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim=kurum.Resim
                             };
                return result.ToList();
            }
        }

        public List<indirimDisplayDto> GetAllDisplay()
        {
            using (var context = new EfContext())
            {
                var result = from ind in context.indirim.OrderByDescending(a => a.Id)
                             where ind.Status && ind.SonTarih > DateTime.Now
                             join kurum in context.Okul on ind.KurumId equals kurum.Id
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new indirimDisplayDto
                             {
                                 CreatedDate = ind.CreatedDate,
                                 DeletedDate = ind.DeletedDate,
                                 Id = ind.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 indirimOrani = ind.indirimOrani,
                                 KurumId = ind.KurumId,
                                 SonTarih = ind.SonTarih,
                                 Status = ind.Status,
                                 UpdatedDate = ind.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim=kurum.Resim
                             };
                return result.ToList();
            }
        }

        public List<indirimDisplayDto> GetindirimFiltre(indirimFiltreDto filtre)
        {
            using (var context = new EfContext())
            {
                var result = from ind in context.indirim.OrderByDescending(a => a.Id)
                             where ind.Status && ind.SonTarih > DateTime.Now
                             join kurum in context.Okul on ind.KurumId equals kurum.Id
                             where kurum.ilId == filtre.ilId && kurum.ilceId == filtre.ilceId
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new indirimDisplayDto
                             {
                                 CreatedDate = ind.CreatedDate,
                                 DeletedDate = ind.DeletedDate,
                                 Id = ind.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 indirimOrani = ind.indirimOrani,
                                 KurumId = ind.KurumId,
                                 SonTarih = ind.SonTarih,
                                 Status = ind.Status,
                                 UpdatedDate = ind.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim = kurum.Resim
                             };
                return result.ToList();
            }
        }
    }
}
