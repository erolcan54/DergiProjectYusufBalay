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
    public interface IOzelDersOgretmenService
    {
        IResult Add(OzelDersOgretmen entity);
        IDataResult<OzelDersOgretmen> GetById(int id);
        IResult Update(OzelDersOgretmen entity);
        IDataResult<List<OzelDersOgretmen>> GetAll();
        IResult Delete(int id);
        IDataResult<List<OzelDersOgretmen>> Get6LastList();
        IDataResult<List<OzelDersOgretmenDto>> GetAllDisplay4Take();
        IDataResult<List<OzelDersOgretmenDto>> GetAllDisplay();
        IDataResult<OzelDersOgretmenDto> GetByIdDisplay(int id);
        IDataResult<List<OzelDersOgretmenDto>> GetAllOzelOgretmenFiltre(OzelOgretmenFiltreDto filtre);
    }
}
