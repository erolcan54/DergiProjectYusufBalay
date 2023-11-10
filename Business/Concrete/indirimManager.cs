using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class indirimManager : IindirimService
    {
        IindirimDal _indirimDal;

        public indirimManager(IindirimDal indirimDal)
        {
            _indirimDal = indirimDal;
        }

        public IResult Add(indirim entity)
        {
            entity.Status = true;
            entity.CreatedDate = DateTime.Now;
            _indirimDal.Add(entity);
            return new SuccessResult("İndirim bilgileri eklendi.");
        }

        public IResult Delete(int id)
        {
            var result = _indirimDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _indirimDal.Update(result);
            return new SuccessResult("İndirim silindi.");
        }

        public IDataResult<List<indirim>> GetAll()
        {
            return new SuccessDataResult<List<indirim>>(_indirimDal.GetAll(), "İndirim listesi getirildi.");
        }

        public IDataResult<List<indirimDisplayDto>> GetAllByKurumId(int id)
        {
            return new SuccessDataResult<List<indirimDisplayDto>>(_indirimDal.GetAllByKurumId(id), "İndirim listesi getirildi.");
        }

        public IDataResult<List<indirimDisplayDto>> GetAllDisplay()
        {
            var result=_indirimDal.GetAllDisplay();
            return new SuccessDataResult<List<indirimDisplayDto>>(result,"İndirim listesi getirildi.");
        }

        public IDataResult<List<indirimDisplayDto>> GetAllDisplay4Take()
        {
            var result = _indirimDal.GetAllDisplay4Take();
            return new SuccessDataResult<List<indirimDisplayDto>>(result, "İndirim listesi getirildi.");
        }

        public IDataResult<indirim> GetById(int id)
        {
            return new SuccessDataResult<indirim>(_indirimDal.Get(a => a.Id == id), "İndirim bilgisi getirildi.");
        }

        public IDataResult<List<indirimDisplayDto>> GetindirimFiltre(indirimFiltreDto filtre)
        {
            return new SuccessDataResult<List<indirimDisplayDto>>(_indirimDal.GetindirimFiltre(filtre), "İndirim listesi getirildi.");
        }

        public IResult Update(indirim entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _indirimDal.Update(entity);
            return new SuccessResult("İndirim bilgisi güncellendi.");
        }
    }
}
