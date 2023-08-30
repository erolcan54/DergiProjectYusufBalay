using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IKatalogService
    {
        IResult Add(Katalog entity);
        IDataResult<Katalog> GetById(int id);
        IDataResult<Katalog> GetBySeriNo(Guid seriNo);
        IResult Update(Katalog entity);
        IDataResult<List<Katalog>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
