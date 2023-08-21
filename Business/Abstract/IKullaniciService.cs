using Core.Utilities.Results;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IKullaniciService
    {
        IResult Add(Kullanici entity);
        IDataResult<Kullanici> GetById(int id);
        IResult Update(Kullanici entity);
        IDataResult<List<Kullanici>> GetAll();
        IDataResult<List<Kullanici>> GetByListStatusTrue();
        IResult Delete(int id);
        IDataResult<Kullanici> GirisKontrol(GirisModelDto model);
    }
}
