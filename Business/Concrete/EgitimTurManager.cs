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
    public class EgitimTurManager:IEgitimTurService
    {
        IEgitimTurDal _egitimTurDal;

        public EgitimTurManager(IEgitimTurDal egitimTurDal)
        {
            _egitimTurDal = egitimTurDal;
        }

        public IDataResult<List<EgitimTur>> GetAll()
        {
            return new SuccessDataResult<List<EgitimTur>>(_egitimTurDal.GetAll(),"Eğitim türleri listelendi."); 
        }

        public IDataResult<EgitimTur> GetById(int id)
        {
            return new SuccessDataResult<EgitimTur>(_egitimTurDal.Get(a => a.Id == id), "Eğitim türü getirildi.");
        }
    }
}
