using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEtkinlikResimService
    {
        IResult Add(EtkinlikResim entity);
        IDataResult<EtkinlikResim> GetById(int id);
        IResult Update(EtkinlikResim entity);
        IDataResult<List<EtkinlikResim>> GetAllByEtkinlikId(int id);
        IResult Delete(int id);
    }
}
