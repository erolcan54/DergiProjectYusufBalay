using Core.DataAccess;
using Core.Utilities.Results;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBurslulukSinavDal : IEntityRepository<BurslulukSinav>
    {
        List<BurslulukSinavDisplayDto> GetAllDisplay4Take();
        BurslulukSinavDisplayDto GetByIdDisplay(int id);

        List<BurslulukSinavDisplayDto> GetAllDisplay();
    }
}
