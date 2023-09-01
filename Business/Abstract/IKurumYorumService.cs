using Core.Utilities.Results;
using Entities;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IKurumYorumService
    {
        IResult Add(KurumYorum entity);
        IDataResult<KurumYorum> GetById(int id);
        IResult Update(KurumYorum entity);
        IDataResult<List<KurumYorum>> GetAll();
        IResult Delete(int id);
        IDataResult<List<KurumYorumDisplayDto>> GetAllByKurumId(int id);
        IDataResult<int> GetCountByKurumId(int id);
        IDataResult<int> GetAvaregeYorumId(int id);

    }
}
