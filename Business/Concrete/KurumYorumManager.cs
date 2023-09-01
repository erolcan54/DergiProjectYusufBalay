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
    public class KurumYorumManager:IKurumYorumService
    {
        private IKurumYorumDal _kurumYorumDal;
        private IKurumYorumBegeniService _kurumYorumBegeniService;

        public KurumYorumManager(IKurumYorumDal kurumYorumDal, IKurumYorumBegeniService kurumYorumBegeniService)
        {
            _kurumYorumDal = kurumYorumDal;
            _kurumYorumBegeniService = kurumYorumBegeniService;
        }

        public IResult Add(KurumYorum entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _kurumYorumDal.Add(entity);
            return new SuccessResult("Yorum yazısı eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _kurumYorumDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _kurumYorumDal.Update(result);
            return new SuccessResult("Yorum silindi.");
        }

        public IDataResult<List<KurumYorum>> GetAll()
        {
            return new SuccessDataResult<List<KurumYorum>>(_kurumYorumDal.GetAll(), "Yorum listesi getirildi.");
        }

        public IDataResult<List<KurumYorumDisplayDto>> GetAllByKurumId(int id)
        {
            List<KurumYorumDisplayDto> yorums = new List<KurumYorumDisplayDto>();
            var result = _kurumYorumDal.GetAll(a => a.KurumId == id && a.Status);
            foreach (var item in result)
            {
                KurumYorumDisplayDto y = new KurumYorumDisplayDto();
                y.AdSoyad = item.AdSoyad;
                y.DeletedDate = item.DeletedDate;
                y.UpdatedDate = item.UpdatedDate;
                y.CreatedDate = item.CreatedDate;
                y.KurumId = item.KurumId;
                y.BegeniAvarege = _kurumYorumBegeniService.GetByYorumIdBegeniAvarege(item.Id).Data;
                y.Id = item.Id;
                y.Status = item.Status;
                y.Yorum = item.Yorum;
                yorums.Add(y);
            }
            return new SuccessDataResult<List<KurumYorumDisplayDto>>(yorums, "Yorum listesi getirildi.");
        }

        public IDataResult<int> GetAvaregeYorumId(int id)
        {
            var result = _kurumYorumBegeniService.GetByYorumIdBegeniAvarege(id);
            return new SuccessDataResult<int>(result.Data);
        }

        public IDataResult<KurumYorum> GetById(int id)
        {
            return new SuccessDataResult<KurumYorum>(_kurumYorumDal.Get(a => a.Id == id), "Yorum getirildi.");
        }

        public IDataResult<int> GetCountByKurumId(int id)
        {
            var result = _kurumYorumDal.GetAll(a => a.KurumId == id && a.Status);
            return new SuccessDataResult<int>((int)result.Count);
        }

        public IResult Update(KurumYorum entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _kurumYorumDal.Update(entity);
            return new SuccessResult("Yorum güncellendi.");
        }
    }
}
