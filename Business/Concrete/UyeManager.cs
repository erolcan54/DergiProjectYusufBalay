using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;

namespace Business.Concrete
{
    public class UyeManager:IUyeService
    {
        private readonly IUyeDal _uyeDal;

        public UyeManager(IUyeDal uyeDal)
        {
            _uyeDal = uyeDal;
        }

        public IResult Add(Uye entity)
        {
            var checkAdd = CheckAddControl(entity.Email, entity.Telefon);
            if (!checkAdd.Success)
                return new ErrorResult(checkAdd.Message);
            _uyeDal.Add(entity);
            return new SuccessDataResult<Uye>(entity,"Kayıt Başarılı");
        }

        public IDataResult<Uye> GetById(int id)
        {
            return new SuccessDataResult<Uye>(_uyeDal.Get(a => a.Id == id), "Üye getirildi.");
        }

        public IResult Update(Uye entity)
        {
            _uyeDal.Update(entity);
            return new SuccessResult("Güncelleme Başarılı");
        }

        public IDataResult<List<Uye>> GetAll()
        {
            return new SuccessDataResult<List<Uye>>(_uyeDal.GetAll(), "Üye listesi getirildi.");
        }

        public IResult Delete(int id)
        {
            var user = _uyeDal.Get(a => a.Id == id);
            user.Status = false;
            user.DeletedDate=DateTime.Now;
            return new SuccessResult("Üye bilgileri silindi.");
        }

        public IDataResult<Uye> GetByUyeGuid(string uyeGuid)
        {
            var user = _uyeDal.Get(a =>a.UyeGuid.ToString()==uyeGuid);
            if(user!=null)
                return new SuccessDataResult<Uye>(user, "Üye getirildi.");
            return new ErrorDataResult<Uye>(null, "Üye bulunamadı.");
        }

        public IResult CheckAddControl(string email,string tel)
        {
            var result = _uyeDal.Get(a => a.Email == email);
            if (result != null)
                return new ErrorResult("Email adresi sistemde zaten kayıtlı.");
            var result2 = _uyeDal.Get(a => a.Telefon == tel);
            if (result2 != null)
                return new ErrorResult("Telefon numarası sistemde zaten kayıtlı.");
            return new SuccessResult("Kayıt yapılabilir.");
        }
    }
}
