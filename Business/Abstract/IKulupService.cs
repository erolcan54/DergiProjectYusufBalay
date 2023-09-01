using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IKulupService
    {
        IResult Add(Kulup entity);
        IDataResult<Kulup> GetById(int id);
        IResult Update(Kulup entity);
        IDataResult<List<Kulup>> GetAllByKurumId(int id);
        IResult Delete(int id);
    }
}
