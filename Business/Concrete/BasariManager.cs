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
    public class BasariManager : IBasariService
    {
        private IBasariDal _basariDal;

        public BasariManager(IBasariDal basariDal)
        {
            _basariDal = basariDal;
        }

        public IResult Add(Basari entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _basariDal.Add(entity);
            return new SuccessResult("Başarı modeli eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _basariDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _basariDal.Update(result);
            return new SuccessResult("Başarı bilgileri silindi.");
        }

        public IDataResult<List<Basari>> GetAllByKurumId(int id)
        {
            var result = _basariDal.GetAll(a => a.KurumId == id&& a.Status).OrderByDescending(a=>a.Id).ToList();
            return new SuccessDataResult<List<Basari>>(result,"Kuruma ait başarı listesi getirildi.");
        }

        public IDataResult<Basari> GetById(int id)
        {
            var result = _basariDal.Get(a => a.Id == id);
            return new SuccessDataResult<Basari>(result,"Başarı getirildi.");
        }

        public IResult Update(Basari entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _basariDal.Update(entity);
            return new SuccessResult("Başarı modeli güncellendi.");
        }
    }
}
