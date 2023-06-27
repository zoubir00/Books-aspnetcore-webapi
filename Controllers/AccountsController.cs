using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Account.UserManager;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _service;

        public AccountsController(IUserService service)
        {
            _service = service;
        }


        // Registration Method
        [HttpPost("Register")]
        public async Task<ActionResult<UserManagerResponse>> RegisterAsync(RegisterVM model)
        {
            
            if (ModelState.IsValid)
            {
                var rslt =await _service.RegisterUserAsync(model);
                if (rslt.IsSuccess)
                {
                    return Ok(rslt);
                }
                return BadRequest(rslt);

            }
            return BadRequest("some properties are not valid");
        }


        // Login 
        [HttpPost("Login")]
        public async Task<ActionResult<UserManagerResponse>> LoginUser(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                var rslt = await _service.LoginUser(model);
                if (rslt.IsSuccess)
                {
                    return Ok(rslt);
                }
                return BadRequest(rslt);

            }
            return BadRequest("some properties are not valid");
        }
    }
}
