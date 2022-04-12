using WebApiSession1912.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSession1912.services
{
    public class AccountService: IAccountService
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public AccountService(UserManager<ApplicationUser> _UserManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole> _roleManager
            )
        {
            userManager = _UserManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> Create(SignUpModel model)
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = model.Email;
            //user.PasswordHash = model.Password;
            user.Name = model.Name;
            user.UserName = model.Username;

             var result= await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var role = await roleManager.FindByIdAsync(model.Role_Id);

                result=await userManager.AddToRoleAsync(user, role.Name);

            }

            return result;
        }

        public SignInResult SignIn(SignInModel model)
        {
            var result = signInManager.PasswordSignInAsync(model.Username, model.Password, false, false).Result;
            return result;
        }

        public async Task<IdentityResult> AddRole(RoleModel model)
        {
            IdentityRole role = new IdentityRole();
            role.Name = model.Name;

            var result= await roleManager.CreateAsync(role);
            return result;
        }

        public List<IdentityRole> GetRole()
        {
            List<IdentityRole> liRole= roleManager.Roles.ToList();
            return liRole;
        }

        public async Task Signout()
        {
           await signInManager.SignOutAsync();
        }

        public ApplicationUser GetUserByName(string Username)
        {
            var user=userManager.FindByNameAsync(Username).Result;
            return user;
        }

        public List<string> GetUserRoles(ApplicationUser obj)
        {
            var user = userManager.GetRolesAsync(obj).Result;
            return user.ToList();
        }





    }
}
