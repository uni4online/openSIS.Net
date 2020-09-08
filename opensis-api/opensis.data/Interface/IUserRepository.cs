using opensis.data.Models;
using opensis.data.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.data.Interface
{
    public interface IUserRepository
    {
        public UserViewModel ValidateLogin(UserViewModel objmodel);

        Task<LoginViewModel> ValidateUserLogin(LoginViewModel objmodel);
    }
}
