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
    public class YoneticiManager : IYoneticiService
    {
        IYoneticiDal _yoneticiDal;

        public YoneticiManager(IYoneticiDal yoneticiDal)
        {
            _yoneticiDal = yoneticiDal;
        }
        public IResult KullaniciAdiCheck(string kullaniciAdi)
        {
            var result = _yoneticiDal.Get(a => a.KullaniciAdi == kullaniciAdi);
            if (result == null)
                return new SuccessResult("Kullanıcı Adı kullanılabilir.");
            return new ErrorResult("Kullanıcı adı sistemde zaten var.");
        }
        public IResult Add(Yonetici entity)
        {
            var kullaniciAdiKontrol = KullaniciAdiCheck(entity.KullaniciAdi);
            if (!kullaniciAdiKontrol.Success)
                return new ErrorResult(kullaniciAdiKontrol.Message);

            entity.Status = true;
            entity.CreatedDate= DateTime.Now;
            _yoneticiDal.Add(entity);
            return new SuccessResult("Yönetici bilgisi eklendi.");
        }

        public IResult Delete(int id)
        {
            var result=_yoneticiDal.Get(a=>a.Id==id);
            result.DeletedDate = DateTime.Now;
            result.Status= false;
            _yoneticiDal.Update(result);
            return new SuccessResult("Yönetici bilgisi silindi.");
        }

        public IDataResult<List<Yonetici>> GetAll()
        {
            return new SuccessDataResult<List<Yonetici>>(_yoneticiDal.GetAll(),"Yönetici listesi getirildi.");
        }

        public IDataResult<Yonetici> GetById(int id)
        {
            return new SuccessDataResult<Yonetici>(_yoneticiDal.Get(a => a.Id == id), "Yönetici bilgileri getirildi.");
        }

        public IDataResult<List<Yonetici>> GetByListStatusTrue()
        {
            return new SuccessDataResult<List<Yonetici>>(_yoneticiDal.GetAll(a=>a.Status), "Yönetici listesi getirildi.");
        }

        public IResult Update(Yonetici entity)
        {
            _yoneticiDal.Update(entity);
            return new SuccessResult("Yönetici bilgileri güncellendi.");
        }

        public IDataResult<Yonetici> GirisKontrol(GirisModelDto model)
        {
            var result = _yoneticiDal.Get(a => a.KullaniciAdi == model.KullaniciAdi && a.Sifre == model.Sifre);
            if (result == null)
                return new ErrorDataResult<Yonetici>(null, "Kullanıcı adı veya şifre yanlış. Lütfen kontrol ediniz.");
            if (!result.Status)
                return new ErrorDataResult<Yonetici>(null, "Yönetici silinmiş.");
            return new SuccessDataResult<Yonetici>(result, "Yönetici bilgileri getirildi.");
        }
    }
}
