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
    public class OzelOgretmenYorumManager:IOzelOgretmenYorumService
    {
        private IOzelOgretmenYorumDal _ozelOgretmenYorumDal;
        private IOzelOgretmenYorumBegeniService _ozelOgretmenYorumBegeniService;

        public OzelOgretmenYorumManager(IOzelOgretmenYorumDal ozelOgretmenYorumDal, IOzelOgretmenYorumBegeniService ozelOgretmenYorumBegeniService)
        {
            _ozelOgretmenYorumDal = ozelOgretmenYorumDal;
            _ozelOgretmenYorumBegeniService = ozelOgretmenYorumBegeniService;
        }

        public IResult Add(OzelOgretmenYorum entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _ozelOgretmenYorumDal.Add(entity);
            return new SuccessResult("Yorum yazısı eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _ozelOgretmenYorumDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _ozelOgretmenYorumDal.Update(result);
            return new SuccessResult("Yorum silindi.");
        }

        public IDataResult<List<OzelOgretmenYorum>> GetAll()
        {
            return new SuccessDataResult<List<OzelOgretmenYorum>>(_ozelOgretmenYorumDal.GetAll(), "Yorum listesi getirildi.");
        }

        public IDataResult<List<YorumDisplayDto>> GetAllByOgretmenId(int id)
        {
            List<YorumDisplayDto> yorums=new List<YorumDisplayDto>();
            var result = _ozelOgretmenYorumDal.GetAll(a => a.OzelOgretmenId == id && a.Status);
            foreach (var item in result)
            {
                YorumDisplayDto y=new YorumDisplayDto();
                y.AdSoyad= item.AdSoyad;
                y.DeletedDate= item.DeletedDate;
                y.UpdatedDate= item.UpdatedDate;
                y.CreatedDate= item.CreatedDate;
                y.OzelOgretmenId = item.OzelOgretmenId;
                y.BegeniAvarege = _ozelOgretmenYorumBegeniService.GetByYorumIdBegeniAvarege(item.Id).Data;
                y.Id= item.Id;
                y.Status= item.Status;
                y.Yorum= item.Yorum;
                yorums.Add(y);  
            }
            return new SuccessDataResult<List<YorumDisplayDto>>(yorums, "Yorum listesi getirildi.");
        }

        public IDataResult<int> GetAvaregeYorumId(int id)
        {
            var result = _ozelOgretmenYorumBegeniService.GetByYorumIdBegeniAvarege(id);
            return new SuccessDataResult<int>(result.Data);
        }

        public IDataResult<OzelOgretmenYorum> GetById(int id)
        {
            return new SuccessDataResult<OzelOgretmenYorum>(_ozelOgretmenYorumDal.Get(a => a.Id == id), "Yorum getirildi.");
        }

        public IDataResult<int> GetCountByOgretmenId(int id)
        {
            var result = _ozelOgretmenYorumDal.GetAll(a => a.OzelOgretmenId == id && a.Status);
            return new SuccessDataResult<int>((int)result.Count);
        }

        public IResult Update(OzelOgretmenYorum entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _ozelOgretmenYorumDal.Update(entity);
            return new SuccessResult("Yorum güncellendi.");
        }
    }
}
