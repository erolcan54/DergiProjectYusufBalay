using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEgitimModeliService
    {
        IResult Add(KurumEgitimModeli entity);
        IDataResult<KurumEgitimModeli> GetById(int id);
        IResult Update(KurumEgitimModeli entity);
        IDataResult<KurumEgitimModeli> GetByKurumId(int id);
        IResult Delete(int id);
    }
}
