using BlogSimpleMVC.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogSimpleMVC.BusinessManagers.Interfaces
{
    public interface IAdminBusinessManager
    {
        /// <summary>
        /// Returns all the posts posted by the logged in admin in the admin dashboard view page.
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<AdminIndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Returns all the author details in the author view page.
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<AboutViewModel> GetAboutViewModel(ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// Applies edited detail changes to the author model in database.
        /// </summary>
        /// <param name="aboutViewModel"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task UpdateAbout(AboutViewModel aboutViewModel, ClaimsPrincipal claimsPrincipal);
    }
}
