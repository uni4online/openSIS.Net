using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace opensis.data.Repository
{
    public class UserRepository : IUserRepository
    {
        private CRMContext context;
        private static string EMAILMESSAGE = "Email address is not registered in the system";
        private static string PASSWORDMESSAGE = "Email + password combination is incorrect, try again";
        public UserRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        public UserViewModel ValidateLogin(UserViewModel objModel)
        {
            UserViewModel ReturnModel = new UserViewModel();
            try
            {
                ReturnModel._tenantName = objModel._tenantName;
                ReturnModel.user = this.context?.tblUser.SingleOrDefault(x => x.user_name == objModel.user.user_name && x.isactive == true);
                if (ReturnModel.user == null)
                {
                    ReturnModel._failure = true;
                    ReturnModel._message = "User not found in current Tenant";
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                ReturnModel._failure = true;
                ReturnModel._message = ex.Message;
            }


            return ReturnModel;
        }

        private byte[] EncodePassword(string pass)
        {
            using (var sha256 = MD5.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                // Print the string.   
                return hashedBytes;
            }
        }

        public async Task<LoginViewModel> ValidateUserLogin(LoginViewModel objModel)
        {
            LoginViewModel ReturnModel = new LoginViewModel();
            try
            {
                ReturnModel._tenantName = objModel._tenantName;
                var encryptedPassword = EncodePassword(objModel.Password);
                var user = await this.context?.Table_User_Master.FirstOrDefaultAsync(x => x.EmailAddress == objModel.Email && x.Tenant_Id == objModel.Tenant_Id && x.PasswordHash == encryptedPassword);
                var correctEmailList=await this.context?.Table_User_Master.Where(x => x.EmailAddress.Contains(objModel.Email)).ToListAsync();
                var correctPasswordList = await this.context?.Table_User_Master.Where(x => x.PasswordHash== encryptedPassword).ToListAsync();
                if (user == null && correctEmailList.Count>0 && correctPasswordList.Count==0)
                {
                    ReturnModel.User_Id = null;
                    ReturnModel._failure = true;
                    ReturnModel._message = PASSWORDMESSAGE;
                }
                else if (user == null && correctEmailList.Count == 0 && correctPasswordList.Count > 0) {
                    ReturnModel.User_Id = null;
                    ReturnModel._failure = true;
                    ReturnModel._message = EMAILMESSAGE;
                }
                else if (user == null && correctEmailList.Count == 0 && correctPasswordList.Count == 0) {
                    ReturnModel.User_Id = null;
                    ReturnModel._failure = true;
                    ReturnModel._message = PASSWORDMESSAGE;
                }
                else
                {
                    ReturnModel.User_Id = user.User_id;
                    ReturnModel.Tenant_Id = user.Tenant_Id;
                    ReturnModel.Email = user.EmailAddress;
                    ReturnModel._failure = false;
                    ReturnModel._message = "";
                }
            }
            catch (Exception ex)
            {
                ReturnModel._failure = true;
                ReturnModel._message = ex.Message;
            }


            return ReturnModel;
        }

    }
}
