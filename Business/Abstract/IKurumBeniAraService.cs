using Core.Utilities.Results;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IKurumBeniAraService
    {
        IResult Add(KurumBeniAra entity);
        IDataResult<KurumBeniAra> GetById(int id);
        IResult Update(KurumBeniAra entity);
        IDataResult<List<KurumBeniAraDto>> GetAll();
        IResult Delete(int id);
        IDataResult<List<KurumBeniAra>> GetListByKurumId(int id);
    }
}
