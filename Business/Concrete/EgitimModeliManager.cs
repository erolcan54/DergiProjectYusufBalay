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
    public class EgitimModeliManager : IEgitimModeliService
    {
        private IEgitimModeliDal _egitimModeliDal;

        public EgitimModeliManager(IEgitimModeliDal egitimModeliDal)
        {
            _egitimModeliDal = egitimModeliDal;
        }

        public IResult Add(KurumEgitimModeli entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _egitimModeliDal.Add(entity);
            return new SuccessResult("Eğitim modeli yazısı eklendi");
        }


        public IResult Delete(int id)
        {
            var result = _egitimModeliDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _egitimModeliDal.Update(result);
            return new SuccessResult("Eğitim modeli yazısı silindi.");
        }

        public IDataResult<KurumEgitimModeli> GetByKurumId(int id)
        {
            var result = _egitimModeliDal.Get(a => a.KurumId == id == a.Status);
            return new SuccessDataResult<KurumEgitimModeli>(result, "Eğitim modeli getirildi.");
        }

        public IDataResult<KurumEgitimModeli> GetById(int id)
        {
            return new SuccessDataResult<KurumEgitimModeli>(_egitimModeliDal.Get(a => a.Id == id), "Eğitim modeli getirildi.");
        }

        public IResult Update(KurumEgitimModeli entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _egitimModeliDal.Update(entity);
            return new SuccessResult("Eğitim modeli yazısı güncellendi.");
        }
    }
}
