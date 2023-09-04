using Business.Abstract;
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

        public BurslulukSinavManager(IBurslulukSinavDal burslulukSinvaDal)
        {
            _burslulukSinvaDal = burslulukSinvaDal;
        }

        public IResult Add(BurslulukSinav entity)
        {
            entity.Status = true;
            entity.CreatedDate = DateTime.Now;
            _burslulukSinvaDal.Add(entity);
            return new SuccessResult("Sınav bilgileri eklendi.");
        }
        public IResult Delete(int id)
        {
            var result = _burslulukSinvaDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _burslulukSinvaDal.Update(result);
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
            return new SuccessDataResult<List<BurslulukSinavDisplayDto>>(_burslulukSinvaDal.GetAllDisplay4Take(),"Listes getirildi.");
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
            return new SuccessResult("Sınva bilgileri güncellendi.");
        }
    }
}
