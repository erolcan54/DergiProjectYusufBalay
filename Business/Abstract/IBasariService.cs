using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasariService
    {
        IResult Add(Basari entity);
        IDataResult<Basari> GetById(int id);
        IResult Update(Basari entity);
        IDataResult<List<Basari>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
