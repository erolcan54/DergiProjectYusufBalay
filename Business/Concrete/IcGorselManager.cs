using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class IcGorselManager : IIcGorselService
    {
        private IIcGorselDal _gorselDal;

        public IcGorselManager(IIcGorselDal gorselDal)
        {
            _gorselDal = gorselDal;
        }

        public IResult Add(IcGorsel entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _gorselDal.Add(entity);
            return new SuccessResult("Resim eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _gorselDal.Get(a => a.Id == id);
            _gorselDal.Delete(result);
            return new SuccessResult("Resim silindi.");
        }

        public IDataResult<List<IcGorsel>> GetAllByKurumId(int id)
        {
            var result = _gorselDal.GetAll(a => a.KurumId == id).OrderByDescending(a => a.Id).ToList();
            return new SuccessDataResult<List<IcGorsel>>(result, "Resimler getirildi.");
        }

        public IDataResult<IcGorsel> GetById(int id)
        {
            return new SuccessDataResult<IcGorsel>(_gorselDal.Get(a => a.Id == id), "Resim getirildi.");
        }

        public IResult Update(IcGorsel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _gorselDal.Update(entity);
            return new SuccessResult("Resim güncellendi.");
        }
    }
}
