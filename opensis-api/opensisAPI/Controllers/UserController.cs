using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.User.Interfaces;
using opensis.data.ViewModels.User;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/User")]
    [ApiController]    
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("ValidateLogin")]
        //public ActionResult<UserViewModel> ValidateLogin(UserViewModel objModel)
        //{
        //    return _userService.ValidateLogin(objModel);
        //}

        [HttpPost("ValidateLogin")]
        public async Task<ActionResult<LoginViewModel>> ValidateLogin(LoginViewModel objModel)
        {
            return await _userService.ValidateUserLogin(objModel);
        }
    }
}
