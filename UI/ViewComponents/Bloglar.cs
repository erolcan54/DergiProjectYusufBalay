using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class Bloglar:ViewComponent
    {
        private readonly IBlogService _blogService;

        public Bloglar(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _blogService.Get4LastList();
            return View(result.Data);
        }
    }
}
