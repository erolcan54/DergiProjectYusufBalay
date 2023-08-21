using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOgretmenService
    {
        IResult Add(Ogretmen entity);
        IDataResult<Ogretmen> GetById(int id);
        IResult Update(Ogretmen entity);
        IDataResult<List<Ogretmen>> GetAll();
        IResult Delete(int id);
    }
}
