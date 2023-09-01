using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete
{
    public class EfKurumYorumDal : EfEntityRepositoryBase<KurumYorum, EfContext>, IKurumYorumDal
    {
    }
}
