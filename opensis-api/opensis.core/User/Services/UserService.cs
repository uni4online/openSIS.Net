using opensis.core.helper;
using opensis.core.User.Interfaces;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opensis.core.User.Services
{
    public class UserService : IUserService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";
        public IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

      
        public LoginViewModel ValidateUserLogin(LoginViewModel ObjModel)
        {
            logger.Info("Method ValidateLogin called.");
            LoginViewModel ReturnModel = new LoginViewModel();
            try
            {
                ReturnModel = this.userRepository.ValidateUserLogin(ObjModel);
                if (ReturnModel._failure == false)
                {
                    ReturnModel._token = TokenManager.GenerateToken(ReturnModel._tenantName);
                    logger.Info("Method ValidateLogin end with success.");
                }

            }
            catch (Exception ex)
            {
                ReturnModel._failure = true;
                ReturnModel._message = ex.Message;
                logger.Info("Method getAllSchools end with error :" + ex.Message);
            }


            return ReturnModel;
        }

        public CheckUserEmailAddressViewModel CheckUserLoginEmail(CheckUserEmailAddressViewModel checkUserEmailAddressViewModel)
        {
            CheckUserEmailAddressViewModel checkUserLoginEmail = new CheckUserEmailAddressViewModel();
            if (TokenManager.CheckToken(checkUserEmailAddressViewModel._tenantName, checkUserEmailAddressViewModel._token))
            {
                checkUserLoginEmail = this.userRepository.CheckUserLoginEmail(checkUserEmailAddressViewModel);
            }
            else
            {
                checkUserLoginEmail._failure = true;
                checkUserLoginEmail._message = TOKENINVALID;
            }
            return checkUserLoginEmail;
        }
    }
}
