using Business.Abstract;
using Business.MemoryCaching;
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
    public class SliderManager:ISliderService
    {
        private readonly ISliderDal _sliderDal;
        private ICacheManager _cacheManager;

        public SliderManager(ISliderDal sliderDal, ICacheManager cacheManager)
        {
            _sliderDal = sliderDal;
            _cacheManager = cacheManager;
        }

        public IResult Add(Slider entity)
        {
            _sliderDal.Add(entity);
            _cacheManager.Remove("Sliders");
            return new SuccessResult("Slider Resmi eklendi.");
        }

        public IResult Delete(int id)
        {
            var result=_sliderDal.Get(a=>a.Id == id);
            result.Status = false;
            _sliderDal.Update(result);
            _cacheManager.Remove("Sliders");
            return new SuccessResult("Resim silindi.");
        }

        public IDataResult<List<Slider>> GetAll()
        {
           List<Slider> result = new List<Slider>();
            if (!_cacheManager.IsAdd("Sliders"))
            {
                result = _sliderDal.GetAll(a => a.Status).OrderByDescending(a => a.Id).ToList();
                _cacheManager.Add("Sliders", result);
            }
            else
                result = _cacheManager.Get<List<Slider>>("Sliders");
            return new SuccessDataResult<List<Slider>>(result,"Slider resimleri getirildi.");
        }

        public IDataResult<Slider> GetById(int id)
        {
            return new SuccessDataResult<Slider>(_sliderDal.Get(a => a.Id == id), "Resim getirildi.");
        }

        public IResult Update(Slider entity)
        {
            _cacheManager.Remove("Sliders");
            _sliderDal.Update(entity); return new SuccessResult("Resim güncellendi.");
        }
    }
}
