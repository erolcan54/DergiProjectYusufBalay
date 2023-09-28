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
    public class KatalogManager : IKatalogService
    {
        private IKatalogDal _katalogDal;

        public KatalogManager(IKatalogDal katalogDal)
        {
            _katalogDal = katalogDal;
        }

        public IResult Add(Katalog entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            entity.SeriNo=Guid.NewGuid();
            _katalogDal.Add(entity);
            return new SuccessResult("Katalog eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _katalogDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _katalogDal.Update(result);
            return new SuccessResult("Katalog silindi.");
        }

        public IDataResult<List<Katalog>> GetAllByKurumId(int id)
        {
            var result = _katalogDal.GetAll(a => a.KurumId == id && a.Status);
            return new SuccessDataResult<List<Katalog>>(result,"Kurum katalogları getirildi.");
        }

        public IDataResult<Katalog> GetById(int id)
        {
            return new SuccessDataResult<Katalog>(_katalogDal.Get(a => a.Id == id), "Katalog getirildi.");
        }

        public IDataResult<int> GetByKurumIdKatalogCount(int id)
        {
            var result = _katalogDal.GetAll(a => a.KurumId == id && a.Status).Count();
            return new SuccessDataResult<int>(result, "Katalog sayısı getirildi.");
        }

        public IDataResult<Katalog> GetBySeriNo(Guid seriNo)
        {
            return new SuccessDataResult<Katalog>(_katalogDal.Get(a => a.SeriNo == seriNo), "Katalog getirildi.");
        }

        public IResult Update(Katalog entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _katalogDal.Update(entity);
            return new SuccessResult("Katalog güncellendi.");
        }
    }
}
