using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using My_Books.Account.UserManager;
using My_Books.Data.Models;
using My_Books.Data.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_Books.Data.Services
{
    
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //Login method
        public async Task<UserManagerResponse> LoginUser(LoginVM model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                return new UserManagerResponse
                {
                    Message = "Email not found",
                    IsSuccess = false
                };
            }
            var result = await _userManager.CheckPasswordAsync(appUser, model.Password);
            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Credentials incorrects",
                    IsSuccess = false
                };
            }
            var claim = new[]
            {
                new Claim("Email",model.Email) ,
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
            };
            var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSetting:Key"]));
            var token = new JwtSecurityToken
           (
              issuer: _configuration["AuthSetting:Issuer"],
              audience: _configuration["AuthSetting:Audience"],
              claims: claim,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserManagerResponse
            {
                Message = tokenString,
                IsSuccess = true,
                Exparedate = token.ValidTo
            };
        }




        // register method
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterVM model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (model.Password != model.CheckPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm password does not match the password ",
                    IsSuccess = false,
                };
            }

            var appUser = new ApplicationUser
            {
                FullName=model.FullName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Registration has completed succussfely",
                    IsSuccess = true

                };

            }
            return new UserManagerResponse
            {
                Message = "registration doesn't completed",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
