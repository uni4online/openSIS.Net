using opensis.core.Gradelevel.Interfaces;
using opensis.core.helper;
using opensis.data.Interface;
using opensis.data.ViewModels.Gradelevel;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Gradelevel.Services
{
    public class GradelevelService : IGradelevelService
    {
        private static string SUCCESS = "success";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string TOKENINVALID = "Token not Valid";

        public IGradelevelRepository gradelevelRepository;
        public GradelevelService(IGradelevelRepository gradelevelRepository)
        {
            this.gradelevelRepository = gradelevelRepository;
        }
        /// <summary>
        /// Add Grade Level
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel AddGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelViewModel = new GradelevelViewModel();
            try
            {
                if (TokenManager.CheckToken(gradelevel._tenantName, gradelevel._token))
                {
                    gradelevelViewModel = this.gradelevelRepository.AddGradelevel(gradelevel);
                }
                else
                {
                    gradelevelViewModel._failure = true;
                    gradelevelViewModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                gradelevelViewModel._failure = true;
                gradelevelViewModel._message = es.Message;
            }
            return gradelevelViewModel;

        }
        /// <summary>
        /// View Grade Level by id
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel ViewGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelViewModel = new GradelevelViewModel();
            try
            {
                if (TokenManager.CheckToken(gradelevel._tenantName, gradelevel._token))
                {
                    gradelevelViewModel = this.gradelevelRepository.ViewGradelevel(gradelevel);
                }
                else
                {
                    gradelevelViewModel._failure = true;
                    gradelevelViewModel._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                gradelevelViewModel._failure = true;
                gradelevelViewModel._message = es.Message;
            }
            return gradelevelViewModel;
        }
        /// <summary>
        /// Update Grade Level
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel UpdateGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelUpdate = new GradelevelViewModel();
            try
            {
                if (TokenManager.CheckToken(gradelevel._tenantName, gradelevel._token))
                {
                    gradelevelUpdate = this.gradelevelRepository.UpdateGradelevel(gradelevel);
                }
                else
                {
                    gradelevelUpdate._failure = true;
                    gradelevelUpdate._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                gradelevelUpdate._failure = true;
                gradelevelUpdate._message = es.Message;
            }

            return gradelevelUpdate;
        }
        /// <summary>
        /// Delete a Grade Level
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel DeleteGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelDelete = new GradelevelViewModel();
            try
            {
                if (TokenManager.CheckToken(gradelevel._tenantName, gradelevel._token))
                {
                    gradelevelDelete = this.gradelevelRepository.DeleteGradelevel(gradelevel);
                }
                else
                {
                    gradelevelDelete._failure = true;
                    gradelevelDelete._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                gradelevelDelete._failure = true;
                gradelevelDelete._message = es.Message;
            }

            return gradelevelDelete;
        }

        public GradelevelListViewModel GetAllGradeLevels(GradelevelListViewModel gradelevel)
        {
            GradelevelListViewModel gradelevelList = new GradelevelListViewModel();
            try
            {
                if (TokenManager.CheckToken(gradelevel._tenantName, gradelevel._token))
                {
                    gradelevelList = this.gradelevelRepository.GetAllGradeLevels(gradelevel);
                }
                else
                {
                    gradelevelList._failure = true;
                    gradelevelList._message = TOKENINVALID;
                }
            }
            catch (Exception es)
            {
                gradelevelList._failure = true;
                gradelevelList._message = es.Message;
            }

            return gradelevelList;
        }
    }
}
