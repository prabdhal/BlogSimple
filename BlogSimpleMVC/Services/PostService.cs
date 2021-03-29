using BlogSimpleMVC.Data;
using BlogSimpleMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PostService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public Post GetPost(int postId)
        {
            return applicationDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Replies)
                        .ThenInclude(r => r.Parent)
                .FirstOrDefault(p => p.Id == postId);
        }

        public IEnumerable<Post> GetPosts(string searchString)
        {
            return applicationDbContext.Posts
                .OrderByDescending(p => p.UpdatedOn)
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .Where(p => p.Title.Contains(searchString) ||
                            p.Description.Contains(searchString) ||
                            p.Content.Contains(searchString));
        }

        public IEnumerable<Post> GetPosts(ApplicationUser applicationUser)
        {
            return applicationDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .Where(p => p.Author == applicationUser);
        }

        public IEnumerable<Post> GetPostsFull(ApplicationUser applicationUser)
        {
            return applicationDbContext.Posts
                .Where(p => p.Author == applicationUser);
        }

        public Comment GetComment(int commentId)
        {
            return applicationDbContext.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .Include(c => c.Parent)
                .FirstOrDefault(c => c.Id == commentId);
        }

        public IEnumerable<Comment> GetComments(int postId)
        {
            return applicationDbContext.Comments
                .Where(c => c.Post.Id == postId);
        }

        public IEnumerable<Comment> GetComments(ApplicationUser applicationUser)
        {
            return applicationDbContext.Comments
                .Where(c => c.Parent == null)
                .Where(c => c.Author == applicationUser);
        }

        public IEnumerable<Comment> GetReplies(ApplicationUser applicationUser)
        {
            return applicationDbContext.Comments
                .Where(c => c.Parent != null)
                .Where(c => c.Author == applicationUser);
        }

        public async Task<Post> Add(Post post)
        {
            applicationDbContext.Add(post);
            await applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Comment> Add(Comment comment)
        {
            applicationDbContext.Comments.Add(comment);
            await applicationDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Post> Update(Post post)
        {
            applicationDbContext.Update(post);
            await applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post> Delete(Post post)
        {
            applicationDbContext.Remove(post);
            await applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Comment> Delete(Comment comment)
        {
            applicationDbContext.Remove(comment);
            await applicationDbContext.SaveChangesAsync();
            return comment;
        }

        public Post DeleteNoSave(Post post)
        {
            applicationDbContext.Remove(post);
            return post;
        }

        public Comment DeleteNoSave(Comment comment)
        {
            applicationDbContext.Remove(comment);
            return comment;
        }
    }
}
