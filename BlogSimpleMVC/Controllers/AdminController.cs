using BlogSimpleMVC.BusinessManagers.Interfaces;
using BlogSimpleMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBusinessManager adminBusinessManager;

        public AdminController(IAdminBusinessManager adminBusinessManager)
        {
            this.adminBusinessManager = adminBusinessManager;
        }

        // GET: /Admin/Index
        public async Task<IActionResult> Index()
        {
            return View(await adminBusinessManager.GetAdminDashboard(User));
        }

        // GET: /Admin/About
        public async Task<IActionResult> About()
        {
            return View(await adminBusinessManager.GetAboutViewModel(User));
        }

        // POST: /Admin/About
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutViewModel aboutViewModel)
        {
            if (!ModelState.IsValid)
                return View("About");

            await adminBusinessManager.UpdateAbout(aboutViewModel, User);
            return RedirectToAction("About");
        }
    }
}
