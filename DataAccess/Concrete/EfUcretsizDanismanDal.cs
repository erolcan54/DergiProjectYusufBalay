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
    public class EfUcretsizDanismanDal : EfEntityRepositoryBase<UcretsizDanisman, EfContext>, IUcretsizDanismanDal
    {
        public List<UcretsizDanismanDisplayDto> GetAllDisplay()
        {
            using (var context = new EfContext())
            {
                var result = from ud in context.UcretsizDanisman.OrderByDescending(a=>a.Id)
                             where ud.Status
                             join iltable in context.il on ud.ilId equals iltable.Id
                             join ilceTable in context.ilce on ud.ilceId equals ilceTable.Id
                             select new UcretsizDanismanDisplayDto
                             {
                                 AdSoyad=ud.AdSoyad,
                                 CreatedDate=ud.CreatedDate,
                                 DeletedDate=ud.DeletedDate,
                                 Email=ud.Email,
                                 Id=ud.Id,
                                 ilAdi=iltable.Ad,
                                 ilceAdi=ilceTable.Ad,
                                 ilceId=ud.ilceId,
                                 ilId=ud.ilceId,
                                 KursMu=ud.KursMu,
                                 KVKK=ud.KVKK,
                                 OkulMu=ud.OkulMu,
                                 Sinif = ud.Sinif,
                                 Status=ud.Status,
                                 Telefon=ud.Telefon,
                                 UpdatedDate = ud.UpdatedDate
                             };
                return result.ToList();

            }
        }
    }
}
