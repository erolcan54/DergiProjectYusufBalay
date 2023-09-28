using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Entities.DTOs;

namespace Business.Concrete
{
    public class KullaniciManager : IKullaniciService
    {
        IKullaniciDal _kullaniciDal;
        public KullaniciManager(IKullaniciDal kullaniciDal)
        {
            _kullaniciDal = kullaniciDal;
        }

        public IResult KullaniciAdiCheck(string kullaniciAdi)
        {
            var result = _kullaniciDal.Get(a => a.KullaniciAdi == kullaniciAdi);
            if (result == null)
                return new SuccessResult("Kullanıcı Adı kullanılabilir.");
            return new ErrorResult("Kullanıcı adı sistemde zaten var.");
        }

        public IResult Add(Kullanici entity)
        {
            var kullaniciAdiKontrol = KullaniciAdiCheck(entity.KullaniciAdi);
            if (!kullaniciAdiKontrol.Success)
                return new ErrorResult(kullaniciAdiKontrol.Message);

            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _kullaniciDal.Add(entity);
            return new SuccessResult("Kullanıcı bilgisi eklendi.");
        }

        public IResult Delete(int id)
        {
            var result = _kullaniciDal.Get(a => a.Id == id);
            result.DeletedDate= DateTime.Now;
            result.Status= false;
            _kullaniciDal.Update(result);
            return new SuccessResult("Kullanıcı bilgisi silindi.");
        }

        public IDataResult<List<Kullanici>> GetAll()
        {
            return new SuccessDataResult<List<Kullanici>>(_kullaniciDal.GetAll(),"Kullanıcı listesi getirildi.");
        }

        public IDataResult<Kullanici> GetById(int id)
        {
            return new SuccessDataResult<Kullanici>(_kullaniciDal.Get(a => a.Id == id), "Kullanıcı bilgisi getirildi.");
        }

        public IDataResult<List<Kullanici>> GetByListStatusTrue()
        {
            return new SuccessDataResult<List<Kullanici>>(_kullaniciDal.GetAll(a=>a.Status), "Kullanıcı listesi getirildi.");
        }

        public IDataResult<Kullanici> GirisKontrol(GirisModelDto model)
        {
            var result = _kullaniciDal.Get(a=>a.KullaniciAdi==model.KullaniciAdi&&a.Sifre==model.Sifre&&a.Status);
            if (result == null)
                return new ErrorDataResult<Kullanici>(null, "Kullanıcı bilgileri bulunamadı.");
            if(!result.Status)
                return new ErrorDataResult<Kullanici>(null,"Kullanıcı bilgileri silinmiş.");
            return new SuccessDataResult<Kullanici>(result, "Kullanıcı bilgileri getirildi.");
        }

        public IResult Update(Kullanici entity)
        {
            entity.UpdatedDate= DateTime.Now;
           _kullaniciDal.Update(entity);
            return new SuccessResult("Kullanıcı bilgisi güncellendi.");
        }

        public IDataResult<List<Kullanici>> GetAllByIdKurum(int id)
        {
            var result = _kullaniciDal.GetAll(a => a.OkulId == id);
            return new SuccessDataResult<List<Kullanici>>(result, "Kurum kullanıcıları listelendi.");
        }

        public IDataResult<int> GetByKurumIdKullaniciCount(int id)
        {
            var result = _kullaniciDal.GetAll(a => a.OkulId == id && a.Status).Count();
            return new SuccessDataResult<int>(result, "Kullanıcı sayısı getirildi.");
        }
    }
}
