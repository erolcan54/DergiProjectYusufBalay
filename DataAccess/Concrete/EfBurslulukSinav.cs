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
    public class EfBurslulukSinav : EfEntityRepositoryBase<BurslulukSinav, EfContext>, IBurslulukSinavDal
    {
        public BurslulukSinavDisplayDto GetByIdDisplay(int id)
        {
            using (var context = new EfContext())
            {
                var result = from brs in context.BurslulukSinav
                             where brs.Status && brs.Id==id
                             join kurum in context.Okul on brs.KurumId equals kurum.Id
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new BurslulukSinavDisplayDto
                             {
                                 CreatedDate = brs.CreatedDate,
                                 DeletedDate = brs.DeletedDate,
                                 Id = brs.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 SinavAdi = brs.SinavAdi,
                                 KurumId = brs.KurumId,
                                 SonBasvuruTarih = brs.SonBasvuruTarih,
                                 Status = brs.Status,
                                 UpdatedDate = brs.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim = kurum.Resim,
                                 SinavSaat = brs.SinavSaat,
                                 SinavTarih = brs.SinavTarih,
                             };
                return result.FirstOrDefault();
            }
        }

        public List<BurslulukSinavDisplayDto> GetAllDisplay4Take()
        {
            using (var context = new EfContext())
            {
                var result = from brs in context.BurslulukSinav.OrderByDescending(a => a.Id).Take(4)
                             where brs.Status && brs.SonBasvuruTarih > DateTime.Now
                             join kurum in context.Okul on brs.KurumId equals kurum.Id
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new BurslulukSinavDisplayDto
                             {
                                 CreatedDate = brs.CreatedDate,
                                 DeletedDate = brs.DeletedDate,
                                 Id = brs.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 SinavAdi = brs.SinavAdi,
                                 KurumId = brs.KurumId,
                                 SonBasvuruTarih = brs.SonBasvuruTarih,
                                 Status = brs.Status,
                                 UpdatedDate = brs.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim = kurum.Resim,
                                 SinavSaat= brs.SinavSaat,
                                 SinavTarih= brs.SinavTarih,
                             };
                return result.ToList();
            }
        }

        public List<BurslulukSinavDisplayDto> GetAllDisplay()
        {
            using (var context = new EfContext())
            {
                var result = from brs in context.BurslulukSinav.OrderByDescending(a => a.Id)
                             where brs.Status && brs.SonBasvuruTarih > DateTime.Now
                             join kurum in context.Okul on brs.KurumId equals kurum.Id
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new BurslulukSinavDisplayDto
                             {
                                 CreatedDate = brs.CreatedDate,
                                 DeletedDate = brs.DeletedDate,
                                 Id = brs.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 SinavAdi = brs.SinavAdi,
                                 KurumId = brs.KurumId,
                                 SonBasvuruTarih = brs.SonBasvuruTarih,
                                 Status = brs.Status,
                                 UpdatedDate = brs.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim = kurum.Resim,
                                 SinavSaat = brs.SinavSaat,
                                 SinavTarih = brs.SinavTarih,
                             };
                return result.ToList();
            }
        }

        public List<BurslulukSinavDisplayDto> GetBurslulukSinavFiltre(BurslulukSinavFiltreDto filtre)
        {
            using (var context = new EfContext())
            {
                var result = from brs in context.BurslulukSinav.OrderByDescending(a => a.Id)
                             where brs.Status && brs.SonBasvuruTarih > DateTime.Now
                             join kurum in context.Okul on brs.KurumId equals kurum.Id
                             where kurum.ilId==filtre.ilId && kurum.ilceId==filtre.ilceId
                             join ils in context.il on kurum.ilId equals ils.Id
                             join ilces in context.ilce on kurum.ilceId equals ilces.Id
                             select new BurslulukSinavDisplayDto
                             {
                                 CreatedDate = brs.CreatedDate,
                                 DeletedDate = brs.DeletedDate,
                                 Id = brs.Id,
                                 ilAdi = ils.Ad,
                                 ilceAdi = ilces.Ad,
                                 SinavAdi = brs.SinavAdi,
                                 KurumId = brs.KurumId,
                                 SonBasvuruTarih = brs.SonBasvuruTarih,
                                 Status = brs.Status,
                                 UpdatedDate = brs.UpdatedDate,
                                 KurumAdi = kurum.Ad,
                                 Resim = kurum.Resim,
                                 SinavSaat = brs.SinavSaat,
                                 SinavTarih = brs.SinavTarih,
                             };
                return result.ToList();
            }
        }
    }
}
