using BlogSimpleMVC.Models;
using System.Collections.Generic;

namespace BlogSimpleMVC.ViewModels
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
