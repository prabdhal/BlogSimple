using BlogSimpleMVC.Models;
using BlogSimpleMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogSimpleMVC.BusinessManagers.Interfaces
{
    public interface IPostBusinessManager
    {
        /// <summary>
        /// Returns paginated posts on the home view.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        HomeIndexViewModel GetHomeIndexViewModel(string searchString, int? page);
        /// <summary>
        /// Returns paginated and published posts.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Returns and displays a specified post .
        /// </summary>
        /// <param name="createViewModel"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<Post> CreatePost(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Returns and saves a comment to the database.
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<ActionResult<Comment>> CreateComment(PostViewModel postViewModel, ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Returns and updates a post to the database.
        /// </summary>
        /// <param name="editViewModel"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<ActionResult<EditViewModel>> UpdatePost(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Returns a post to be edited.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Returns and deletes a post.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        Task<ActionResult<ApplicationUser>> DeletePosts(ApplicationUser applicationUser);
        /// <summary>
        /// Returns and deletes a post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task<ActionResult<Post>> DeletePost(int postId);
        /// <summary>
        /// Returns and delete a comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<ActionResult<Comment>> DeleteComment(int commentId);
        /// <summary>
        /// Returns a specified post id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetPostId(int commentId);
        /// <summary>
        /// Deletes all user data (Posts, Comments and Replies).
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        ActionResult<ApplicationUser> DeleteAllUserPosts(ApplicationUser applicationUser);
    }
}
