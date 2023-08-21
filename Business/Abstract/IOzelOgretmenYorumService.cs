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
    public interface IOzelOgretmenYorumService
    {
        IResult Add(OzelOgretmenYorum entity);
        IDataResult<OzelOgretmenYorum> GetById(int id);
        IResult Update(OzelOgretmenYorum entity);
        IDataResult<List<OzelOgretmenYorum>> GetAll();
        IResult Delete(int id);
        IDataResult<List<YorumDisplayDto>> GetAllByOgretmenId(int id);
        IDataResult<int> GetCountByOgretmenId(int id);
        IDataResult<int> GetAvaregeYorumId(int id);

    }
}
