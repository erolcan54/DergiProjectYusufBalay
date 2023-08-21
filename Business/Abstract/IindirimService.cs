using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IindirimService
    {
        IResult Add(indirim entity);
        IDataResult<indirim> GetById(int id);
        IResult Update(indirim entity);
        IDataResult<List<indirim>> GetAll();
    }
}
