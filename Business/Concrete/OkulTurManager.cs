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
    public class OkulTurManager : IOkulTurService
    {
        IOkulTurDal _okulTurDal;

        public OkulTurManager(IOkulTurDal okulTurDal)
        {
            _okulTurDal = okulTurDal;
        }

        public IResult Add(OkulTur entity)
        {
            _okulTurDal.Add(entity);
            return new SuccessResult("Okul türü eklendi.");
        }

        public IDataResult<List<OkulTur>> GetAll()
        {
            return new SuccessDataResult<List<OkulTur>>(_okulTurDal.GetAll(),"Okul türleri listelendi.");
        }

        public IDataResult<OkulTur> GetById(int id)
        {
            return new SuccessDataResult<OkulTur>(_okulTurDal.Get(a => a.Id == id), "Okul tür bilgileri getirildi.");
        }

        public IResult Update(OkulTur entity)
        {
            _okulTurDal.Update(entity);
            return new SuccessResult("Okul tür bilgileri listelendi.");
        }
    }
}
