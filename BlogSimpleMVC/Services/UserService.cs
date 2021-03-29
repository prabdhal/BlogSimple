using BlogSimpleMVC.Data;
using BlogSimpleMVC.Models;
using BlogSimpleMVC.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSimpleMVC.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public ApplicationUser Get(string userId)
        {
            return applicationDbContext.Users
                .FirstOrDefault(u => u.Id == userId);
        }

        public async Task<ApplicationUser> Update(ApplicationUser applicationUser)
        {
            applicationDbContext.Update(applicationUser);
            await applicationDbContext.SaveChangesAsync();
            return applicationUser;
        }
    }
}
