using Business.Abstract;
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
    public class PopupModalManager:IPopupModalService
    {
        private IPopupModalDal _popupModalDal;

        public PopupModalManager(IPopupModalDal popupModalDal)
        {
            _popupModalDal = popupModalDal;
        }

        public IResult Add(PopupModal entity)
        {
            if (_popupModalDal.GetAll().Count > 0)
            {
                var result = _popupModalDal.Get(a => a.Status);
                result.Status = false;
                result.DeletedDate = DateTime.Now;
                _popupModalDal.Update(result);
            }
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _popupModalDal.Add(entity);
            return new SuccessResult("Popup Modal bilgileri eklendi.");
        }

        public IResult Delete(int id)
        {
            var result=_popupModalDal.Get(a=>a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _popupModalDal.Update(result);
            return new SuccessResult("Popup Modal bilgisi silindi.");
        }

        public IDataResult<List<PopupModal>> GetAll()
        {
            return new SuccessDataResult<List<PopupModal>>(_popupModalDal.GetAll(a=>a.Status),"Popup Modal listesi getirildi.");
        }

        public IDataResult<PopupModal> GetById(int id)
        {
            var result = _popupModalDal.Get(a => a.Id == id);
            return new SuccessDataResult<PopupModal>(result,"Popup Modal bilgisi getirildi.");
        }

        public IDataResult<PopupModal> GetByStatus()
        {
            var result = _popupModalDal.Get(a => a.Status && a.SonTarih>DateTime.Now);
            return new SuccessDataResult<PopupModal>(result, "Popup Modal bilgisi getirildi.");
        }
    }
}
