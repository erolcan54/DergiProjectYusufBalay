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
using Entities.DTOs;

namespace Business.Concrete
{
    public class KurumBeniAraManager:IKurumBeniAraService
    {
        private IKurumBeniAraDal _kurumBeniAraDal;

        public KurumBeniAraManager(IKurumBeniAraDal kurumBeniAraDal)
        {
            _kurumBeniAraDal = kurumBeniAraDal;
        }

        public IResult Add(KurumBeniAra entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _kurumBeniAraDal.Add(entity);
            return new SuccessResult("Kayıt eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _kurumBeniAraDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _kurumBeniAraDal.Update(result);
            return new SuccessResult("Kayıt silindi.");
        }

        public IDataResult<List<KurumBeniAraDto>> GetAll()
        {
            var result = _kurumBeniAraDal.GetAllDisplay();
            return new SuccessDataResult<List<KurumBeniAraDto>>(result, "Kurum aranması isteyenler listelendi.");
        }

        public IDataResult<KurumBeniAra> GetById(int id)
        {
            return new SuccessDataResult<KurumBeniAra>(_kurumBeniAraDal.Get(a => a.Id == id), "Kayıt getirildi.");
        }

        public IDataResult<List<KurumBeniAra>> GetListByKurumId(int id)
        {
            return new SuccessDataResult<List<KurumBeniAra>>(_kurumBeniAraDal.GetAll(a => a.KurumId == id));
        }

        public IResult Update(KurumBeniAra entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _kurumBeniAraDal.Update(entity);
            return new SuccessResult("Kayıt güncellendi.");
        }
    }
}
