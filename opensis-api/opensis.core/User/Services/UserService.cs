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
        public IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserViewModel ValidateLogin(UserViewModel ObjModel)
        {
            logger.Info("Method ValidateLogin called.");
            UserViewModel ReturnModel = new UserViewModel();
            try
            {
                               
                ReturnModel=this.userRepository.ValidateLogin(ObjModel);
                if(ReturnModel._failure==false)
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
        public async Task<LoginViewModel> ValidateUserLogin(LoginViewModel ObjModel)
        {
            logger.Info("Method ValidateLogin called.");
            LoginViewModel ReturnModel = new LoginViewModel();
            try
            {
                ReturnModel = await this.userRepository.ValidateUserLogin(ObjModel);
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
    }
}
