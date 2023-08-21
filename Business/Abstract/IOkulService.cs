using Entities.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOkulService
    {
        IResult Add(Okul entity);
        IDataResult<Okul> GetById(int id);
        IResult Update(Okul entity);
        IDataResult<List<Okul>> GetAll();
        IResult Delete(int id);
        IDataResult<List<KurumDisplayDto>> GetOkulListFilter(OkulAraDto model);
        IDataResult<List<KurumDisplayDto>> GetKursListFilter(KursAraDto model);
    }
}
