using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPopupModalService
    {
        IResult Add(PopupModal entity);
        IDataResult<PopupModal> GetById(int id);
        IDataResult<PopupModal> GetByStatus();
        IResult Delete(int id);
        IDataResult<List<PopupModal>> GetAll();
    }
}
