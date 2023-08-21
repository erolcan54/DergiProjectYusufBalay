using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IilceService
    {
        IDataResult<ilce> GetById(int id);
        IDataResult<List<ilce>> GetByilIdToList(int ilId);
    }
}
