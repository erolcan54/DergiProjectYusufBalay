using Core.Utilities.Results;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IYoneticiService
    {
        IResult Add(Yonetici entity);
        IDataResult<Yonetici> GetById(int id);
        IResult Update(Yonetici entity);
        IDataResult<List<Yonetici>> GetAll();
        IDataResult<List<Yonetici>> GetByListStatusTrue();
        IResult Delete(int id);
        IDataResult<Yonetici> GirisKontrol(GirisModelDto model);
    }
}
