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
    public class OzelDersVeliBasvuruManager:IOzelDersVeliBasvuruService
    {
        private IOzelDersVeliBasvuruDal _ozelDersVeliBasvuruDal;

        public OzelDersVeliBasvuruManager(IOzelDersVeliBasvuruDal ozelDersVeliBasvuruDal)
        {
            _ozelDersVeliBasvuruDal = ozelDersVeliBasvuruDal;
        }

        public IResult Add(OzelDersVeliBasvuru entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _ozelDersVeliBasvuruDal.Add(entity);
            return new SuccessResult("Başvuru eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _ozelDersVeliBasvuruDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _ozelDersVeliBasvuruDal.Update(result);
            return new SuccessResult("Başvuru silindi.");
        }

        public IDataResult<List<OzelDersVeliBasvuru>> GetAll()
        {
            return new SuccessDataResult<List<OzelDersVeliBasvuru>>(_ozelDersVeliBasvuruDal.GetAll(), "Basvurular listesi getirildi.");
        }

        public IDataResult<OzelDersVeliBasvuru> GetById(int id)
        {
            return new SuccessDataResult<OzelDersVeliBasvuru>(_ozelDersVeliBasvuruDal.Get(a => a.Id == id), "Başvuru getirildi.");
        }

        public IDataResult<List<OzelDersVeliBasvuru>> GetListByOgretmenId(int id)
        {
            return new SuccessDataResult<List<OzelDersVeliBasvuru>>(_ozelDersVeliBasvuruDal.GetAll(a => a.OzelDersOgretmenId == id));
        }

        public IResult Update(OzelDersVeliBasvuru entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _ozelDersVeliBasvuruDal.Update(entity);
            return new SuccessResult("Başvuru güncellendi.");
        }

    }
}
