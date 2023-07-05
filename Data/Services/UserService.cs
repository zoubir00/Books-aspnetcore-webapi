using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using My_Books.Account.UserManager;
using My_Books.Data.Models;
using My_Books.Data.ViewModels;
using My_Books.helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_Books.Data.Services
{
    
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private IConfiguration _configuration;
        private readonly JWT _jwt;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration,IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwt = jwt.Value;
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
                Exparedate = token.ValidTo, 
                User =appUser
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
               await _userManager.AddToRoleAsync(appUser, "User");
                var jwtsecurityToken = await CreateJwtToken(appUser);
                return new UserManagerResponse
                {
                    Message = "Registration has completed succussfely",
                    IsSuccess = true,
                    User = appUser,
                    Exparedate = jwtsecurityToken.ValidTo,
                    Roles = new List<string> { "User" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtsecurityToken)
                };

            }
            return new UserManagerResponse
            {
                Message = "registration doesn't completed",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSetting:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
