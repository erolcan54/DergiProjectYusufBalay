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
    public class OzelDersOgretmenManager : IOzelDersOgretmenService
    {
        private IOzelDersOretmenDal _ozelDersOretmenDal;
        private ICacheManager _cacheManager;

        public OzelDersOgretmenManager(IOzelDersOretmenDal ozelDersOretmenDal, ICacheManager cacheManager)
        {
            _ozelDersOretmenDal = ozelDersOretmenDal;
            _cacheManager = cacheManager;
        }

        public IResult Add(OzelDersOgretmen entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _ozelDersOretmenDal.Add(entity);

            _cacheManager.Remove("OzelDersOgretmens");

            return new SuccessResult("Öğretmen bilgileri eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _ozelDersOretmenDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _ozelDersOretmenDal.Update(result);
            _cacheManager.Remove("OzelDersOgretmens");
            return new SuccessResult("Öğretmen bilgileri silindi.");
        }

        public IDataResult<List<OzelDersOgretmen>> Get6LastList()
        {
            var result = _ozelDersOretmenDal.GetAll(a => a.Status).OrderByDescending(a => a.Id).Take(6).ToList();
            return new SuccessDataResult<List<OzelDersOgretmen>>(result);
        }

        public IDataResult<List<OzelDersOgretmen>> GetAll()
        {
            return new SuccessDataResult<List<OzelDersOgretmen>>(_ozelDersOretmenDal.GetAll(), "Özel ders veren öğretmen listesi getirildi.");
        }

        public IDataResult<List<OzelDersOgretmenDto>> GetAllDisplay()
        {
            return new SuccessDataResult<List<OzelDersOgretmenDto>>(_ozelDersOretmenDal.GetAllDisplay(), "Özel ders öğretmen display listesi getirildi.");
        }

        public IDataResult<List<OzelDersOgretmenDto>> GetAllDisplay4Take()
        {
            var list=new List<OzelDersOgretmenDto>();
            if (!_cacheManager.IsAdd("OzelDersOgretmens"))
            {
                list = _ozelDersOretmenDal.GetAllDisplay4Take();
                _cacheManager.Add("OzelDersOgretmens", list);
            }
            else
                list = _cacheManager.Get<List<OzelDersOgretmenDto>>("OzelDersOgretmens");
            return new SuccessDataResult<List<OzelDersOgretmenDto>>(list, "Özel ders öğretmen display listesi getirildi.");
        }

        public IDataResult<List<OzelDersOgretmenDto>> GetAllOzelOgretmenFiltre(OzelOgretmenFiltreDto filtre)
        {
            return new SuccessDataResult<List<OzelDersOgretmenDto>>(_ozelDersOretmenDal.GetAllOzelOgretmenFiltre(filtre), "Özel Ders veren öğretmenlerin listesi getirildi.");
        }

        public IDataResult<OzelDersOgretmen> GetById(int id)
        {
            return new SuccessDataResult<OzelDersOgretmen>(_ozelDersOretmenDal.Get(a => a.Id == id), "Öğretmen bilgileri getirildi.");
        }

        public IDataResult<OzelDersOgretmenDto> GetByIdDisplay(int id)
        {
            return new SuccessDataResult<OzelDersOgretmenDto>(_ozelDersOretmenDal.GetByIdDisplay(id), "Özel ders öğretmenine ait display getirildi.");
        }

        public IResult Update(OzelDersOgretmen entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _ozelDersOretmenDal.Update(entity);
            _cacheManager.Remove("OzelDersOgretmens");
            return new SuccessResult("Öğretmen bilgileri güncellendi.");
        }
    }
}
