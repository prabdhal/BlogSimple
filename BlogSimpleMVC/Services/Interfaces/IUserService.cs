using BlogSimpleMVC.Models;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Services.Interfaces
{
    public interface IUserService
    {
        ApplicationUser Get(string userId);
        Task<ApplicationUser> Update(ApplicationUser applicationUser);
    }
}
