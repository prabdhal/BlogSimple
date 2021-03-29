using BlogSimpleMVC.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogSimpleMVC.ViewModels
{
    public class AboutViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Header Image")]
        public IFormFile HeaderImage { get; set; }

        public string SubHeader { get; set; }

        public string Content { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        [Display(Name = "Portfolio Website")]
        public string PortfolioWebsite { get; set; }
    }
}
