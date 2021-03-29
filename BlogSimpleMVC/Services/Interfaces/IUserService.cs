using BlogSimpleMVC.Models;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Returns the searched admin user if it exists.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ApplicationUser Get(string userId);
        /// <summary>
        /// Returns an admin user which is updated and saved into the database
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        Task<ApplicationUser> Update(ApplicationUser applicationUser);
    }
}
