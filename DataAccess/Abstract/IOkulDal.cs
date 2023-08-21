using Core.DataAccess;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOkulDal : IEntityRepository<Okul>
    {
        List<KurumDisplayDto> GetOkulListFilter(OkulAraDto model);
        List<KurumDisplayDto> GetKursListFilter(KursAraDto model);
    }
}
