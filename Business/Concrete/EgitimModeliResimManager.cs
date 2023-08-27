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
    public class EgitimModeliResimManager : IEgitimModeliResimService
    {
        private IEgitimModeliResimDal _egitimModeliResimDal;

        public EgitimModeliResimManager(IEgitimModeliResimDal egitimModeliResimDal)
        {
            _egitimModeliResimDal = egitimModeliResimDal;
        }

        public IResult Add(KurumEgitimModeliResim entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _egitimModeliResimDal.Add(entity);
            return new SuccessResult("Eğitim modeli resmi eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _egitimModeliResimDal.Get(a => a.Id == id);
            _egitimModeliResimDal.Delete(result);
            return new SuccessResult("Resim silindi.");
        }

        public IDataResult<List<KurumEgitimModeliResim>> GetAllByKurumId(int id)
        {
            var result = _egitimModeliResimDal.GetAll(a => a.KurumId == id).OrderByDescending(a=>a.Id).ToList();
            return new SuccessDataResult<List<KurumEgitimModeliResim>>(result, "Resimler getirildi.");
        }

        public IDataResult<KurumEgitimModeliResim> GetById(int id)
        {
            return new SuccessDataResult<KurumEgitimModeliResim>(_egitimModeliResimDal.Get(a => a.Id == id), "Resim getirildi.");
        }

        public IResult Update(KurumEgitimModeliResim entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _egitimModeliResimDal.Update(entity);
            return new SuccessResult("Resim güncellendi.");
        }
    }
}
