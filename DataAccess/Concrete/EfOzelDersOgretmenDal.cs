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
    public class EfOzelDersOgretmenDal : EfEntityRepositoryBase<OzelDersOgretmen, EfContext>, IOzelDersOretmenDal
    {
        public List<OzelDersOgretmenDto> GetAllDisplay()
        {
            using(var context=new EfContext())
            {
                var result = from odo in context.OzelDersOgretmen
                             where odo.Status
                             join iltable in context.il on odo.ilId equals iltable.Id
                             join branstable in context.Brans on odo.BransId equals branstable.Id
                             select new OzelDersOgretmenDto
                             {
                                 Id=odo.Id,
                                 AdSoyad = odo.AdSoyad,
                                 BransAdi = branstable.Ad,
                                 BransId = odo.BransId,
                                 Hakkinda = odo.Hakkinda,
                                 ilAdi = iltable.Ad,
                                 ilId = iltable.Id,
                                 Online = odo.Online,
                                 Resim = odo.Resim,
                                 Yuzyuze = odo.Yuzyuze,
                                 Email= odo.Email,
                                 Telefon= odo.Telefon,
                                 CreatedDate= odo.CreatedDate,
                                 DeletedDate= odo.DeletedDate,
                                 UpdatedDate = odo.UpdatedDate,
                                 Status= odo.Status,
                             };
                return result.ToList(); 

            }
        }

        public List<OzelDersOgretmenDto> GetAllDisplay4Take()
        {
            using (var context = new EfContext())
            {
                var result = from odo in context.OzelDersOgretmen.OrderByDescending(o => o.Id).Take(4)
                             where odo.Status
                             join iltable in context.il on odo.ilId equals iltable.Id
                             join branstable in context.Brans on odo.BransId equals branstable.Id
                             select new OzelDersOgretmenDto
                             {
                                 Id = odo.Id,
                                 AdSoyad = odo.AdSoyad,
                                 BransAdi = branstable.Ad,
                                 BransId = odo.BransId,
                                 Hakkinda = odo.Hakkinda,
                                 ilAdi = iltable.Ad,
                                 ilId = iltable.Id,
                                 Online = odo.Online,
                                 Resim = odo.Resim,
                                 Yuzyuze = odo.Yuzyuze,
                                 Email = odo.Email,
                                 Telefon = odo.Telefon,
                                 CreatedDate = odo.CreatedDate,
                                 DeletedDate = odo.DeletedDate,
                                 UpdatedDate = odo.UpdatedDate,
                                 Status = odo.Status,
                             };
                return result.ToList();

            }
        }

        public List<OzelDersOgretmenDto> GetAllOzelOgretmenFiltre(OzelOgretmenFiltreDto filtre)
        {
            using (var context = new EfContext())
            {
                var result = from odo in context.OzelDersOgretmen.OrderByDescending(o => o.Id)
                             join iltable in context.il on odo.ilId equals iltable.Id
                             join branstable in context.Brans on odo.BransId equals branstable.Id
                             where odo.Status && (iltable.Id == filtre.ilId || branstable.Id == filtre.BransId) && (odo.Online == filtre.OnlineSecenek || odo.Yuzyuze == filtre.YuzyuzeSecenek)
                             select new OzelDersOgretmenDto
                             {
                                 Id = odo.Id,
                                 AdSoyad = odo.AdSoyad,
                                 BransAdi = branstable.Ad,
                                 BransId = odo.BransId,
                                 Hakkinda = odo.Hakkinda,
                                 ilAdi = iltable.Ad,
                                 ilId = iltable.Id,
                                 Online = odo.Online,
                                 Resim = odo.Resim,
                                 Yuzyuze = odo.Yuzyuze,
                                 Email = odo.Email,
                                 Telefon = odo.Telefon,
                                 CreatedDate = odo.CreatedDate,
                                 DeletedDate = odo.DeletedDate,
                                 UpdatedDate = odo.UpdatedDate,
                                 Status = odo.Status,
                             };
                return result.ToList();
            }
        }

        public OzelDersOgretmenDto GetByIdDisplay(int id)
        {
            using (var context = new EfContext())
            {
                var result = from odo in context.OzelDersOgretmen.Where(a=>a.Id==id)
                             join iltable in context.il on odo.ilId equals iltable.Id
                             join branstable in context.Brans on odo.BransId equals branstable.Id
                             select new OzelDersOgretmenDto
                             {
                                 Id = odo.Id,
                                 AdSoyad = odo.AdSoyad,
                                 BransAdi = branstable.Ad,
                                 BransId = odo.BransId,
                                 Hakkinda = odo.Hakkinda,
                                 ilAdi = iltable.Ad,
                                 ilId = iltable.Id,
                                 Online = odo.Online,
                                 Resim = odo.Resim,
                                 Yuzyuze = odo.Yuzyuze,
                                 Email = odo.Email,
                                 Telefon = odo.Telefon,
                                 CreatedDate = odo.CreatedDate,
                                 DeletedDate = odo.DeletedDate,
                                 UpdatedDate = odo.UpdatedDate,
                                 Status = odo.Status,
                             };
                return result.FirstOrDefault();

            }
        }
    }
}
