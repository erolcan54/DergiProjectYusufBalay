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
    public class KulupManager: IKulupService
    {
        private IKulupDal _kulupDal;

        public KulupManager(IKulupDal kulupDal)
        {
            _kulupDal = kulupDal;
        }

        public IResult Add(Kulup entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _kulupDal.Add(entity);
            return new SuccessResult("Kulüp eklendi");
        }


        public IResult Delete(int id)
        {
            var result = _kulupDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _kulupDal.Update(result);
            return new SuccessResult("Kulüp silindi.");
        }

        public IDataResult<List<Kulup>> GetAllByKurumId(int id)
        {
            var result = _kulupDal.GetAll(a => a.KurumId == id == a.Status);
            return new SuccessDataResult<List<Kulup>>(result, "Kulüpler getirildi.");
        }

        public IDataResult<Kulup> GetById(int id)
        {
            return new SuccessDataResult<Kulup>(_kulupDal.Get(a => a.Id == id), "Kulüp getirildi.");
        }

        public IResult Update(Kulup entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _kulupDal.Update(entity);
            return new SuccessResult("Kulüp güncellendi.");
        }
    }
}
