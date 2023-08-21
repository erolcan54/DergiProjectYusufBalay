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
    public interface IOzelDersOretmenDal: IEntityRepository<OzelDersOgretmen>
    {
        List<OzelDersOgretmenDto> GetAllDisplay4Take();
        List<OzelDersOgretmenDto> GetAllDisplay();
        OzelDersOgretmenDto GetByIdDisplay(int id);
    }
}
