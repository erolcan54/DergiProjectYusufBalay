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
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BurslulukSinavManager : IBurslulukSinavService
    {
        IBurslulukSinavDal _burslulukSinvaDal;
        private ICacheManager _cacheManager;

        public BurslulukSinavManager(IBurslulukSinavDal burslulukSinvaDal, ICacheManager cacheManager)
        {
            _burslulukSinvaDal = burslulukSinvaDal;
            _cacheManager = cacheManager;
        }

        public IResult Add(BurslulukSinav entity)
        {
            entity.Status = true;
            entity.CreatedDate = DateTime.Now;
            _burslulukSinvaDal.Add(entity);

            _cacheManager.Remove("Sinavs");

            return new SuccessResult("Sınav bilgileri eklendi.");
        }
        public IResult Delete(int id)
        {
            var result = _burslulukSinvaDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _burslulukSinvaDal.Update(result);
            _cacheManager.Remove("Sinavs");
            return new SuccessResult("Sınav silindi.");
        }

        public IDataResult<List<BurslulukSinav>> GetAllByKurumId(int id)
        {
            return new SuccessDataResult<List<BurslulukSinav>>(_burslulukSinvaDal.GetAll(a=>a.KurumId==id && a.Status), "Sınva listesi getirildi.");
        }

        public IDataResult<List<BurslulukSinavDisplayDto>> GetAllDisplay()
        {
            return new SuccessDataResult<List<BurslulukSinavDisplayDto>>(_burslulukSinvaDal.GetAllDisplay(), "Listes getirildi.");
        }

        public IDataResult<List<BurslulukSinavDisplayDto>> GetAllDisplay4Take()
        {
            var list=new List<BurslulukSinavDisplayDto>();
            if (!_cacheManager.IsAdd("Sinavs"))
            {
                list = _burslulukSinvaDal.GetAllDisplay4Take();
                _cacheManager.Add("Sinavs", list);
            }
            else
                list = _cacheManager.Get<List<BurslulukSinavDisplayDto>>("Sinavs");
            return new SuccessDataResult<List<BurslulukSinavDisplayDto>>(list,"Listes getirildi.");
        }

        public IDataResult<List<BurslulukSinavDisplayDto>> GetBurslulukSinavFiltre(BurslulukSinavFiltreDto filtre)
        {
            return new SuccessDataResult<List<BurslulukSinavDisplayDto>>(_burslulukSinvaDal.GetBurslulukSinavFiltre(filtre), "Listes getirildi.");
        }

        public IDataResult<BurslulukSinav> GetById(int id)
        {
            return new SuccessDataResult<BurslulukSinav>(_burslulukSinvaDal.Get(a => a.Id == id), "Sinav bilgisi getirildi.");
        }

        public IDataResult<BurslulukSinavDisplayDto> GetByIdDisplay(int id)
        {
            return new SuccessDataResult<BurslulukSinavDisplayDto>(_burslulukSinvaDal.GetByIdDisplay(id), "Sınav getirildi.");
        }

        public IResult Update(BurslulukSinav entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _burslulukSinvaDal.Update(entity);
            _cacheManager.Remove("Sinavs");
            return new SuccessResult("Sınva bilgileri güncellendi.");
        }
    }
}
