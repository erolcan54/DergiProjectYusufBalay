using Business.Abstract;
using Business.MemoryCaching;
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
        private ICacheManager _cacheManager;

        public indirimManager(IindirimDal indirimDal, ICacheManager cacheManager)
        {
            _indirimDal = indirimDal;
            _cacheManager = cacheManager;
        }

        public IResult Add(indirim entity)
        {
            entity.Status = true;
            entity.CreatedDate = DateTime.Now;
            _indirimDal.Add(entity);

            _cacheManager.Remove("indirims");

            return new SuccessResult("İndirim bilgileri eklendi.");
        }

        public IResult Delete(int id)
        {
            var result = _indirimDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _indirimDal.Update(result);

            _cacheManager.Remove("indirims");

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
            var list=new List<indirimDisplayDto>();
            if (!_cacheManager.IsAdd("indirims"))
            {
                list = _indirimDal.GetAllDisplay4Take();
                _cacheManager.Add("indirims", list);
            }
            else
                list = _cacheManager.Get<List<indirimDisplayDto>>("indirims");
            
            return new SuccessDataResult<List<indirimDisplayDto>>(list, "İndirim listesi getirildi.");
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
            _cacheManager.Remove("indirims");
            return new SuccessResult("İndirim bilgisi güncellendi.");
        }
    }
}
