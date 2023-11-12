using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISliderService
    {
        IResult Add(Slider entity);
        IDataResult<Slider> GetById(int id);
        IDataResult<List<Slider>> GetAll();
        IResult Update(Slider entity);
        IResult Delete(int id);
    }
}
