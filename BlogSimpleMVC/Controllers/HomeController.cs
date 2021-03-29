using BlogSimpleMVC.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostBusinessManager postBusinessManager;
        private readonly IHomeBusinessManager homeBusinessManager;

        public HomeController(
            IPostBusinessManager postBusinessManager,
            IHomeBusinessManager homeBusinessManager)
        {
            this.postBusinessManager = postBusinessManager;
            this.homeBusinessManager = homeBusinessManager;
        }

        // GET: Home/Index
        public IActionResult Index(string searchString, int? page)
        {
            return View(postBusinessManager.GetHomeIndexViewModel(searchString, page));
        }

        // GET: Home/Author/{id}
        public IActionResult Author(string authorId, string searchString, int? page)
        {
            var actionResult = homeBusinessManager.GetAuthorViewModel(authorId, searchString, page);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }
    }
}
