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
                             where isbasvuru.Status
                             select new isBasvuruDisplayDto
                             {
                                 Ad = isbasvuru.Ad,
                                 Soyad = isbasvuru.Soyad,
                                 CreatedDate = isbasvuru.CreatedDate,
                                 DeletedDate = isbasvuru.DeletedDate,
                                 Id = isbasvuru.Id,
                                 KVKK = isbasvuru.KVKK,
                                 Status = isbasvuru.Status,
                                 Telefon = isbasvuru.Telefon,
                                 UpdatedDate = isbasvuru.UpdatedDate,
                             };
                return result.ToList();
            }
        }

        public List<isBasvuruDisplayDto> GetAllDisplay()
        {
            using (var context = new EfContext())
            {
                var result = from isbasvuru in context.isBasvuru
                             where isbasvuru.Status
                             select new isBasvuruDisplayDto
                             {
                                 Ad = isbasvuru.Ad,
                                 Soyad = isbasvuru.Soyad,
                                 CreatedDate = isbasvuru.CreatedDate,
                                 DeletedDate = isbasvuru.DeletedDate,
                                 Id = isbasvuru.Id,
                                 KVKK = isbasvuru.KVKK,
                                 Status = isbasvuru.Status,
                                 Telefon = isbasvuru.Telefon,
                                 UpdatedDate = isbasvuru.UpdatedDate,
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
                             select new isBasvuruDisplayDto
                             {
                                 Ad = isbasvuru.Ad,
                                 Soyad = isbasvuru.Soyad,
                                 CreatedDate = isbasvuru.CreatedDate,
                                 DeletedDate = isbasvuru.DeletedDate,
                                 Id = isbasvuru.Id,
                                 KVKK = isbasvuru.KVKK,
                                 Status = isbasvuru.Status,
                                 Telefon = isbasvuru.Telefon,
                                 UpdatedDate = isbasvuru.UpdatedDate,
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
