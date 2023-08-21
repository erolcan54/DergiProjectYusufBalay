using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfilceDal : EfEntityRepositoryBase<ilce, EfContext>, IilceDal
    {
        public List<ilce> GetByilIdToList(int ilId)
        {
            using (EfContext context=new EfContext())
            {
                var result = context.ilce.Where(a => a.ilId == ilId);
                return result.ToList();
            }
        }
    }
}
