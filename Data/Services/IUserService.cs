using Microsoft.AspNetCore.Identity;
using My_Books.Account.UserManager;
using My_Books.Data.Models;
using My_Books.Data.ViewModels;

namespace My_Books.Data.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterVM model);
    }

    
}
