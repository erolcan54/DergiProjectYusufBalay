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
    public class EtkinlikResimManager:IEtkinlikResimService
    {
        private IEtkinlikResimDal _etkinlikResimDal;

        public EtkinlikResimManager(IEtkinlikResimDal etkinlikResimDal)
        {
            _etkinlikResimDal = etkinlikResimDal;
        }

        public IResult Add(EtkinlikResim entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _etkinlikResimDal.Add(entity);
            return new SuccessResult("Resim eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _etkinlikResimDal.Get(a => a.Id == id);
            _etkinlikResimDal.Delete(result);
            return new SuccessResult("Resim silindi.");
        }

        public IDataResult<List<EtkinlikResim>> GetAllByEtkinlikId(int id)
        {
            var result = _etkinlikResimDal.GetAll(a => a.EtkinlikId == id).OrderByDescending(a => a.Id).ToList();
            return new SuccessDataResult<List<EtkinlikResim>>(result, "Resimler getirildi.");
        }

        public IDataResult<EtkinlikResim> GetById(int id)
        {
            return new SuccessDataResult<EtkinlikResim>(_etkinlikResimDal.Get(a => a.Id == id), "Resim getirildi.");
        }

        public IResult Update(EtkinlikResim entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _etkinlikResimDal.Update(entity);
            return new SuccessResult("Resim güncellendi.");
        }
    }
}
