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
    public class EtkinlikManager:IEtkinlikService
    {
        private IEtkinlikDal _etkinlikDal;

        public EtkinlikManager(IEtkinlikDal etkinlikDal)
        {
            _etkinlikDal = etkinlikDal;
        }

        public IResult Add(Etkinlik entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _etkinlikDal.Add(entity);
            return new SuccessResult("Etkinlik yazısı eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _etkinlikDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _etkinlikDal.Update(result);
            return new SuccessResult("Etkinlik yazısı silindi.");
        }

        public IDataResult<List<Etkinlik>> GetAllByKurumId(int id)
        {
            var result = _etkinlikDal.GetAll(a => a.KurumId == id == a.Status).OrderByDescending(a=>a.Id).ToList();
            return new SuccessDataResult<List<Etkinlik>>(result, "Etkinlikler getirildi.");
        }

        public IDataResult<Etkinlik> GetById(int id)
        {
            return new SuccessDataResult<Etkinlik>(_etkinlikDal.Get(a => a.Id == id), "Etkinlik getirildi.");
        }

        public IResult Update(Etkinlik entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _etkinlikDal.Update(entity);
            return new SuccessResult("Etkinlik yazısı güncellendi.");
        }
    }
}
