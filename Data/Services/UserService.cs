using Microsoft.AspNetCore.Identity;
using My_Books.Account.UserManager;
using My_Books.Data.Models;
using My_Books.Data.ViewModels;

namespace My_Books.Data.Services
{
    
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
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
