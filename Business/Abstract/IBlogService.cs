using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBlogService
    {
        IResult Add(Blog entity);
        IDataResult<Blog> GetById(int id);
        IResult Update(Blog entity);
        IDataResult<List<Blog>> GetAll();
        IResult Delete(int id);
        IDataResult<List<Blog>> Get4LastList();
    }
}
