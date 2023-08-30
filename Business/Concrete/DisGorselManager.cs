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
    public class DisGorselManager:IDisGorselService
    {
        private IDisGorselDal _gorselDal;

        public DisGorselManager(IDisGorselDal gorselDal)
        {
            _gorselDal = gorselDal;
        }

        public IResult Add(DisGorsel entity)
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

        public IDataResult<List<DisGorsel>> GetAllByKurumId(int id)
        {
            var result = _gorselDal.GetAll(a => a.KurumId == id).OrderByDescending(a => a.Id).ToList();
            return new SuccessDataResult<List<DisGorsel>>(result, "Resimler getirildi.");
        }

        public IDataResult<DisGorsel> GetById(int id)
        {
            return new SuccessDataResult<DisGorsel>(_gorselDal.Get(a => a.Id == id), "Resim getirildi.");
        }

        public IResult Update(DisGorsel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _gorselDal.Update(entity);
            return new SuccessResult("Resim güncellendi.");
        }
    }
}
