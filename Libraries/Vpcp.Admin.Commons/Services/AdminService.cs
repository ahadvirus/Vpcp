using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Vpcp.Admin.Commons.Models.ViewModels;

namespace Vpcp.Admin.Commons.Services
{
    public class AdminService
    {
        

        public async Task<bool> Add(AdminVM entity, IdentityUser user, PasswordVM password)
        {
            bool result = false;
            // Checking Admin
            // Permission
            // Bad Words
            // Credit
            // Creating

            return result;
        }
    }
}
