using opensis.core.helper;
using opensis.core.ParentInfo.Interfaces;
using opensis.data.Interface;
using opensis.data.ViewModels.ParentInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.ParentInfo.Services
{
    public class ParentInfoRegister : IParentInfoRegisterService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public IParentInfoRepository parentInfoRepository;
        public ParentInfoRegister(IParentInfoRepository parentInfoRepository)
        {
            this.parentInfoRepository = parentInfoRepository;
        }
        public ParentInfoRegister() { }
        public ParentInfoAddViewModel SaveParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel ParentInfoAddModel = new ParentInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(parentInfoAddViewModel._tenantName, parentInfoAddViewModel._token))
                {

                    ParentInfoAddModel = this.parentInfoRepository.AddParentInfo(parentInfoAddViewModel);

                }
                else
                {
                    ParentInfoAddModel._failure = true;
                    ParentInfoAddModel._message = TOKENINVALID;

                }
            }
            catch (Exception es)
            {

                ParentInfoAddModel._failure = true;
                ParentInfoAddModel._message = es.Message;
            }
            return ParentInfoAddModel;

        }
        //public ParentInfoAddViewModel ViewParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        //{
        //    ParentInfoAddViewModel parentInfoViewModel = new ParentInfoAddViewModel();
        //    try
        //    {
        //        if (TokenManager.CheckToken(parentInfoAddViewModel._tenantName, parentInfoAddViewModel._token))
        //        {
        //            parentInfoViewModel = this.parentInfoRepository.ViewParentInfo(parentInfoAddViewModel);
        //        }
        //        else
        //        {
        //            parentInfoViewModel._failure = true;
        //            parentInfoViewModel._message = TOKENINVALID;
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        parentInfoViewModel._failure = true;
        //        parentInfoViewModel._message = es.Message;
        //    }
        //    return parentInfoViewModel;
        //}
        public ParentInfoAddViewModel UpdateParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel parentInfoUpdateModel = new ParentInfoAddViewModel();
            try
            {
                if (TokenManager.CheckToken(parentInfoAddViewModel._tenantName, parentInfoAddViewModel._token))
                {
                    parentInfoUpdateModel = this.parentInfoRepository.UpdateParentInfo(parentInfoAddViewModel);
                }
                else
                {
                    parentInfoUpdateModel._failure = true;
                    parentInfoUpdateModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                parentInfoUpdateModel._failure = true;
                parentInfoUpdateModel._message = es.Message;
            }

            return parentInfoUpdateModel;
        }
    }
}
