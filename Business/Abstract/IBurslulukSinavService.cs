using Core.Utilities.Results;
using Entities;
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
        IDataResult<List<BurslulukSinav>> GetAll();
    }
}
