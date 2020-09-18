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

        /// <summary>
        /// This is used for authentcatred the login process
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>

        [HttpPost("ValidateLogin")]
        public ActionResult<LoginViewModel> ValidateLogin(LoginViewModel objModel)
        {
            return  _userService.ValidateUserLogin(objModel);
        }
    }
}
