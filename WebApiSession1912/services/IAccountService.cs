using WebApiSession1912.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSession1912.services
{
    public interface IAccountService
    {
        Task<IdentityResult> Create(SignUpModel model);

        SignInResult SignIn(SignInModel model);

        Task<IdentityResult> AddRole(RoleModel model);

        List<IdentityRole> GetRole();

        Task Signout();

        ApplicationUser GetUserByName(string Username);

        List<string> GetUserRoles(ApplicationUser obj);
    }
}
