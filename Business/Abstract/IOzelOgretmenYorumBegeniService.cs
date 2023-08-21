using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOzelOgretmenYorumBegeniService
    {
        IResult Add(OzelOgretmenYorumBegeni entity);
        IDataResult<OzelOgretmenYorumBegeni> GetById(int id);
        IResult Update(OzelOgretmenYorumBegeni entity);
        IDataResult<List<OzelOgretmenYorumBegeni>> GetAll();
        IResult Delete(int id);
        IDataResult<List<OzelOgretmenYorumBegeni>> GetAllByYorumId(int id);
        IDataResult<int> GetByYorumIdBegeniAvarege(int id);
        IResult IPAddressControltoYorum(int yorumId, string IPAddress);
    }
}
