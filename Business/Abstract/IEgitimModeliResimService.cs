using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEgitimModeliResimService
    {
        IResult Add(KurumEgitimModeliResim entity);
        IDataResult<KurumEgitimModeliResim> GetById(int id);
        IResult Update(KurumEgitimModeliResim entity);
        IDataResult<List<KurumEgitimModeliResim>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
