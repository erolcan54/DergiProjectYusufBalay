using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Business.Concrete
{
    public class KurumsalManager:IKurumsalService
    {
        private IKurumsalDal _kurumsalDal;

        public KurumsalManager(IKurumsalDal kurumsalDal)
        {
            _kurumsalDal = kurumsalDal;
        }

        public IDataResult<Kurumsal> GetAll()
        {
            var result = _kurumsalDal.GetAll().FirstOrDefault();
            return new SuccessDataResult<Kurumsal>(result);
        }

        public IResult Update(Kurumsal entity)
        {
            _kurumsalDal.Update(entity);
            return new SuccessResult("Kurumsal bilgiler güncellendi.");
        }
    }
}
