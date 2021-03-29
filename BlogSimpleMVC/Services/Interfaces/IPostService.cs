using BlogSimpleMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Services.Interfaces
{
    public interface IPostService
    {
        Post GetPost(int postId);
        IEnumerable<Post> GetPosts(string searchString);

        IEnumerable<Post> GetPosts(ApplicationUser applicationUser);

        IEnumerable<Post> GetPostsFull(ApplicationUser applicationUser);

        Comment GetComment(int commentId);

        IEnumerable<Comment> GetComments(int postId);

        IEnumerable<Comment> GetComments(ApplicationUser applicationUser);

        IEnumerable<Comment> GetReplies(ApplicationUser applicationUser);

        Task<Post> Add(Post post);

        Task<Comment> Add(Comment comment);

        Task<Post> Update(Post post);

        Task<Post> Delete(Post post);

        Task<Comment> Delete(Comment comment);

        Post DeleteNoSave(Post post);

        Comment DeleteNoSave(Comment comment);
    }
}
