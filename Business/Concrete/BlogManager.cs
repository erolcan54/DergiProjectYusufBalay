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
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public IResult Add(Blog entity)
        {
            entity.CreatedDate= DateTime.Now;
            entity.Status = true;
            entity.Hit = 0;
            _blogDal.Add(entity);

            return new SuccessResult("Blog yazısı eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _blogDal.Get(a => a.Id == id);
            result.Status= false;
            result.DeletedDate= DateTime.Now;
            _blogDal.Update(result);

            return new SuccessResult("Blog silindi.");
        }

        public IDataResult<List<Blog>> Get4LastList()
        {
            var list= new List<Blog>();
            
                list = _blogDal.GetAll(a => a.Status).OrderByDescending(a => a.Id).Take(4).ToList();
           
         
            
            return new SuccessDataResult<List<Blog>>(list);
        }

        public IDataResult<List<Blog>> GetAll()
        {
            return new SuccessDataResult<List<Blog>>(_blogDal.GetAll(),"Blog listesi getirildi.");
        }

        public IDataResult<Blog> GetById(int id)
        {
            return new SuccessDataResult<Blog>(_blogDal.Get(a => a.Id == id), "Blog getirildi.");
        }

        public IResult Update(Blog entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            _blogDal.Update(entity);


            return new SuccessResult("Blog güncellendi.");
        }
    }
}
