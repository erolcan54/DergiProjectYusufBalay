using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IIcGorselService
    {
        IResult Add(IcGorsel entity);
        IDataResult<IcGorsel> GetById(int id);
        IResult Update(IcGorsel entity);
        IDataResult<List<IcGorsel>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
