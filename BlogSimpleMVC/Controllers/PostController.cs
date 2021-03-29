using BlogSimpleMVC.BusinessManagers.Interfaces;
using BlogSimpleMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostBusinessManager postBusinessManager;

        public PostController(IPostBusinessManager postBusinessManager)
        {
            this.postBusinessManager = postBusinessManager;
        }

        // GET: Post/{id}
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            var actionResult = await postBusinessManager.GetPostViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        // GET: Post/Edit/{id}
        public async Task<ActionResult> Edit(int? id)
        {
            var actionResult = await postBusinessManager.GetEditViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        // POST: Post/Add
        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createViewModel)
        {
            if (!ModelState.IsValid)
                return View("Create");

            await postBusinessManager.CreatePost(createViewModel, User);
            return RedirectToAction("Index", new { createViewModel.Post.Id });
        }

        // PUT: Post/Update
        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            if (!ModelState.IsValid)
                return View("Edit");

            var actionResult = await postBusinessManager.UpdatePost(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Index", new { editViewModel.Post.Id });

            return actionResult.Result;
        }

        // POST: Post/Comment
        [HttpPost]
        public async Task<IActionResult> Comment(PostViewModel postViewModel)
        {
            var actionResult = await postBusinessManager.CreateComment(postViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Index", new { postViewModel.Post.Id });

            return actionResult.Result;
        }

        // DELETE: Post/DeletePost/{id}
        public async Task<IActionResult> DeletePost(int id)
        {
            var actionResult = await postBusinessManager.DeletePost(id);

            if (actionResult.Result is null)
                return RedirectToAction("Index", "Admin");

            return actionResult.Result;
        }

        // DELETE: Post/DeleteComment/{id}
        public async Task<IActionResult> DeleteComment(int id)
        {
            var postId = postBusinessManager.GetPostId(id);
            var actionResult = await postBusinessManager.DeleteComment(id);

            if (actionResult.Result is null)
                return RedirectToAction("Index", new { id = postId });

            return actionResult.Result;
        }
    }
}
