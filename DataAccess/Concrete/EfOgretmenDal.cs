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
    public class EfOgretmenDal : EfEntityRepositoryBase<Ogretmen, EfContext>, IOgretmenDal
    {
        public List<OgretmenDisplayDto> GetAllGetByKurumId(int id)
        {
            using (var context = new EfContext())
            {
                var result = from ogrt in context.Ogretmen
                             where ogrt.KurumId == id
                             join brns in context.Brans on ogrt.BransId equals brns.Id
                             select new OgretmenDisplayDto
                             {
                                 Id = ogrt.Id,
                                 BransAdi = brns.Ad,
                                 Ad = ogrt.Ad,
                                 BransId = brns.Id,
                                 CreatedDate = ogrt.CreatedDate,
                                 DeletedDate = ogrt.DeletedDate,
                                 KurumId = id,
                                 Resim = ogrt.Resim,
                                 Soyad = ogrt.Soyad,
                                 Status = ogrt.Status,
                                 UpdatedDate = ogrt.UpdatedDate,
                             };
                return result.ToList();
            }
        }
    }
}
