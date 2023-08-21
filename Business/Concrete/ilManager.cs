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
    public class ilManager : IilService
    {
        IilDal _ilDal;

        public ilManager(IilDal ilDal)
        {
            _ilDal = ilDal;
        }

        public IDataResult<List<il>> GetAll()
        {
            return new SuccessDataResult<List<il>>(_ilDal.GetAll(), "il listesi getirildi.");
        }

        public IDataResult<il> GetById(int id)
        {
            return new SuccessDataResult<il>(_ilDal.Get(a => a.Id == id), "İl bilgileri getirildi.");
        }
    }
}
