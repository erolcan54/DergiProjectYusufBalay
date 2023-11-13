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
    public interface IUcretsizDanismanService
    {
        IResult Add(UcretsizDanisman entity);
        IDataResult<UcretsizDanisman> GetById(int id);
        IResult Update(UcretsizDanisman entity);
        IResult Delete(int id);
        IDataResult<List<UcretsizDanisman>> GetAll();
        IDataResult<List<UcretsizDanismanDisplayDto>> GetAllDisplay();

    }
}
