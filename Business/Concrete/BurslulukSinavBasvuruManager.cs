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
    public class BurslulukSinavBasvuruManager:IBurslulukSinavBasvuruService
    {
        private IBurslulukSinavBasvuruDal _burslulukSinavBasvuruDal;

        public BurslulukSinavBasvuruManager(IBurslulukSinavBasvuruDal burslulukSinavBasvuruDal)
        {
            _burslulukSinavBasvuruDal = burslulukSinavBasvuruDal;
        }

        public IResult Add(BurslulukSinavBasvuru entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _burslulukSinavBasvuruDal.Add(entity);
            return new SuccessResult("Başvuru eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _burslulukSinavBasvuruDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _burslulukSinavBasvuruDal.Update(result);
            return new SuccessResult("Basvuru silindi.");
        }

        public IDataResult<List<BurslulukSinavBasvuru>> GetAll()
        {
            return new SuccessDataResult<List<BurslulukSinavBasvuru>>(_burslulukSinavBasvuruDal.GetAll(a=>a.Status), "Başvuru Listesi getirildi.");
        }

        public IDataResult<BurslulukSinavBasvuru> GetById(int id)
        {
            return new SuccessDataResult<BurslulukSinavBasvuru>(_burslulukSinavBasvuruDal.Get(a => a.Id == id), "Başvuru bilgisi getirildi.");
        }

        public IResult Update(BurslulukSinavBasvuru entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _burslulukSinavBasvuruDal.Update(entity);
            return new SuccessResult("Başvuru bilgisi güncellendi.");
        }
    }
}
