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
    public class OgretmenManager : IOgretmenService
    {
        IOgretmenDal _ogretmenDal;
        public OgretmenManager(IOgretmenDal ogretmenDal)
        {
            _ogretmenDal = ogretmenDal;
        }

        public IResult Add(Ogretmen entity)
        {
            entity.Status = true;
            entity.CreatedDate = DateTime.Now;
            _ogretmenDal.Add(entity);
            return new SuccessResult("Öğretmen bilgileri eklendi.");
        }

        public IResult Delete(int id)
        {
            var result = _ogretmenDal.Get(a => a.Id == id);
            result.DeletedDate = DateTime.Now;
            result.Status = false;
            _ogretmenDal.Update(result);
            return new SuccessResult("Öğretmen bilgileri silindi.");
        }

        public IDataResult<List<Ogretmen>> GetAll()
        {
            return new SuccessDataResult<List<Ogretmen>>(_ogretmenDal.GetAll(), "Öğretmen listesi getirildi.");
        }

        public IDataResult<List<OgretmenDisplayDto>> GetAllGetByKurumId(int id)
        {
            return new SuccessDataResult<List<OgretmenDisplayDto>>(_ogretmenDal.GetAllGetByKurumId(id), "Öğretmen listesi getirildi.");
        }

        public IDataResult<Ogretmen> GetById(int id)
        {
            return new SuccessDataResult<Ogretmen>(_ogretmenDal.Get(a => a.Id == id), "Öğretmen bilgisi getirildi.");
        }

        public IDataResult<int> GetByKurumIdOgretmenCount(int id)
        {
            var result=_ogretmenDal.GetAll(a=>a.KurumId==id).Count();
            return new SuccessDataResult<int>(result, "Öğretmen sayısı getirildi.");
        }

        public IResult Update(Ogretmen entity)
        {
            _ogretmenDal.Update(entity);
            return new SuccessResult("Öğretmen bilgisi güncellendi.");
        }
    }
}
