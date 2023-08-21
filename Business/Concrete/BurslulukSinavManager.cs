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
    public class BurslulukSinavManager : IBurslulukSinavService
    {
        IBurslulukSinavDal _burslulukSinvaDal;

        public BurslulukSinavManager(IBurslulukSinavDal burslulukSinvaDal)
        {
            _burslulukSinvaDal = burslulukSinvaDal;
        }

        public IResult Add(BurslulukSinav entity)
        {
            entity.Status = true;
            entity.CreatedDate = DateTime.Now;
            _burslulukSinvaDal.Add(entity);
            return new SuccessResult("Sınav bilgileri eklendi.");
        }

        public IDataResult<List<BurslulukSinav>> GetAll()
        {
            return new SuccessDataResult<List<BurslulukSinav>>(_burslulukSinvaDal.GetAll(), "Sınva listesi getirildi.");
        }

        public IDataResult<BurslulukSinav> GetById(int id)
        {
            return new SuccessDataResult<BurslulukSinav>(_burslulukSinvaDal.Get(a => a.Id == id), "Sinav bilgisi getirildi.");
        }

        public IResult Update(BurslulukSinav entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _burslulukSinvaDal.Update(entity);
            return new SuccessResult("Sınva bilgileri güncellendi.");
        }
    }
}
