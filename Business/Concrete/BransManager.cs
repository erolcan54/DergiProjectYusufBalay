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
    public class BransManager : IBransService
    {
        IBransDal _bransDal;

        public BransManager(IBransDal bransDal)
        {
            _bransDal = bransDal;
        }

        public IResult Add(Brans entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _bransDal.Add(entity);
            return new SuccessResult("Branş eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _bransDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _bransDal.Update(result);
            return new SuccessResult("Blog silindi.");
        }

        public IDataResult<List<Brans>> GetAll()
        {
            return new SuccessDataResult<List<Brans>>(_bransDal.GetAll(), "Brans Listesi getirildi.");
        }

        public IDataResult<Brans> GetById(int id)
        {
            return new SuccessDataResult<Brans>(_bransDal.Get(a => a.Id == id), "Branş bilgisi getirildi.");
        }

        public IResult Update(Brans entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _bransDal.Update(entity);
            return new SuccessResult("Branş bilgisi güncellendi.");
        }
    }
}
