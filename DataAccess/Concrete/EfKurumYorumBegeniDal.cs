using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete
{
    public class EfKurumYorumBegeniDal : EfEntityRepositoryBase<KurumYorumBegeni, EfContext>, IKurumYorumBegeniDal
    {
    }
}
