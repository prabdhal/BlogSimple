using BlogSimpleMVC.Authorization;
using BlogSimpleMVC.BusinessManagers.Interfaces;
using BlogSimpleMVC.Models;
using BlogSimpleMVC.Services.Interfaces;
using BlogSimpleMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogSimpleMVC.BusinessManagers
{
    public class PostBusinessManager : IPostBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAuthorizationService authorizationService;

        public PostBusinessManager(UserManager<ApplicationUser> userManager,
            IPostService postService,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment,
            IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.userService = userService;
            this.webHostEnvironment = webHostEnvironment;
            this.authorizationService = authorizationService;
        }

        public HomeIndexViewModel GetHomeIndexViewModel(string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var posts = postService.GetPosts(searchString ?? string.Empty)
                .Where(p => p.Published);

            return new HomeIndexViewModel
            {
                // paginates posts
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }

        public async Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var postId = id.Value;

            var post = postService.GetPost(postId);

            if (post is null)
                return new NotFoundResult();

            if (!post.Published)
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Read);

                if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);
            }

            return new PostViewModel
            {
                Post = post
            };
        }

        public async Task<Post> CreatePost(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Post post = createViewModel.Post;

            post.Author = await userManager.GetUserAsync(claimsPrincipal);
            post.CreatedOn = DateTime.Now;
            post.UpdatedOn = DateTime.Now;

            post = await postService.Add(post);

            string webRootPath = webHostEnvironment.WebRootPath;
            string pathToImage = $@"{webRootPath}\UserFiles\Posts\{post.Id}\HeaderImage.jpg";

            EnsureFolder(pathToImage);

            if (createViewModel.HeaderImage is null)
                return post;

            using (var fileStream = new FileStream(pathToImage, FileMode.Create))
            {
                await createViewModel.HeaderImage.CopyToAsync(fileStream);
            }

            return post;
        }

        public async Task<ActionResult<Comment>> CreateComment(PostViewModel postViewModel, ClaimsPrincipal claimsPrincipal)
        {
            if (postViewModel.Post is null || postViewModel.Post.Id == 0)
                return new BadRequestResult();

            var post = postService.GetPost(postViewModel.Post.Id);

            if (post is null)
                return new NotFoundResult();

            var comment = postViewModel.Comment;

            comment.Author = await userManager.GetUserAsync(claimsPrincipal);
            comment.Post = post;
            comment.CreatedOn = DateTime.Now;

            if (comment.Parent != null)
            {
                comment.Parent = postService.GetComment(comment.Parent.Id);
            }

            return await postService.Add(comment);
        }

        public async Task<ActionResult<EditViewModel>> UpdatePost(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var post = postService.GetPost(editViewModel.Post.Id);

            if (post is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            post.Published = editViewModel.Post.Published;
            post.Title = editViewModel.Post.Title;
            post.Description = editViewModel.Post.Description;
            post.Content = editViewModel.Post.Content;
            post.UpdatedOn = DateTime.Now;

            if (editViewModel.HeaderImage != null)
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Posts\{post.Id}\HeaderImage.jpg";

                EnsureFolder(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await editViewModel.HeaderImage.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                Post = await postService.Update(post)
            };
        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var postId = id.Value;

            var post = postService.GetPost(postId);

            if (post is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, post, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            return new EditViewModel
            {
                Post = post
            };
        }

        public async Task<ActionResult<ApplicationUser>> DeletePosts(ApplicationUser applicationUser)
        {
            var user = userService.Get(applicationUser.Id);
            if (user is null)
                return new NotFoundResult();

            var posts = postService.GetPosts(applicationUser);

            // delete all posts of the user
            foreach (var post in posts)
            {
                await DeletePost(post.Id);
            }

            return user;
        }

        public async Task<ActionResult<Post>> DeletePost(int postId)
        {
            var post = postService.GetPost(postId);
            if (post is null)
                return new NotFoundResult();

            // delete all comments of the post 
            foreach (var comment in post.Comments)
            {
                await DeleteComment(comment.Id);
            }

            // delete the post
            await postService.Delete(post);

            return post;
        }

        public async Task<ActionResult<Comment>> DeleteComment(int commentId)
        {
            var comment = postService.GetComment(commentId);
            if (comment is null)
                return new NotFoundResult();

            // Error 500 for some reason
            DeleteReplies(comment);

            await postService.Delete(comment);
            return comment;
        }

        public int GetPostId(int commentId)
        {
            var comment = postService.GetComment(commentId);

            return comment.Post.Id;
        }

        private Comment DeleteReplies(Comment comment)
        {
            var replies = postService.GetComments(comment.Post.Id)
                            .Where(c => c.Parent != null);

            if (replies is null)
                return null;

            // delete all replies from the comment
            foreach (var reply in replies)
            {
                if (reply.Parent.Id == comment.Id)
                {
                    postService.DeleteNoSave(reply);
                }
            }

            return comment;
        }

        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }

        public ActionResult<ApplicationUser> DeleteAllUserPosts(ApplicationUser applicationUser)
        {
            var user = userService.Get(applicationUser.Id);
            if (user is null)
                return new NotFoundResult();

            var posts = postService.GetPostsFull(applicationUser);

            var comments = postService.GetComments(applicationUser);

            var replies = postService.GetReplies(applicationUser);

            // delete all replies of the user
            if (replies is not null)
            {
                foreach (var reply in replies)
                {
                    postService.DeleteNoSave(reply);
                }
            }

            // delete all comments of the user
            if (comments is not null)
            {
                foreach (var comment in comments)
                {
                    postService.DeleteNoSave(comment);
                }
            }

            // delete all posts of the user
            if (posts is not null)
            {
                foreach (var post in posts)
                {
                    postService.DeleteNoSave(post);
                }
            }

            return user;
        }
    }
}
