using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOkulTurService
    {
        IResult Add(OkulTur entity);
        IDataResult<OkulTur> GetById(int id);
        IResult Update(OkulTur entity);
        IDataResult<List<OkulTur>> GetAll();
    }
}
