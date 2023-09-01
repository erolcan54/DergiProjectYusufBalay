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
    public class KurumYorumBegeniManager:IKurumYorumBegeniService
    {
        private IKurumYorumBegeniDal _kurumYorumBegeniDal;

        public KurumYorumBegeniManager(IKurumYorumBegeniDal kurumYorumBegeniDal)
        {
            _kurumYorumBegeniDal = kurumYorumBegeniDal;
        }

        public IResult Add(KurumYorumBegeni entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = true;
            if (IPAddressControltoYorum(entity.YorumId, entity.IPAddress).Success)
                return new ErrorResult("Daha önce bu yorum için değerlendirme yapmışsınız.");
            _kurumYorumBegeniDal.Add(entity);
            return new SuccessResult("Yorum begenisi eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _kurumYorumBegeniDal.Get(a => a.Id == id);
            result.Status = false;
            result.DeletedDate = DateTime.Now;
            _kurumYorumBegeniDal.Update(result);
            return new SuccessResult("Yorum beğenisi silindi.");
        }

        public IDataResult<List<KurumYorumBegeni>> GetAll()
        {
            return new SuccessDataResult<List<KurumYorumBegeni>>(_kurumYorumBegeniDal.GetAll(), "Yorum begeni listesi getirildi.");
        }

        public IDataResult<List<KurumYorumBegeni>> GetAllByYorumId(int id)
        {
            return new SuccessDataResult<List<KurumYorumBegeni>>(_kurumYorumBegeniDal.GetAll(a => a.YorumId == id && a.Status), "Yorum begeni listesi getirildi.");
        }

        public IDataResult<KurumYorumBegeni> GetById(int id)
        {
            return new SuccessDataResult<KurumYorumBegeni>(_kurumYorumBegeniDal.Get(a => a.Id == id), "Yorum begenisi getirildi.");
        }

        public IDataResult<int> GetByYorumIdBegeniAvarege(int id)
        {
            int avarege = 0;
            var result = _kurumYorumBegeniDal.GetAll(a => a.YorumId == id && a.Status);
            if (result.Count > 0)
                avarege = Convert.ToInt32(result.Average(a => a.Begeni));
            return new SuccessDataResult<int>(avarege, "Yorum beğeni ortalaması getirildi.");
        }

        public IResult IPAddressControltoYorum(int yorumId, string IPAddress)
        {
            var result = _kurumYorumBegeniDal.Get(a => a.IPAddress == IPAddress && a.YorumId == yorumId && a.Status);
            if (result != null)
                return new SuccessResult();
            return new ErrorResult();
        }

        public IResult Update(KurumYorumBegeni entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _kurumYorumBegeniDal.Update(entity);
            return new SuccessResult("Yorum beğenisi güncellendi.");
        }
    }
}
