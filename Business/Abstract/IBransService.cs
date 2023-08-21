using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBransService
    {
        IResult Add(Brans entity);
        IDataResult<Brans> GetById(int id);
        IResult Update(Brans entity);
        IDataResult<List<Brans>> GetAll();
    }
}
