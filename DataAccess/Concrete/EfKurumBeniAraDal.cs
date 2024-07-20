using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfKurumBeniAraDal: EfEntityRepositoryBase<KurumBeniAra, EfContext>, IKurumBeniAraDal
    {
        public List<KurumBeniAraDto> GetAllDisplay()
        {
            using (var context = new EfContext())
            {
                var result = from kba in context.KurumBeniAra.OrderByDescending(a => a.Id)
                    where kba.Status
                    join okulTable in context.Okul on kba.KurumId equals okulTable.Id
                    join iltable in context.il on okulTable.ilId equals iltable.Id
                    join ilceTable in context.ilce on okulTable.ilceId equals ilceTable.Id
                    select new KurumBeniAraDto()
                    {
                        Id = kba.Id,
                        AdSoyad = kba.AdSoyad,
                        CreatedDate = kba.CreatedDate,
                        DeletedDate = kba.DeletedDate,
                        Email = kba.Email,
                        KVKK = kba.KVKK,
                        KurumId = kba.KurumId,
                        KurumAdi = okulTable.Ad,
                        Sinif = kba.Sinif,
                        Status = kba.Status,
                        Telefon = kba.Telefon,
                        UpdatedDate = kba.UpdatedDate,
                        ilAdi = iltable.Ad,
                        ilceAdi = ilceTable.Ad
                    };
                return result.ToList();
            }
        }
    }
}
