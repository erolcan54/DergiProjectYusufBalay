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
    public class EfisBasvuruDal : EfEntityRepositoryBase<isBasvuru, EfContext>, IisBasvuruDal
    {
        public List<isBasvuruDisplayDto> GetAllByilId(int id)
        {
            using (var context = new EfContext())
            {
                var result = from isbasvuru in context.isBasvuru
                             where isbasvuru.ilId == id && isbasvuru.Status
                             join brans in context.Brans on isbasvuru.BransId equals brans.Id
                             join iltable in context.il on isbasvuru.ilId equals iltable.Id
                             select new isBasvuruDisplayDto
                             {
                                 AdSoyad = isbasvuru.AdSoyad,
                                 BransAdi = brans.Ad,
                                 BransId = isbasvuru.BransId,
                                 CreatedDate = isbasvuru.CreatedDate,
                                 DeletedDate = isbasvuru.DeletedDate,
                                 Email = isbasvuru.Email,
                                 Hakkinda = isbasvuru.Hakkinda,
                                 Id = isbasvuru.Id,
                                 ilId = isbasvuru.ilId,
                                 KVKK = isbasvuru.KVKK,
                                 Resim = isbasvuru.Resim,
                                 Status = isbasvuru.Status,
                                 Telefon = isbasvuru.Telefon,
                                 UpdatedDate = isbasvuru.UpdatedDate,
                                 ilAdi = iltable.Ad
                             };
                return result.ToList();
            }
        }

        public isBasvuruDisplayDto GetByIdDisplay(int id)
        {
            using (var context = new EfContext())
            {
                var result = from isbasvuru in context.isBasvuru
                             where isbasvuru.Id == id && isbasvuru.Status
                             join brans in context.Brans on isbasvuru.BransId equals brans.Id
                             join iltable in context.il on isbasvuru.ilId equals iltable.Id
                             select new isBasvuruDisplayDto
                             {
                                 AdSoyad = isbasvuru.AdSoyad,
                                 BransAdi = brans.Ad,
                                 BransId = isbasvuru.BransId,
                                 CreatedDate = isbasvuru.CreatedDate,
                                 DeletedDate = isbasvuru.DeletedDate,
                                 Email = isbasvuru.Email,
                                 Hakkinda = isbasvuru.Hakkinda,
                                 Id = isbasvuru.Id,
                                 ilId = isbasvuru.ilId,
                                 KVKK = isbasvuru.KVKK,
                                 Resim = isbasvuru.Resim,
                                 Status = isbasvuru.Status,
                                 Telefon = isbasvuru.Telefon,
                                 UpdatedDate = isbasvuru.UpdatedDate,
                                 ilAdi = iltable.Ad
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
