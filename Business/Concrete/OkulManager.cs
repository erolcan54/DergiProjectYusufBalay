using Business.Abstract;
using Business.MemoryCaching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OkulManager : IOkulService
    {
        private IOkulDal _okulDal;
        private IOkulTurService _okulTurService;
        private IKurumYorumService _kurumYorumService;
        private ICacheManager _cacheManager;
        public OkulManager(IOkulDal okulDal, IOkulTurService okulTurService, IKurumYorumService kurumYorumService, ICacheManager cacheManager)
        {
            _okulDal = okulDal;
            _okulTurService = okulTurService;
            _kurumYorumService = kurumYorumService;
            _cacheManager = cacheManager;
        }

        public IResult Add(Okul entity)
        {
            entity.Status = true;
            entity.CreatedDate= DateTime.Now;
            _okulDal.Add(entity);

            _cacheManager.Remove("Kurums");

            return new SuccessResult("Okul bilgisi eklendi.");
        }

        public IResult Delete(int id)
        {
            var result=_okulDal.Get(a=>a.Id==id);
            result.Status= false;
            result.DeletedDate= DateTime.Now;
            _okulDal.Update(result);

            _cacheManager.Remove("Kurums");

            return new SuccessResult("Okul bilgisi silindi.");
        }

        public IDataResult<List<Okul>> GetAll()
        {
            return new SuccessDataResult<List<Okul>>(_okulDal.GetAll(),"Okul listesi getirildi.");
        }

        public IDataResult<List<KurumDisplayDto>> GetAllKurum()
        {
            var result=_okulDal.GetAllKurum();
            return new SuccessDataResult<List<KurumDisplayDto>>(result, "Kurum listesi getirildi.");
        }

        public IDataResult<Okul> GetById(int id)
        {
            return new SuccessDataResult<Okul>(_okulDal.Get(a => a.Id == id), "Okul bilgisi getirildi.");
        }

        public IDataResult<KurumDisplayDto> GetByIdDisplay(int id)
        {
            var result = _okulDal.GetByIdDisplay(id);
            if (result.OkulTurId != 0)
            {
                var okulturu = _okulTurService.GetById(result.OkulTurId);
                result.OkulTurAdi = okulturu.Data.Tip;
            }
            return new SuccessDataResult<KurumDisplayDto>(result, "Kurum bilgileri getirildi.");
        }

        public IDataResult<List<KurumDisplayDto>> GetKursListFilter(KursAraDto model)
        {
            var result = _okulDal.GetKursListFilter(model);
            List<KurumDisplayDto> liste=new List<KurumDisplayDto>();
            if (model.KurumYorumSecenekKurs)
            {
                foreach (var item in result)
                {
                    var yorums = _kurumYorumService.GetCountByKurumId(item.Id);
                    if (yorums.Data > 20)
                        liste.Add(item);
                }
                return new SuccessDataResult<List<KurumDisplayDto>>(liste.OrderByDescending(a=>a.TikSayisi).ToList(), "Aranan kurs listesi getirildi.");
            }
            return new SuccessDataResult<List<KurumDisplayDto>>(result.OrderByDescending(a => a.TikSayisi).ToList(), "Aranan kurs listesi getirildi.");
        }

        public IDataResult<List<KurumDisplayDto>> GetOkulListFilter(OkulAraDto model)
        {
            var result = _okulDal.GetOkulListFilter(model);
            List<KurumDisplayDto> liste = new List<KurumDisplayDto>();
            if (model.KurumYorumSecenekOkul)
            {
                foreach (var item in result)
                {
                    var yorums = _kurumYorumService.GetCountByKurumId(item.Id);
                    if (yorums.Data > 20)
                        liste.Add(item);
                }
                return new SuccessDataResult<List<KurumDisplayDto>>(liste.OrderByDescending(a => a.TikSayisi).ToList(), "Aranan okul listesi getirildi.");
            }
            return new SuccessDataResult<List<KurumDisplayDto>>(result.OrderByDescending(a => a.TikSayisi).ToList(), "Aranan okul listesi getirildi.");
        }

        public IDataResult<List<KurumDisplayDto>> GetTikKurum4Take()
        {
            var list=new List<KurumDisplayDto>();
            if (!_cacheManager.IsAdd("Kurums"))
            {
                MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddDays(5);
                cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                list = _okulDal.GetTikKurum4Take();
                _cacheManager.Add("Kurums", list);
            }
            else
                list = _cacheManager.Get<List<KurumDisplayDto>>("Kurums");
            return new SuccessDataResult<List<KurumDisplayDto>>(list, "Kurum listesi getirildi.");
        }

        public IResult Update(Okul entity)
        {
            entity.Status= true;
            entity.UpdatedDate =DateTime.Now;
            _okulDal.Update(entity);

            _cacheManager.Remove("Kurums");

            return new SuccessResult("Okul bilgisi güncellendi.");
        }
    }
}
