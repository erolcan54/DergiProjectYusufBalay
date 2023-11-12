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
        IDataResult<KurumDisplayDto> GetByIdDisplay(int id);
        IResult Update(Okul entity);
        IDataResult<List<KurumDisplayDto>> GetAllKurum();
        IResult Delete(int id);
        IDataResult<List<KurumDisplayDto>> GetOkulListFilter(OkulAraDto model);
        IDataResult<List<KurumDisplayDto>> GetKursListFilter(KursAraDto model);
        IDataResult<List<KurumDisplayDto>> GetTikKurum4Take();
    }
}
