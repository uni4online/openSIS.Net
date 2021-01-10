using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public GradeRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        /// <summary>
        /// Add Grade Scale
        /// </summary>
        /// <param name="gradeScaleAddViewModel"></param>
        /// <returns></returns>
        public GradeScaleAddViewModel AddGradeScale(GradeScaleAddViewModel gradeScaleAddViewModel)
        {
            try
            {
                int? GradeScaleId = 1;

                var gradeScaleData = this.context?.GradeScale.Where(x => x.SchoolId == gradeScaleAddViewModel.gradeScale.SchoolId && x.TenantId == gradeScaleAddViewModel.gradeScale.TenantId).OrderByDescending(x => x.GradeScaleId).FirstOrDefault();

                if (gradeScaleData != null)
                {
                    GradeScaleId = gradeScaleData.GradeScaleId + 1;
                }
                gradeScaleAddViewModel.gradeScale.GradeScaleId = (int)GradeScaleId;
                gradeScaleAddViewModel.gradeScale.CreatedOn = DateTime.UtcNow;
                this.context?.GradeScale.Add(gradeScaleAddViewModel.gradeScale);
                this.context?.SaveChanges();
                gradeScaleAddViewModel._failure = false;
            }
            catch (Exception es)
            {

                gradeScaleAddViewModel._failure = true;
                gradeScaleAddViewModel._message = es.Message;
            }
            return gradeScaleAddViewModel;
        }
        /// <summary>
        /// Update Grade Scale
        /// </summary>
        /// <param name="gradeScaleAddViewModel"></param>
        /// <returns></returns>
        public GradeScaleAddViewModel UpdateGradeScale(GradeScaleAddViewModel gradeScaleAddViewModel)
        {            
            try
            {
                var gradeScaleUpdate = this.context?.GradeScale.FirstOrDefault(x => x.TenantId == gradeScaleAddViewModel.gradeScale.TenantId && x.SchoolId == gradeScaleAddViewModel.gradeScale.SchoolId && x.GradeScaleId == gradeScaleAddViewModel.gradeScale.GradeScaleId);
                if (gradeScaleUpdate != null)
                {
                    gradeScaleUpdate.GradeScaleName = gradeScaleAddViewModel.gradeScale.GradeScaleName;
                    gradeScaleUpdate.GradeScaleValue = gradeScaleAddViewModel.gradeScale.GradeScaleValue;
                    gradeScaleUpdate.GradeScaleComment = gradeScaleAddViewModel.gradeScale.GradeScaleComment;
                    gradeScaleUpdate.CalculateGpa = gradeScaleAddViewModel.gradeScale.CalculateGpa;
                    gradeScaleUpdate.UseAsStandardGradeScale = gradeScaleAddViewModel.gradeScale.UseAsStandardGradeScale;
                    gradeScaleUpdate.SortOrder = gradeScaleAddViewModel.gradeScale.SortOrder;
                    gradeScaleUpdate.UpdatedBy = gradeScaleAddViewModel.gradeScale.UpdatedBy;
                    gradeScaleUpdate.UpdatedOn = DateTime.UtcNow;
                    this.context?.SaveChanges();
                    gradeScaleAddViewModel._failure = false;
                    gradeScaleAddViewModel._message = "Grade Scale Updated Successfully";
                }
                else
                {
                    gradeScaleAddViewModel.gradeScale = null;
                    gradeScaleAddViewModel._failure = true;
                    gradeScaleAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {

                gradeScaleAddViewModel._failure = true;
                gradeScaleAddViewModel._message = es.Message;
            }
            return gradeScaleAddViewModel;
        }
        /// <summary>
        /// Delete Grade Scale
        /// </summary>
        /// <param name="gradeScaleAddViewModel"></param>
        /// <returns></returns>
        public GradeScaleAddViewModel DeleteGradeScale(GradeScaleAddViewModel gradeScaleAddViewModel)
        {
            try
            {
                var gradeScaleDelete = this.context?.GradeScale.FirstOrDefault(x => x.TenantId == gradeScaleAddViewModel.gradeScale.TenantId && x.SchoolId == gradeScaleAddViewModel.gradeScale.SchoolId && x.GradeScaleId == gradeScaleAddViewModel.gradeScale.GradeScaleId);
                
                var gradeList = this.context?.Grade.Where(e => e.GradeScaleId == gradeScaleDelete.GradeScaleId && e.SchoolId == gradeScaleDelete.SchoolId && e.TenantId == gradeScaleDelete.TenantId).ToList();
                
                if (gradeScaleDelete!=null)
                {
                    if (gradeList.Count > 0)
                    {
                        gradeScaleAddViewModel._message = "It Has Associationship";
                        gradeScaleAddViewModel._failure = true;
                    }
                    else
                    {
                        this.context?.GradeScale.Remove(gradeScaleDelete);
                        this.context?.SaveChanges();
                        gradeScaleAddViewModel._failure = false;
                        gradeScaleAddViewModel._message = "Deleted";
                    }
                }
                else
                {
                    gradeScaleAddViewModel._failure = true;
                    gradeScaleAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {

                gradeScaleAddViewModel._failure = true;
                gradeScaleAddViewModel._message = es.Message;
            }
            return gradeScaleAddViewModel;
        }
        /// <summary>
        /// Add Grade
        /// </summary>
        /// <param name="gradeAddViewModel"></param>
        /// <returns></returns>
        public GradeAddViewModel AddGrade(GradeAddViewModel gradeAddViewModel)
        {
            try
            {
                int? GradeId = 1;
                int? SortOrder = 1;

                var gradeData = this.context?.Grade.Where(x => x.SchoolId == gradeAddViewModel.grade.SchoolId && x.TenantId == gradeAddViewModel.grade.TenantId).OrderByDescending(x => x.GradeId).FirstOrDefault();

                if (gradeData != null)
                {
                    GradeId = gradeData.GradeId + 1;
                }
                var gradeSortOrder = this.context?.Grade.Where(x => x.SchoolId == gradeAddViewModel.grade.SchoolId && x.TenantId == gradeAddViewModel.grade.TenantId && x.GradeScaleId== gradeAddViewModel.grade.GradeScaleId).OrderByDescending(x => x.SortOrder).FirstOrDefault();
                
                if (gradeSortOrder != null)
                {
                    SortOrder = gradeSortOrder.SortOrder + 1;
                }
                gradeAddViewModel.grade.GradeId = (int)GradeId;
                gradeAddViewModel.grade.SortOrder = (int)SortOrder;
                gradeAddViewModel.grade.CreatedOn = DateTime.UtcNow;
                this.context?.Grade.Add(gradeAddViewModel.grade);
                this.context?.SaveChanges();
                gradeAddViewModel._failure = false;
            }
            catch (Exception es)
            {

                gradeAddViewModel._failure = true;
                gradeAddViewModel._message = es.Message;
            }
            return gradeAddViewModel;
        }
        /// <summary>
        /// Update Grade
        /// </summary>
        /// <param name="gradeAddViewModel"></param>
        /// <returns></returns>
        public GradeAddViewModel UpdateGrade(GradeAddViewModel gradeAddViewModel)
        {
            try
            {
                var gradeUpdate = this.context?.Grade.FirstOrDefault(x => x.TenantId == gradeAddViewModel.grade.TenantId && x.SchoolId == gradeAddViewModel.grade.SchoolId && x.GradeId == gradeAddViewModel.grade.GradeId);
                if (gradeUpdate != null)
                {
                    gradeUpdate.Tite = gradeAddViewModel.grade.Tite;
                    gradeUpdate.Breakoff = gradeAddViewModel.grade.Breakoff;
                    gradeUpdate.WeightedGpValue = gradeAddViewModel.grade.WeightedGpValue;
                    gradeUpdate.UnweightedGpValue = gradeAddViewModel.grade.UnweightedGpValue;
                    gradeUpdate.Comment = gradeAddViewModel.grade.Comment;
                    gradeUpdate.UpdatedBy = gradeAddViewModel.grade.UpdatedBy;
                    gradeUpdate.UpdatedOn = DateTime.UtcNow;
                    this.context?.SaveChanges();
                    gradeAddViewModel._failure = false;
                    gradeAddViewModel._message = "Grade Updated Successfully";
                }
                else
                {
                    gradeAddViewModel.grade = null;
                    gradeAddViewModel._failure = true;
                    gradeAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {

                gradeAddViewModel._failure = true;
                gradeAddViewModel._message = es.Message;
            }
            return gradeAddViewModel;
        }
        /// <summary>
        /// Delete Grade
        /// </summary>
        /// <param name="gradeAddViewModel"></param>
        /// <returns></returns>
        public GradeAddViewModel DeleteGrade(GradeAddViewModel gradeAddViewModel)
        {
            try
            {
                var gradeDelete = this.context?.Grade.FirstOrDefault(x => x.TenantId == gradeAddViewModel.grade.TenantId && x.SchoolId == gradeAddViewModel.grade.SchoolId && x.GradeId==gradeAddViewModel.grade.GradeId);
                
                if (gradeDelete != null)
                {
                        this.context?.Grade.Remove(gradeDelete);
                        this.context?.SaveChanges();
                        gradeAddViewModel._failure = false;
                        gradeAddViewModel._message = "Deleted";                    
                }
                else
                {
                        gradeAddViewModel._failure = true;
                        gradeAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                gradeAddViewModel._failure = true;
                gradeAddViewModel._message = es.Message;
            }
            return gradeAddViewModel;
        }
        /// <summary>
        /// Get All GradeScale List
        /// </summary>
        /// <param name="gradeScaleListViewModel"></param>
        /// <returns></returns>
        public GradeScaleListViewModel GetAllGradeScaleList(GradeScaleListViewModel gradeScaleListViewModel)
        {
            GradeScaleListViewModel GradeScaleListModel = new GradeScaleListViewModel();
            try
            {

                var GradeScaleList = this.context?.GradeScale.Include(x => x.Grade).Where(e => e.TenantId == gradeScaleListViewModel.TenantId && e.SchoolId == gradeScaleListViewModel.SchoolId).OrderBy(e=>e.SortOrder).ToList();                
                
                if (GradeScaleList.Count>0)
                {
                    foreach (var GradeScale in GradeScaleList)
                    {
                        GradeScale.Grade = GradeScale.Grade.OrderBy(y => y.SortOrder).ToList();
                    }

                    GradeScaleListModel.gradeScaleList = GradeScaleList;
                    GradeScaleListModel._tenantName = gradeScaleListViewModel._tenantName;
                    GradeScaleListModel._token = gradeScaleListViewModel._token;
                    GradeScaleListModel._failure = false;
                }
                else
                {
                    GradeScaleListModel._message = NORECORDFOUND;
                    GradeScaleListModel._failure = true;
                }
            }
            catch (Exception es)
            {
                GradeScaleListModel._message = es.Message;
                GradeScaleListModel._failure = true;
                GradeScaleListModel._tenantName = gradeScaleListViewModel._tenantName;
                GradeScaleListModel._token = gradeScaleListViewModel._token;
            }
            return GradeScaleListModel;
        }
        /// <summary>
        /// Update Grade Sort Order
        /// </summary>
        /// <param name="gradeSortOrderModel"></param>
        /// <returns></returns>
        public GradeSortOrderModel UpdateGradeSortOrder(GradeSortOrderModel gradeSortOrderModel)
        {
            try
            {
                var GradeRecords = new List<Grade>();

                var targetGrade = this.context?.Grade.FirstOrDefault(x => x.SortOrder == gradeSortOrderModel.PreviousSortOrder && x.SchoolId == gradeSortOrderModel.SchoolId && x.GradeScaleId == gradeSortOrderModel.GradeScaleId && x.TenantId== gradeSortOrderModel.TenantId);
                
                targetGrade.SortOrder = gradeSortOrderModel.CurrentSortOrder;

                if (gradeSortOrderModel.PreviousSortOrder > gradeSortOrderModel.CurrentSortOrder)
                {
                    GradeRecords = this.context?.Grade.Where(x => x.SortOrder >= gradeSortOrderModel.CurrentSortOrder && x.SortOrder < gradeSortOrderModel.PreviousSortOrder && x.SchoolId == gradeSortOrderModel.SchoolId && x.GradeScaleId == gradeSortOrderModel.GradeScaleId && x.TenantId == gradeSortOrderModel.TenantId).ToList();

                    if (GradeRecords.Count > 0)
                    {
                        GradeRecords.ForEach(x => x.SortOrder = x.SortOrder + 1);
                    }
                }
                if (gradeSortOrderModel.CurrentSortOrder > gradeSortOrderModel.PreviousSortOrder)
                {
                    GradeRecords = this.context?.Grade.Where(x => x.SortOrder <= gradeSortOrderModel.CurrentSortOrder && x.SortOrder > gradeSortOrderModel.PreviousSortOrder && x.SchoolId == gradeSortOrderModel.SchoolId && x.GradeScaleId == gradeSortOrderModel.GradeScaleId && x.TenantId == gradeSortOrderModel.TenantId).ToList();
                    if (GradeRecords.Count > 0)
                    {
                        GradeRecords.ForEach(x => x.SortOrder = x.SortOrder - 1);
                    }
                }
                this.context?.SaveChanges();
                gradeSortOrderModel._failure = false;
            }
            catch (Exception es)
            {
                gradeSortOrderModel._message = es.Message;
                gradeSortOrderModel._failure = true;
            }
            return gradeSortOrderModel;
        }
        /// <summary>
        /// Add EffortGrade Library Category
        /// </summary>
        /// <param name="effortGradeLibraryCategoryAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeLibraryCategoryAddViewModel AddEffortGradeLibraryCategory(EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryAddViewModel)
        {
            try
            {
                int? EffortCategoryId = 1;
                int? SortOrder = 1;

                var effortGradeLibraryCategoryData = this.context?.EffortGradeLibraryCategory.Where(x => x.SchoolId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.SchoolId && x.TenantId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.TenantId).OrderByDescending(x => x.EffortCategoryId).FirstOrDefault();

                if (effortGradeLibraryCategoryData != null)
                {
                    EffortCategoryId = effortGradeLibraryCategoryData.EffortCategoryId + 1;
                }
                var effortGradeLibraryCategorySortOrder = this.context?.EffortGradeLibraryCategory.Where(x => x.SchoolId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.SchoolId && x.TenantId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.TenantId).OrderByDescending(x => x.SortOrder).FirstOrDefault();

                if (effortGradeLibraryCategorySortOrder != null)
                {
                    SortOrder = effortGradeLibraryCategorySortOrder.SortOrder + 1;
                }
                effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.EffortCategoryId = (int)EffortCategoryId;
                effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.SortOrder = (int)SortOrder;
                effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.CreatedOn = DateTime.UtcNow;
                this.context?.EffortGradeLibraryCategory.Add(effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory);
                this.context?.SaveChanges();
                effortGradeLibraryCategoryAddViewModel._failure = false;
            }
            catch (Exception es)
            {

                effortGradeLibraryCategoryAddViewModel._failure = true;
                effortGradeLibraryCategoryAddViewModel._message = es.Message;
            }
            return effortGradeLibraryCategoryAddViewModel;
        }
        /// <summary>
        /// Update EffortGrade Library Category
        /// </summary>
        /// <param name="effortGradeLibraryCategoryAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeLibraryCategoryAddViewModel UpdateEffortGradeLibraryCategory(EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryAddViewModel)
        {
            try
            {
                var EffortGradeLibraryCategoryUpdate = this.context?.EffortGradeLibraryCategory.FirstOrDefault(x => x.TenantId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.TenantId && x.SchoolId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.SchoolId && x.EffortCategoryId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.EffortCategoryId);
                if (EffortGradeLibraryCategoryUpdate != null)
                {
                    EffortGradeLibraryCategoryUpdate.CategoryName = effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.CategoryName;
                    EffortGradeLibraryCategoryUpdate.UpdatedBy = effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.UpdatedBy;
                    EffortGradeLibraryCategoryUpdate.UpdatedOn = DateTime.UtcNow;
                    this.context?.SaveChanges();
                    effortGradeLibraryCategoryAddViewModel._failure = false;
                    effortGradeLibraryCategoryAddViewModel._message = "Effort Grade Library Category Updated Successfully";
                }
                else
                {
                    effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory = null;
                    effortGradeLibraryCategoryAddViewModel._failure = true;
                    effortGradeLibraryCategoryAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                effortGradeLibraryCategoryAddViewModel._failure = true;
                effortGradeLibraryCategoryAddViewModel._message = es.Message;
            }
            return effortGradeLibraryCategoryAddViewModel;
        }
        /// <summary>
        /// Delete EffortGrade Library Category
        /// </summary>
        /// <param name="effortGradeLibraryCategoryAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeLibraryCategoryAddViewModel DeleteEffortGradeLibraryCategory(EffortGradeLibraryCategoryAddViewModel effortGradeLibraryCategoryAddViewModel)
        {
            try
            {
                var EffortGradeLibraryCategoryDelete = this.context?.EffortGradeLibraryCategory.FirstOrDefault(x => x.TenantId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.TenantId && x.SchoolId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.SchoolId && x.EffortCategoryId == effortGradeLibraryCategoryAddViewModel.effortGradeLibraryCategory.EffortCategoryId);

                if (EffortGradeLibraryCategoryDelete != null)
                {
                    var effortGradeLibraryCategoryItemExits = this.context?.EffortGradeLibraryCategoryItem.FirstOrDefault(e => e.TenantId == EffortGradeLibraryCategoryDelete.TenantId && e.SchoolId == EffortGradeLibraryCategoryDelete.SchoolId && e.EffortCategoryId == EffortGradeLibraryCategoryDelete.EffortCategoryId);
                    if (effortGradeLibraryCategoryItemExits!=null)
                    {
                        effortGradeLibraryCategoryAddViewModel._failure = true;
                        effortGradeLibraryCategoryAddViewModel._message = "Cannot be deleted because it has association.";
                    }
                    else
                    {
                        this.context?.EffortGradeLibraryCategory.Remove(EffortGradeLibraryCategoryDelete);
                        this.context?.SaveChanges();
                        effortGradeLibraryCategoryAddViewModel._failure = false;
                        effortGradeLibraryCategoryAddViewModel._message = "Deleted";
                    }
                    
                }
                else
                {
                    effortGradeLibraryCategoryAddViewModel._failure = true;
                    effortGradeLibraryCategoryAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                effortGradeLibraryCategoryAddViewModel._failure = true;
                effortGradeLibraryCategoryAddViewModel._message = es.Message;
            }
            return effortGradeLibraryCategoryAddViewModel;
        }
        /// <summary>
        /// Add EffortGrade Library Category Item
        /// </summary>
        /// <param name="effortGradeLibraryCategoryItemAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeLibraryCategoryItemAddViewModel AddEffortGradeLibraryCategoryItem(EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemAddViewModel)
        {
            try
            {
                int? EffortCategoryItemId = 1;
                int? SortOrder = 1;

                var effortGradeLibraryCategoryItemData = this.context?.EffortGradeLibraryCategoryItem.Where(x => x.SchoolId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.SchoolId && x.TenantId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.TenantId).OrderByDescending(x => x.EffortItemId).FirstOrDefault();

                if (effortGradeLibraryCategoryItemData != null)
                {
                    EffortCategoryItemId = effortGradeLibraryCategoryItemData.EffortItemId + 1;
                }
                var effortGradeLibraryCategoryItemSortOrder = this.context?.EffortGradeLibraryCategoryItem.Where(x => x.SchoolId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.SchoolId && x.TenantId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.TenantId && x.EffortCategoryId== effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.EffortCategoryId).OrderByDescending(x => x.SortOrder).FirstOrDefault();

                if (effortGradeLibraryCategoryItemSortOrder != null)
                {
                    SortOrder = effortGradeLibraryCategoryItemSortOrder.SortOrder + 1;
                }
                effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.EffortItemId = (int)EffortCategoryItemId;
                effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.SortOrder = (int)SortOrder;
                effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.CreatedOn = DateTime.UtcNow;
                this.context?.EffortGradeLibraryCategoryItem.Add(effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem);
                this.context?.SaveChanges();
                effortGradeLibraryCategoryItemAddViewModel._failure = false;
            }
            catch (Exception es)
            {

                effortGradeLibraryCategoryItemAddViewModel._failure = true;
                effortGradeLibraryCategoryItemAddViewModel._message = es.Message;
            }
            return effortGradeLibraryCategoryItemAddViewModel;
        }
        /// <summary>
        /// Update EffortGrade Library Category Item
        /// </summary>
        /// <param name="effortGradeLibraryCategoryItemAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeLibraryCategoryItemAddViewModel UpdateEffortGradeLibraryCategoryItem(EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemAddViewModel)
        {
            try
            {
                var EffortGradeLibraryCategoryItemUpdate = this.context?.EffortGradeLibraryCategoryItem.FirstOrDefault(x => x.TenantId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.TenantId && x.SchoolId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.SchoolId && x.EffortItemId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.EffortItemId);
                if (EffortGradeLibraryCategoryItemUpdate != null)
                {
                    EffortGradeLibraryCategoryItemUpdate.EffortItemTitle = effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.EffortItemTitle;
                    EffortGradeLibraryCategoryItemUpdate.UpdatedBy = effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.UpdatedBy;
                    EffortGradeLibraryCategoryItemUpdate.UpdatedOn = DateTime.UtcNow;
                    this.context?.SaveChanges();
                    effortGradeLibraryCategoryItemAddViewModel._failure = false;
                    effortGradeLibraryCategoryItemAddViewModel._message = "Effort Grade Library Category Item Updated Successfully";
                }
                else
                {
                    effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem = null;
                    effortGradeLibraryCategoryItemAddViewModel._failure = true;
                    effortGradeLibraryCategoryItemAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {

                effortGradeLibraryCategoryItemAddViewModel._failure = true;
                effortGradeLibraryCategoryItemAddViewModel._message = es.Message;
            }
            return effortGradeLibraryCategoryItemAddViewModel;
        }
        /// <summary>
        /// Delete EffortGrade Library Category Item
        /// </summary>
        /// <param name="effortGradeLibraryCategoryItemAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeLibraryCategoryItemAddViewModel DeleteEffortGradeLibraryCategoryItem(EffortGradeLibraryCategoryItemAddViewModel effortGradeLibraryCategoryItemAddViewModel)
        {
            try
            {
                var EffortGradeLibraryCategoryItemDelete = this.context?.EffortGradeLibraryCategoryItem.FirstOrDefault(x => x.TenantId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.TenantId && x.SchoolId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.SchoolId && x.EffortItemId == effortGradeLibraryCategoryItemAddViewModel.effortGradeLibraryCategoryItem.EffortItemId);
                
                if (EffortGradeLibraryCategoryItemDelete != null)
                {
                    this.context?.EffortGradeLibraryCategoryItem.Remove(EffortGradeLibraryCategoryItemDelete);
                    this.context?.SaveChanges();
                    effortGradeLibraryCategoryItemAddViewModel._failure = false;
                    effortGradeLibraryCategoryItemAddViewModel._message = "Deleted";
                }
                else
                {
                    effortGradeLibraryCategoryItemAddViewModel._failure = true;
                    effortGradeLibraryCategoryItemAddViewModel._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {

                effortGradeLibraryCategoryItemAddViewModel._failure = true;
                effortGradeLibraryCategoryItemAddViewModel._message = es.Message;
            }
            return effortGradeLibraryCategoryItemAddViewModel;
        }
        /// <summary>
        /// Get All EffortGrade Llibrary Category List
        /// </summary>
        /// <param name="effortGradeLlibraryCategoryListViewModel"></param>
        /// <returns></returns>
        public EffortGradeLlibraryCategoryListViewModel GetAllEffortGradeLlibraryCategoryList(EffortGradeLlibraryCategoryListViewModel effortGradeLlibraryCategoryListViewModel)
        {
            EffortGradeLlibraryCategoryListViewModel effortGradeLlibraryCategoryListModel = new EffortGradeLlibraryCategoryListViewModel();
            try
            {

                var effortGradeLlibraryCategoryList = this.context?.EffortGradeLibraryCategory.Include(x => x.EffortGradeLibraryCategoryItem).Where(e => e.TenantId == effortGradeLlibraryCategoryListViewModel.TenantId && e.SchoolId == effortGradeLlibraryCategoryListViewModel.SchoolId).OrderBy(e => e.SortOrder)
                    .Select(p => new EffortGradeLibraryCategory()
                    {
                        SchoolId = p.SchoolId,
                        TenantId = p.TenantId,
                        EffortCategoryId = p.EffortCategoryId,
                        CategoryName=p.CategoryName,
                        CreatedBy=p.CreatedBy,
                        CreatedOn=p.CreatedOn,
                        SortOrder = p.SortOrder,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedOn=p.UpdatedOn,
                        EffortGradeLibraryCategoryItem = p.EffortGradeLibraryCategoryItem.OrderBy(c => c.SortOrder).ToList() 
                    }).ToList();

                    effortGradeLlibraryCategoryListModel.effortGradeLibraryCategoryList = effortGradeLlibraryCategoryList;
                    effortGradeLlibraryCategoryListModel._tenantName = effortGradeLlibraryCategoryListViewModel._tenantName;
                    effortGradeLlibraryCategoryListModel._token = effortGradeLlibraryCategoryListViewModel._token;
                    effortGradeLlibraryCategoryListModel._failure = false;                
            }
            catch (Exception es)
            {
                effortGradeLlibraryCategoryListModel._message = es.Message;
                effortGradeLlibraryCategoryListModel._failure = true;
                effortGradeLlibraryCategoryListModel._tenantName = effortGradeLlibraryCategoryListViewModel._tenantName;
                effortGradeLlibraryCategoryListModel._token = effortGradeLlibraryCategoryListViewModel._token;
            }
            return effortGradeLlibraryCategoryListModel;
        }
        /// <summary>
        /// Update EffortGrade Llibrary Category SortOrder
        /// </summary>
        /// <param name="effortgradeLibraryCategorySortOrderModel"></param>
        /// <returns></returns>
        public EffortgradeLibraryCategorySortOrderModel UpdateEffortGradeLlibraryCategorySortOrder(EffortgradeLibraryCategorySortOrderModel effortgradeLibraryCategorySortOrderModel)
        {
            try
            {
                if( effortgradeLibraryCategorySortOrderModel.EffortCategoryId>0)
                {
                    var EffortGradeLibraryCategoryItemRecords = new List<EffortGradeLibraryCategoryItem>();

                    var targetEffortGradeLibraryCategoryItem = this.context?.EffortGradeLibraryCategoryItem.FirstOrDefault(x => x.SortOrder == effortgradeLibraryCategorySortOrderModel.PreviousSortOrder && x.SchoolId == effortgradeLibraryCategorySortOrderModel.SchoolId && x.TenantId == effortgradeLibraryCategorySortOrderModel.TenantId && x.EffortCategoryId == effortgradeLibraryCategorySortOrderModel.EffortCategoryId);

                    if (targetEffortGradeLibraryCategoryItem != null)
                    {
                        targetEffortGradeLibraryCategoryItem.SortOrder = effortgradeLibraryCategorySortOrderModel.CurrentSortOrder;

                        if (effortgradeLibraryCategorySortOrderModel.PreviousSortOrder > effortgradeLibraryCategorySortOrderModel.CurrentSortOrder)
                        {
                            EffortGradeLibraryCategoryItemRecords = this.context?.EffortGradeLibraryCategoryItem.Where(x => x.SortOrder >= effortgradeLibraryCategorySortOrderModel.CurrentSortOrder && x.SortOrder < effortgradeLibraryCategorySortOrderModel.PreviousSortOrder && x.SchoolId == effortgradeLibraryCategorySortOrderModel.SchoolId && x.TenantId == effortgradeLibraryCategorySortOrderModel.TenantId && x.EffortCategoryId == effortgradeLibraryCategorySortOrderModel.EffortCategoryId).ToList();

                            if (EffortGradeLibraryCategoryItemRecords.Count > 0)
                            {
                                EffortGradeLibraryCategoryItemRecords.ForEach(x => x.SortOrder = x.SortOrder + 1);
                            }
                        }
                        if (effortgradeLibraryCategorySortOrderModel.CurrentSortOrder > effortgradeLibraryCategorySortOrderModel.PreviousSortOrder)
                        {
                            EffortGradeLibraryCategoryItemRecords = this.context?.EffortGradeLibraryCategoryItem.Where(x => x.SortOrder <= effortgradeLibraryCategorySortOrderModel.CurrentSortOrder && x.SortOrder > effortgradeLibraryCategorySortOrderModel.PreviousSortOrder && x.SchoolId == effortgradeLibraryCategorySortOrderModel.SchoolId && x.TenantId == effortgradeLibraryCategorySortOrderModel.TenantId && x.EffortCategoryId == effortgradeLibraryCategorySortOrderModel.EffortCategoryId).ToList();
                            if (EffortGradeLibraryCategoryItemRecords.Count > 0)
                            {
                                EffortGradeLibraryCategoryItemRecords.ForEach(x => x.SortOrder = x.SortOrder - 1);
                            }
                        }
                    }

                }                
                else
                {
                    var EffortGradeLibraryCategoryRecords = new List<EffortGradeLibraryCategory>();

                    var targetEffortGradeLibraryCategory = this.context?.EffortGradeLibraryCategory.FirstOrDefault(x => x.SortOrder == effortgradeLibraryCategorySortOrderModel.PreviousSortOrder && x.SchoolId == effortgradeLibraryCategorySortOrderModel.SchoolId && x.TenantId == effortgradeLibraryCategorySortOrderModel.TenantId);
                    if (targetEffortGradeLibraryCategory != null)
                    {
                        targetEffortGradeLibraryCategory.SortOrder = effortgradeLibraryCategorySortOrderModel.CurrentSortOrder;

                        if (effortgradeLibraryCategorySortOrderModel.PreviousSortOrder > effortgradeLibraryCategorySortOrderModel.CurrentSortOrder)
                        {
                            EffortGradeLibraryCategoryRecords = this.context?.EffortGradeLibraryCategory.Where(x => x.SortOrder >= effortgradeLibraryCategorySortOrderModel.CurrentSortOrder && x.SortOrder < effortgradeLibraryCategorySortOrderModel.PreviousSortOrder && x.SchoolId == effortgradeLibraryCategorySortOrderModel.SchoolId && x.TenantId == effortgradeLibraryCategorySortOrderModel.TenantId).ToList();

                            if (EffortGradeLibraryCategoryRecords.Count > 0)
                            {
                                EffortGradeLibraryCategoryRecords.ForEach(x => x.SortOrder = x.SortOrder + 1);
                            }
                        }
                        if (effortgradeLibraryCategorySortOrderModel.CurrentSortOrder > effortgradeLibraryCategorySortOrderModel.PreviousSortOrder)
                        {
                            EffortGradeLibraryCategoryRecords = this.context?.EffortGradeLibraryCategory.Where(x => x.SortOrder <= effortgradeLibraryCategorySortOrderModel.CurrentSortOrder && x.SortOrder > effortgradeLibraryCategorySortOrderModel.PreviousSortOrder && x.SchoolId == effortgradeLibraryCategorySortOrderModel.SchoolId && x.TenantId == effortgradeLibraryCategorySortOrderModel.TenantId).ToList();
                            if (EffortGradeLibraryCategoryRecords.Count > 0)
                            {
                                EffortGradeLibraryCategoryRecords.ForEach(x => x.SortOrder = x.SortOrder - 1);
                            }
                        }
                    }

                }
                this.context?.SaveChanges();
                effortgradeLibraryCategorySortOrderModel._failure = false;
            }
            catch (Exception es)
            {
                effortgradeLibraryCategorySortOrderModel._message = es.Message;
                effortgradeLibraryCategorySortOrderModel._failure = true;
            }
            return effortgradeLibraryCategorySortOrderModel;
        }

        /// <summary>
        /// Add EffortGradeScale
        /// </summary>
        /// <param name="effortGradeScaleAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeScaleAddViewModel AddEffortGradeScale(EffortGradeScaleAddViewModel effortGradeScaleAddViewModel)
        {
            EffortGradeScaleAddViewModel effortGradeScaleAdd = new EffortGradeScaleAddViewModel();
            try
            {
                int? effortGradeScaleId = 1;
                int? SortOrder = 1;

                var effortGradeScaleData = this.context?.EffortGradeScale.Where(x => x.TenantId == effortGradeScaleAddViewModel.effortGradeScale.TenantId && x.SchoolId == effortGradeScaleAddViewModel.effortGradeScale.SchoolId).OrderByDescending(x => x.EffortGradeScaleId).FirstOrDefault();

                if (effortGradeScaleData != null)
                {
                    effortGradeScaleId = effortGradeScaleData.EffortGradeScaleId + 1;
                }
                var sortOrderData = this.context?.EffortGradeScale.Where(x => x.TenantId == effortGradeScaleAddViewModel.effortGradeScale.TenantId && x.SchoolId == effortGradeScaleAddViewModel.effortGradeScale.SchoolId).OrderByDescending(x => x.SortOrder).FirstOrDefault();

                if (sortOrderData != null)
                {
                    SortOrder = sortOrderData.SortOrder + 1;
                }

                effortGradeScaleAddViewModel.effortGradeScale.EffortGradeScaleId = (int)effortGradeScaleId;
                effortGradeScaleAddViewModel.effortGradeScale.SortOrder = (int)SortOrder;
                effortGradeScaleAddViewModel.effortGradeScale.CreatedOn = DateTime.UtcNow;
                this.context?.EffortGradeScale.Add(effortGradeScaleAddViewModel.effortGradeScale);
                this.context?.SaveChanges();
                effortGradeScaleAddViewModel._failure = false;
            }
            catch(Exception es)
            {
                effortGradeScaleAddViewModel._failure = false;
                effortGradeScaleAddViewModel._message = es.Message;
            }
            return effortGradeScaleAddViewModel;
        }

        /// <summary>
        /// Update EffortGradeScale
        /// </summary>
        /// <param name="effortGradeScaleAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeScaleAddViewModel UpdateEffortGradeScale(EffortGradeScaleAddViewModel effortGradeScaleAddViewModel)
        {
            try
            {
                var effortGradeScaleUpdate = this.context?.EffortGradeScale.FirstOrDefault(x => x.TenantId == effortGradeScaleAddViewModel.effortGradeScale.TenantId && x.SchoolId == effortGradeScaleAddViewModel.effortGradeScale.SchoolId && x.EffortGradeScaleId == effortGradeScaleAddViewModel.effortGradeScale.EffortGradeScaleId);
                if (effortGradeScaleUpdate != null)
                {
                    effortGradeScaleUpdate.GradeScaleValue = effortGradeScaleAddViewModel.effortGradeScale.GradeScaleValue;
                    effortGradeScaleUpdate.GradeScaleComment = effortGradeScaleAddViewModel.effortGradeScale.GradeScaleComment;
                    effortGradeScaleUpdate.UpdatedBy = effortGradeScaleAddViewModel.effortGradeScale.UpdatedBy;
                    effortGradeScaleUpdate.UpdatedOn = DateTime.UtcNow;
                    this.context?.SaveChanges();
                    effortGradeScaleAddViewModel._failure = false;
                    effortGradeScaleAddViewModel._message = "EffortGradeScale Updated Successfully";
                }
                else
                {
                    effortGradeScaleAddViewModel.effortGradeScale = null;
                    effortGradeScaleAddViewModel._message = NORECORDFOUND;
                    effortGradeScaleAddViewModel._failure = true;
                }
            }
            catch (Exception es)
            {
                effortGradeScaleAddViewModel._failure = true;
                effortGradeScaleAddViewModel._message = es.Message;
            }
            return effortGradeScaleAddViewModel;
        }

        /// <summary>
        /// Delete EffortGradeScale
        /// </summary>
        /// <param name="effortGradeScaleAddViewModel"></param>
        /// <returns></returns>
        public EffortGradeScaleAddViewModel DeleteEffortGradeScale(EffortGradeScaleAddViewModel effortGradeScaleAddViewModel)
        {
            try
            {
                var effortGradeScaleDelete = this.context?.EffortGradeScale.FirstOrDefault(x => x.TenantId == effortGradeScaleAddViewModel.effortGradeScale.TenantId && x.SchoolId == effortGradeScaleAddViewModel.effortGradeScale.SchoolId && x.EffortGradeScaleId == effortGradeScaleAddViewModel.effortGradeScale.EffortGradeScaleId);

                if (effortGradeScaleDelete != null)
                {
                    this.context?.EffortGradeScale.Remove(effortGradeScaleDelete);
                    this.context?.SaveChanges();
                    effortGradeScaleAddViewModel._failure = false;
                    effortGradeScaleAddViewModel._message = "Deleted Successfully";
                }
                else
                {
                    effortGradeScaleAddViewModel._message = NORECORDFOUND;
                    effortGradeScaleAddViewModel._failure = true;
                }
            }
            catch (Exception es)
            {
                effortGradeScaleAddViewModel._failure = true;
                effortGradeScaleAddViewModel._message = es.Message;
            }
            return effortGradeScaleAddViewModel;
        }

        /// <summary>
        /// Get All EffortGradeScale
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public EffortGradeScaleListModel GetAllEffortGradeScale(PageResult pageResult)
        {
            EffortGradeScaleListModel effortGradeScaleList = new EffortGradeScaleListModel();
            IQueryable<EffortGradeScale> transactionIQ = null;

            var effortGradeScaleData = this.context?.EffortGradeScale.Where(x => x.TenantId == pageResult.TenantId && x.SchoolId==pageResult.SchoolId);
            try
            {
                if (pageResult.FilterParams == null || pageResult.FilterParams.Count == 0)
                {
                    transactionIQ = effortGradeScaleData;
                }
                else
                {
                    if (pageResult.FilterParams != null && pageResult.FilterParams.ElementAt(0).ColumnName == null && pageResult.FilterParams.Count == 1)
                    {
                        string Columnvalue = pageResult.FilterParams.ElementAt(0).FilterValue;
                        transactionIQ = effortGradeScaleData.Where(x => x.GradeScaleValue != null && x.GradeScaleValue.ToString() == Columnvalue.ToString() || x.GradeScaleComment != null && x.GradeScaleComment.ToLower().Contains(Columnvalue.ToLower()));
                    }
                    else
                    {
                        transactionIQ = Utility.FilteredData(pageResult.FilterParams, effortGradeScaleData).AsQueryable();
                    }
                }
                if (pageResult.SortingModel != null)
                {
                    transactionIQ = Utility.Sort(transactionIQ, pageResult.SortingModel.SortColumn, pageResult.SortingModel.SortDirection.ToLower());
                }
                int totalCount = transactionIQ.Count();
                if (pageResult.PageNumber > 0 && pageResult.PageSize > 0)
                {
                    transactionIQ = transactionIQ.Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize);
                }
                var effortList = transactionIQ.AsNoTracking().Select(s => new GetEffortGradeScaleForView
                {
                    TenantId = s.TenantId,
                    SchoolId = s.SchoolId,
                    EffortGradeScaleId = s.EffortGradeScaleId,
                    GradeScaleValue = s.GradeScaleValue,
                    GradeScaleComment = s.GradeScaleComment,
                    SortOrder = s.SortOrder
                }).ToList();

                effortGradeScaleList.TenantId = pageResult.TenantId;
                effortGradeScaleList.SchoolId = pageResult.SchoolId;
                effortGradeScaleList.getEffortGradeScaleForView = effortList;
                effortGradeScaleList.TotalCount = totalCount;
                effortGradeScaleList.PageNumber = pageResult.PageNumber;
                effortGradeScaleList._pageSize = pageResult.PageSize;
                effortGradeScaleList._tenantName = pageResult._tenantName;
                effortGradeScaleList._token = pageResult._token;
                effortGradeScaleList._failure = false;
            }
            catch (Exception es)
            {
                effortGradeScaleList._message = es.Message;
                effortGradeScaleList._failure = true;
                effortGradeScaleList._tenantName = pageResult._tenantName;
                effortGradeScaleList._token = pageResult._token;
            }
            return effortGradeScaleList;
        }

        /// <summary>
        /// Update EffortGradeScale SortOrder
        /// </summary>
        /// <param name="effortGradeScaleSortOrderViewModel"></param>
        /// <returns></returns>
        public EffortGradeScaleSortOrderViewModel UpdateEffortGradeScaleSortOrder(EffortGradeScaleSortOrderViewModel effortGradeScaleSortOrderViewModel)
        {
            try
            {
                var effortGradeScaleRecords = new List<EffortGradeScale>();

                var targetEffortGradeScale = this.context?.EffortGradeScale.FirstOrDefault(x => x.SortOrder == effortGradeScaleSortOrderViewModel.PreviousSortOrder && x.SchoolId == effortGradeScaleSortOrderViewModel.SchoolId && x.TenantId == effortGradeScaleSortOrderViewModel.TenantId);
                targetEffortGradeScale.SortOrder = effortGradeScaleSortOrderViewModel.CurrentSortOrder;

                if (effortGradeScaleSortOrderViewModel.PreviousSortOrder > effortGradeScaleSortOrderViewModel.CurrentSortOrder)
                {
                    effortGradeScaleRecords = this.context?.EffortGradeScale.Where(x => x.SortOrder >= effortGradeScaleSortOrderViewModel.CurrentSortOrder && x.SortOrder < effortGradeScaleSortOrderViewModel.PreviousSortOrder && x.TenantId == effortGradeScaleSortOrderViewModel.TenantId && x.SchoolId == effortGradeScaleSortOrderViewModel.SchoolId).ToList();

                    if (effortGradeScaleRecords.Count > 0)
                    {
                        effortGradeScaleRecords.ForEach(x => x.SortOrder = x.SortOrder + 1);
                    }
                }
                if (effortGradeScaleSortOrderViewModel.CurrentSortOrder > effortGradeScaleSortOrderViewModel.PreviousSortOrder)
                {
                    effortGradeScaleRecords = this.context?.EffortGradeScale.Where(x => x.SortOrder <= effortGradeScaleSortOrderViewModel.CurrentSortOrder && x.SortOrder > effortGradeScaleSortOrderViewModel.PreviousSortOrder && x.SchoolId == effortGradeScaleSortOrderViewModel.SchoolId && x.TenantId == effortGradeScaleSortOrderViewModel.TenantId).ToList();
                    if (effortGradeScaleRecords.Count > 0)
                    {
                        effortGradeScaleRecords.ForEach(x => x.SortOrder = x.SortOrder - 1);
                    }
                }
                this.context?.SaveChanges();
                effortGradeScaleSortOrderViewModel._failure = false;
            }
            catch (Exception es)
            {
                effortGradeScaleSortOrderViewModel._message = es.Message;
                effortGradeScaleSortOrderViewModel._failure = true;
            }
            return effortGradeScaleSortOrderViewModel;
        }
    }
}
