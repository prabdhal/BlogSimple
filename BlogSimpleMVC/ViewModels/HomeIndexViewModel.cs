using BlogSimpleMVC.Models;
using PagedList.Core;

namespace BlogSimpleMVC.ViewModels
{
    public class HomeIndexViewModel
    {
        public IPagedList<Post> Posts { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
    }
}
