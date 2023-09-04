using Core.Utilities.Results;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBurslulukSinavService
    {
        IResult Add(BurslulukSinav entity);
        IDataResult<BurslulukSinav> GetById(int id);
        IResult Update(BurslulukSinav entity);
        IDataResult<List<BurslulukSinav>> GetAllByKurumId(int id);
        IResult Delete(int id);
        IDataResult<List<BurslulukSinavDisplayDto>> GetAllDisplay4Take();
        IDataResult<List<BurslulukSinavDisplayDto>> GetAllDisplay();
        IDataResult<BurslulukSinavDisplayDto> GetByIdDisplay(int id);
    }
}
