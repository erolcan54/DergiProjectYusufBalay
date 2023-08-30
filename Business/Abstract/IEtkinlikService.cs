using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEtkinlikService
    {
        IResult Add(Etkinlik entity);
        IDataResult<Etkinlik> GetById(int id);
        IResult Update(Etkinlik entity);
        IDataResult<List<Etkinlik>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
