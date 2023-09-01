using Core.Utilities.Results;
using Entities;

namespace Business.Abstract
{
    public interface IKurumYorumBegeniService
    {
        IResult Add(KurumYorumBegeni entity);
        IDataResult<KurumYorumBegeni> GetById(int id);
        IResult Update(KurumYorumBegeni entity);
        IDataResult<List<KurumYorumBegeni>> GetAll();
        IResult Delete(int id);
        IDataResult<List<KurumYorumBegeni>> GetAllByYorumId(int id);
        IDataResult<int> GetByYorumIdBegeniAvarege(int id);
        IResult IPAddressControltoYorum(int yorumId, string IPAddress);
    }
}
