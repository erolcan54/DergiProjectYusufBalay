using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ilceManager:IilceService
    {
        IilceDal _ilceDal;

        public ilceManager(IilceDal ilceDal)
        {
            _ilceDal = ilceDal;
        }

        public IDataResult<ilce> GetById(int id)
        {
            return new SuccessDataResult<ilce>(_ilceDal.Get(a => a.Id == id), "ilce bilgileri getirildi.");
        }

        public IDataResult<List<ilce>> GetByilIdToList(int ilId)
        {
            return new SuccessDataResult<List<ilce>>(_ilceDal.GetByilIdToList(ilId), "ile göre ilçe listesi getirildi.");
        }
    }
}
