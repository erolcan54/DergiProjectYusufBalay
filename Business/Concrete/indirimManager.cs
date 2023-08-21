using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class indirimManager:IindirimService
    {
        IindirimDal _indirimDal;

        public indirimManager(IindirimDal indirimDal)
        {
            _indirimDal = indirimDal;
        }

        public IResult Add(indirim entity)
        {
            entity.Status = true;
            entity.CreatedDate= DateTime.Now;
            _indirimDal.Add(entity);
            return new SuccessResult("İndirim bilgileri eklendi.");
        }

        public IDataResult<List<indirim>> GetAll()
        {
            return new SuccessDataResult<List<indirim>>(_indirimDal.GetAll(),"İndirim listesi getirildi.");
        }

        public IDataResult<indirim> GetById(int id)
        {
            return new SuccessDataResult<indirim>(_indirimDal.Get(a => a.Id == id), "İndirim bilgisi getirildi.");
        }

        public IResult Update(indirim entity)
        {
            entity.UpdatedDate= DateTime.Now;
            _indirimDal.Update(entity);
            return new SuccessResult("İndirim bilgisi güncellendi.");
        }
    }
}
