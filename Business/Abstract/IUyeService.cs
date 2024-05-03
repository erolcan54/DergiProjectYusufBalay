using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUyeService
    {
        IResult Add(Uye entity);
        IDataResult<Uye> GetById(int id);
        IResult Update(Uye entity);
        IDataResult<List<Uye>> GetAll();
        IResult Delete(int id);
        IDataResult<Uye> GetByUyeGuid(string uyeGuid);
    }
}
