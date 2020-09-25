﻿using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Gradelevel;
using opensis.data.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class GradelevelRepository : IGradelevelRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public GradelevelRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }


        /// <summary>
        /// Grade Level Add
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel AddGradelevel(GradelevelViewModel gradelevel)
        {
            try
            {
                int? MasterSchoolId = Utility.GetMaxPK(this.context, new Func<TableGradelevels, int>(x => x.GradeId));
                gradelevel.tblGradelevel.GradeId = (int)MasterSchoolId;
                this.context?.TableGradelevels.Add(gradelevel.tblGradelevel);
                this.context?.SaveChanges();
                gradelevel._failure = false;
            }
            catch (Exception es)
            {
                gradelevel._failure = true;
                gradelevel._message = es.Message;
            }
            return gradelevel;

        }
        /// <summary>
        /// Get Grade Level by id
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel ViewGradelevel(GradelevelViewModel gradelevel)
        {
            GradelevelViewModel gradelevelModel = new GradelevelViewModel();
            try
            {
                var Gradelevel = this.context?.TableGradelevels.FirstOrDefault(x => x.TenantId == gradelevel.tblGradelevel.TenantId && x.SchoolId == gradelevel.tblGradelevel.SchoolId && x.GradeId == gradelevel.tblGradelevel.GradeId);
                if (Gradelevel != null)
                {
                    gradelevelModel.tblGradelevel = Gradelevel;
                    gradelevelModel._failure = false;
                }
                else
                {
                    gradelevelModel._failure = true;
                    gradelevelModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                gradelevelModel._failure = true;
                gradelevelModel._message = es.Message;
            }
            return gradelevelModel;

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
                var GradeLevel = this.context?.TableGradelevels.FirstOrDefault(x => x.TenantId == gradelevel.tblGradelevel.TenantId && x.SchoolId == gradelevel.tblGradelevel.SchoolId && x.GradeId == gradelevel.tblGradelevel.GradeId);
                GradeLevel.Title = gradelevel.tblGradelevel.Title;
                GradeLevel.LastUpdated = DateTime.UtcNow;
                GradeLevel.NextGradeId = gradelevel.tblGradelevel.NextGradeId;
                GradeLevel.ShortName = gradelevel.tblGradelevel.ShortName;
                GradeLevel.SortOrder = gradelevel.tblGradelevel.SortOrder;
                GradeLevel.UpdatedBy = gradelevel.tblGradelevel.UpdatedBy;
                this.context?.SaveChanges();
                gradelevel._failure = false;
                gradelevel._message = "Entity Updated";
            }
            catch(Exception es)
            {
                gradelevel._failure = true;
                gradelevel._message = es.Message;
            }
            return gradelevel;
        }
        /// <summary>
        /// Delete grade Level
        /// </summary>
        /// <param name="gradelevel"></param>
        /// <returns></returns>
        public GradelevelViewModel DeleteGradelevel(GradelevelViewModel gradelevel)
        {
            try
            {
                var GradeLevel = this.context?.TableGradelevels.FirstOrDefault(x => x.TenantId == gradelevel.tblGradelevel.TenantId && x.SchoolId == gradelevel.tblGradelevel.SchoolId && x.GradeId == gradelevel.tblGradelevel.GradeId);
                this.context?.TableGradelevels.Remove(GradeLevel);
                this.context?.SaveChanges();
                gradelevel._failure = false;
                gradelevel._message = "Deleted";
            }
            catch (Exception es)
            {
                gradelevel._failure = true;
                gradelevel._message = es.Message;
            }
            return gradelevel;
        }

        public GradelevelListViewModel GetAllGradeLevels(GradelevelListViewModel gradelevelList)
        {
            GradelevelListViewModel gradelevelListModel = new GradelevelListViewModel();
            try
            {

                var gradelevels = this.context?.TableGradelevels.Where(x => x.TenantId == gradelevelList.TenantId && x.SchoolId==gradelevelList.SchoolId).OrderBy(x=>x.SortOrder).ToList();
                gradelevelListModel.TableGradelevelList = gradelevels;
                gradelevelListModel._tenantName = gradelevelList._tenantName;
                gradelevelListModel._token = gradelevelList._token;
                gradelevelListModel._failure = false;
            }
            catch (Exception es)
            {
                gradelevelListModel._message = es.Message;
                gradelevelListModel._failure = true;
                gradelevelListModel._tenantName = gradelevelList._tenantName;
                gradelevelListModel._token = gradelevelList._token;
            }
            return gradelevelListModel;

        }
    }
}
