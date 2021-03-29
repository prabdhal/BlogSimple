using BlogSimpleMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Services.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Returns a post along with the post author, comments, comment author, replies and reply parent details.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Post GetPost(int postId);
        /// <summary>
        /// Returns posts with matching search string within the post title, description and/or content.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        IEnumerable<Post> GetPosts(string searchString);
        /// <summary>
        /// Returns posts authored by the logged in admin user only.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        IEnumerable<Post> GetPosts(ApplicationUser applicationUser);
        /// <summary>
        /// Returns entire post model authored by the logged in admin user only.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        IEnumerable<Post> GetPostsFull(ApplicationUser applicationUser);
        /// <summary>
        /// Returns a comment along with the post, comment author and comment parent (if it is a reply).
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Comment GetComment(int commentId);
        /// <summary>
        /// Returns a list of comments for a specific post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IEnumerable<Comment> GetComments(int postId);
        /// <summary>
        /// Returns all comments of a single user.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        IEnumerable<Comment> GetComments(ApplicationUser applicationUser);
        /// <summary>
        /// Returns all replies of a single user.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        IEnumerable<Comment> GetReplies(ApplicationUser applicationUser);
        /// <summary>
        /// Returns a post which is added and saved into the database.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task<Post> Add(Post post);
        /// <summary>
        /// Returns a comment which is added and saved into the database. 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<Comment> Add(Comment comment);
        /// <summary>
        /// Returns a post which is updated and saved into the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task<Post> Update(Post post);
        /// <summary>
        /// Deletes a specified post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task<Post> Delete(Post post);
        /// <summary>
        /// Deletes a specified comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<Comment> Delete(Comment comment);
        /// <summary>
        /// Deletes a specified post without saving (for deleting several data within nested loops).
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Post DeleteNoSave(Post post);
        /// <summary>
        /// Deletes a specified comment without saving (for deleting several data within nested loops)..
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Comment DeleteNoSave(Comment comment);
    }
}
