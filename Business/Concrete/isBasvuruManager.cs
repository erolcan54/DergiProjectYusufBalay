using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class isBasvuruManager : IisBasvuruService
    {
        private IisBasvuruDal _isBasvuruDal;

        public isBasvuruManager(IisBasvuruDal isBasvuruDal)
        {
            _isBasvuruDal = isBasvuruDal;
        }

        public IResult Add(isBasvuru entity)
        {
            if (!KayitKontrol(entity.Email, entity.Telefon).Success)
                return new ErrorResult("Daha önce yapılmış iş başvurunuz var.");
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _isBasvuruDal.Add(entity);
            return new SuccessResult("İş başvurunuz alınmıştır. Bilgileriniz kurumlarla paylaşılacaktır.");
        }

        public IResult KayitKontrol(string email, string telefon)
        {
            var model = _isBasvuruDal.Get(a => (a.Telefon == telefon || a.Email == email) && a.Status);
            if (model != null)
                return new ErrorResult("Kayıt var");
            return new SuccessResult("Kayıt yapılabilir.");
        }

        public IResult Delete(int id)
        {
            var result = _isBasvuruDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _isBasvuruDal.Update(result);
            return new SuccessResult("Başvuru bilgileri silindi.");
        }

        public IDataResult<List<isBasvuru>> GetAll()
        {
            var result = _isBasvuruDal.GetAll(a => a.Status).OrderByDescending(a => a.Id).ToList();
            return new SuccessDataResult<List<isBasvuru>>(result, "Başvuru listesi getirildi.");
        }

        public IDataResult<isBasvuru> GetById(int id)
        {
            var result = _isBasvuruDal.Get(a => a.Id == id);
            return new SuccessDataResult<isBasvuru>(result, "Başvuru getirildi.");
        }

        public IResult Update(isBasvuru entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _isBasvuruDal.Update(entity);
            return new SuccessResult("Başvuru güncellendi.");
        }

        public IDataResult<List<isBasvuruDisplayDto>> GetAllByilId(int id)
        {
            var result = _isBasvuruDal.GetAllByilId(id).OrderByDescending(a => a.Id).ToList();
            return new SuccessDataResult<List<isBasvuruDisplayDto>>(result, "Başvuru listesi getirildi.");
        }

        public IDataResult<isBasvuruDisplayDto> GetByIdDisplay(int id)
        {
            var result = _isBasvuruDal.GetByIdDisplay(id);
            return new SuccessDataResult<isBasvuruDisplayDto>(result, "Başvuru getirildi.");
        }
    }
}
