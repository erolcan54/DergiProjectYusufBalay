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
    public interface IisBasvuruDal: IEntityRepository<isBasvuru>
    {
        List<isBasvuruDisplayDto> GetAllByilId(int id);
        isBasvuruDisplayDto GetByIdDisplay(int id);
        List<isBasvuruDisplayDto> GetAllDisplay();
    }
}
