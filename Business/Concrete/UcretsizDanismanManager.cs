using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UcretsizDanismanManager : IUcretsizDanismanService
    {
        private IUcretsizDanismanDal _ucretsizDanismanDal;

        public UcretsizDanismanManager(IUcretsizDanismanDal ucretsizDanismanDal)
        {
            _ucretsizDanismanDal = ucretsizDanismanDal;
        }

        public IResult Add(UcretsizDanisman entity)
        {
            entity.Status = true;
            entity.CreatedDate= DateTime.Now;
            _ucretsizDanismanDal.Add(entity);
            return new SuccessResult("Ücretsiz Danışman Başvuru bilgileriniz alındı. Ekip arkadaşlarımızı sizlere dönüş sağlayacaktır.");
        }

        public IResult Delete(int id)
        {
            var result = _ucretsizDanismanDal.Get(a => a.Id == id);
            result.Status= false;
            result.DeletedDate= DateTime.Now;
            _ucretsizDanismanDal.Update(result);
            return new SuccessResult("Başvuru bilgileri silindi.");
        }

        public IDataResult<List<UcretsizDanisman>> GetAll()
        {
            var result = _ucretsizDanismanDal.GetAll(a => a.Status);
            return new SuccessDataResult<List<UcretsizDanisman>>(result,"Ücretsiz Danışman başvuru listesi getirildi.");
        }

        public IDataResult<List<UcretsizDanismanDisplayDto>> GetAllDisplay()
        {
            return new SuccessDataResult<List<UcretsizDanismanDisplayDto>>(_ucretsizDanismanDal.GetAllDisplay(), "Ücretsiz Danışman başvuru listesi getirildi.");
        }

        public IDataResult<UcretsizDanisman> GetById(int id)
        {
            return new SuccessDataResult<UcretsizDanisman>(_ucretsizDanismanDal.Get(a => a.Id == id), "Başvuru bilgisi getirildi.");
        }

        public IResult Update(UcretsizDanisman entity)
        {
            entity.UpdatedDate= DateTime.Now;
            _ucretsizDanismanDal.Update(entity);
            return new SuccessResult("Başvuru bilgileri güncellendi.");
        }
    }
}
