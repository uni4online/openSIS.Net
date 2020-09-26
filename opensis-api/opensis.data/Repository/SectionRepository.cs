using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class SectionRepository : ISectionRepositiory
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public SectionRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        /// <summary>
        /// For Adding Section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public SectionAddViewModel AddSection(SectionAddViewModel section)
        {
            int? MasterSectionId = Utility.GetMaxPK(this.context, new Func<TableSections, int>(x => x.SectionId));
            section.tableSections.SectionId = (int)MasterSectionId;
            section.tableSections.LastUpdated = DateTime.UtcNow;
            this.context?.TableSections.Add(section.tableSections);
            this.context?.SaveChanges();
            section._failure = false;
            return section;
        }

       /// <summary>
       /// For Updating Section
       /// </summary>
       /// <param name="section"></param>
       /// <returns></returns>

        public SectionAddViewModel UpdateSection(SectionAddViewModel section)
        {
            try
            {
                var sectionUpdate = this.context?.TableSections.FirstOrDefault(x => x.TenantId == section.tableSections.TenantId && x.SchoolId == section.tableSections.SchoolId && x.SectionId == section.tableSections.SectionId);

                sectionUpdate.TenantId = section.tableSections.TenantId;
                sectionUpdate.SchoolId = section.tableSections.SchoolId;
                sectionUpdate.SectionId = section.tableSections.SectionId;
                sectionUpdate.Name = section.tableSections.Name;
                sectionUpdate.SortOrder = section.tableSections.SortOrder;
                section.tableSections.LastUpdated = DateTime.UtcNow;
                sectionUpdate.UpdatedBy = section.tableSections.UpdatedBy;

                this.context?.SaveChanges();

                section._failure = false;
                return section;
            }
            catch (Exception ex)
            {
                section.tableSections = null;
                section._failure = true;
                section._message = NORECORDFOUND;
                return section;
            }

        }

        /// <summary>
        /// For Section View By ID
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>

        public SectionAddViewModel ViewSection(SectionAddViewModel section)        
        {
            try
            {
                SectionAddViewModel sectionView = new SectionAddViewModel();
                var sectionById = this.context?.TableSections.FirstOrDefault(x => x.TenantId == section.tableSections.TenantId && x.SchoolId == section.tableSections.SchoolId && x.SectionId== section.tableSections.SectionId);
                if (sectionById != null)
                {
                    sectionView.tableSections = sectionById;
                    return sectionView;
                }
                else
                {
                    sectionView._failure = true;
                    sectionView._message = NORECORDFOUND;
                    return sectionView;
                }
            }
            catch (Exception es)
            {

                throw;
            }
        }

        /// <summary>
        /// For GetAllSection
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>

        public SectionListViewModel GetAllsection(SectionListViewModel section)
        {
            SectionListViewModel sectionList = new SectionListViewModel();
            try
            {

                var sectionAll = this.context?.TableSections.Where(x => x.TenantId == section.TenantId && x.SchoolId == section.SchoolId).OrderBy(x => x.SortOrder).ToList();
                sectionList.tableSectionsList = sectionAll;
                sectionList._tenantName = section._tenantName;
                sectionList._token = section._token;
                sectionList._failure = false;
            }
            catch (Exception es)
            {
                sectionList._message = es.Message;
                sectionList._failure = true;
                sectionList._tenantName = section._tenantName;
                sectionList._token = section._token;
            }
            return sectionList;

        }
        /// <summary>
        /// For Deleting Section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>

        public SectionAddViewModel DeleteSection(SectionAddViewModel section)
        {
            try
            {
                var sectionDel = this.context?.TableSections.FirstOrDefault(x => x.TenantId == section.tableSections.TenantId && x.SchoolId == section.tableSections.SchoolId && x.SectionId == section.tableSections.SectionId);
                this.context?.TableSections.Remove(sectionDel);
                this.context?.SaveChanges();
                section._failure = false;
                section._message = "Deleted";
            }
            catch (Exception es)
            {
                section._failure = true;
                section._message = es.Message;
            }
            return section;
        }

    }
}
