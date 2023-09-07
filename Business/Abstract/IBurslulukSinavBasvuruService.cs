using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBurslulukSinavBasvuruService
    {
        IResult Add(BurslulukSinavBasvuru entity);
        IDataResult<BurslulukSinavBasvuru> GetById(int id);
        IResult Update(BurslulukSinavBasvuru entity);
        IDataResult<List<BurslulukSinavBasvuru>> GetAllBySinavId(int id);
        IResult Delete(int id);
    }
}
