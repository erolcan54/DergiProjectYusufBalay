using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDisGorselService
    {
        IResult Add(DisGorsel entity);
        IDataResult<DisGorsel> GetById(int id);
        IResult Update(DisGorsel entity);
        IDataResult<List<DisGorsel>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
