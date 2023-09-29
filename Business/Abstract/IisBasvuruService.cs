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
    public interface IisBasvuruService
    {
        IResult Add(isBasvuru entity);
        IDataResult<isBasvuru> GetById(int id);
        IDataResult<isBasvuruDisplayDto> GetByIdDisplay(int id);
        IResult Update(isBasvuru entity);
        IDataResult<List<isBasvuru>> GetAll();
        IResult Delete(int id);
        IDataResult<List<isBasvuruDisplayDto>> GetAllByilId(int id);
        IDataResult<List<isBasvuruDisplayDto>> GetAllDisplay();
    }
}
