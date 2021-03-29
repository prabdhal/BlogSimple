using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogSimpleMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [StringLength(100)]
        public string SubHeader { get; set; }

        public string AboutContent { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        [Display(Name = "Portfolio Website")]
        public string PortfolioWebsite { get; set; }
    }
}
