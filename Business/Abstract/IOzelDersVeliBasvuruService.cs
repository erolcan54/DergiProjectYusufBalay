using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOzelDersVeliBasvuruService
    {
        IResult Add(OzelDersVeliBasvuru entity);
        IDataResult<OzelDersVeliBasvuru> GetById(int id);
        IResult Update(OzelDersVeliBasvuru entity);
        IDataResult<List<OzelDersVeliBasvuru>> GetAll();
        IResult Delete(int id);
        IDataResult<List<OzelDersVeliBasvuru>> GetListByOgretmenId(int id);
    }
}
