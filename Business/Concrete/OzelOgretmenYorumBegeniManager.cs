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

namespace Business.Concrete
{
    public class OzelOgretmenYorumBegeniManager:IOzelOgretmenYorumBegeniService
    {
        private IOzelOgretmenYorumBegeniDal _ozelOgretmenYorumBegeniDal;

        public OzelOgretmenYorumBegeniManager(IOzelOgretmenYorumBegeniDal ozelOgretmenYorumBegeniDal)
        {
            _ozelOgretmenYorumBegeniDal = ozelOgretmenYorumBegeniDal;
        }

        public IResult Add(OzelOgretmenYorumBegeni entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            _ozelOgretmenYorumBegeniDal.Add(entity);
            return new SuccessResult("Yorum begenisi eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _ozelOgretmenYorumBegeniDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _ozelOgretmenYorumBegeniDal.Update(result);
            return new SuccessResult("Yorum beğenisi silindi.");
        }

        public IDataResult<List<OzelOgretmenYorumBegeni>> GetAll()
        {
            return new SuccessDataResult<List<OzelOgretmenYorumBegeni>>(_ozelOgretmenYorumBegeniDal.GetAll(), "Yorum begeni listesi getirildi.");
        }

        public IDataResult<List<OzelOgretmenYorumBegeni>> GetAllByYorumId(int id)
        {
            return new SuccessDataResult<List<OzelOgretmenYorumBegeni>>(_ozelOgretmenYorumBegeniDal.GetAll(a => a.YorumId == id && a.Status), "Yorum begeni listesi getirildi.");
        }

        public IDataResult<OzelOgretmenYorumBegeni> GetById(int id)
        {
            return new SuccessDataResult<OzelOgretmenYorumBegeni>(_ozelOgretmenYorumBegeniDal.Get(a => a.Id == id), "Yorum begenisi getirildi.");
        }

        public IDataResult<int> GetByYorumIdBegeniAvarege(int id)
        {
            int avarege = 0;
            var result = _ozelOgretmenYorumBegeniDal.GetAll(a => a.YorumId == id && a.Status);
            if(result.Count > 0)
                avarege = Convert.ToInt32(result.Average(a => a.Begeni));
            return new SuccessDataResult<int>(avarege, "Yorum beğeni ortalaması getirildi.");
        }

        public IResult IPAddressControltoYorum(int yorumId, string IPAddress)
        {
            var result = _ozelOgretmenYorumBegeniDal.Get(a => a.IPAddress == IPAddress && a.YorumId == yorumId && a.Status);
            if (result != null)
                return new SuccessResult();
            return new ErrorResult();
        }

        public IResult Update(OzelOgretmenYorumBegeni entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _ozelOgretmenYorumBegeniDal.Update(entity);
            return new SuccessResult("Yorum beğenisi güncellendi.");
        }
    }
}
