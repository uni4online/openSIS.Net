using opensis.data.Models;
using opensis.data.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.core.User.Interfaces
{
    public interface IUserService
    {
        public UserViewModel ValidateLogin(UserViewModel ObjModel);
        public Task<LoginViewModel> ValidateUserLogin(LoginViewModel ObjModel);
    }
}
