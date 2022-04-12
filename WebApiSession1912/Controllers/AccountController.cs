using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiSession1912.Models;
using WebApiSession1912.services;

namespace WebApiSession1912.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService _accountService, IConfiguration _configuration)
        {
            accountService = _accountService;
            configuration = _configuration;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task SignUp(SignUpModel signUpModel)
        {
            var result = await accountService.Create(signUpModel);

        }

        [HttpGet]
        [Route("getRoles")]
        public List<IdentityRole> getRoles()
        {
            var liRole = accountService.GetRole();
            return liRole;
        }

        [HttpPost]
        [Route("NewRole")]
        public async Task NewRole(RoleModel model)
        {
            var result = await accountService.AddRole(model);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(SignInModel model)
        {
            var result = accountService.SignIn(model);

            if (result.Succeeded) {

                ApplicationUser user = accountService.GetUserByName(model.Username);
                
                var userRoles= accountService.GetUserRoles(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(15),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            else
            {
                return Unauthorized();
            }


        }
    }
}
