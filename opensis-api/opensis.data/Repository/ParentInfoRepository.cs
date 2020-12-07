using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.ParentInfos;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class ParentInfoRepository : IParentInfoRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public ParentInfoRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add Parent For Student
        /// </summary>
        /// <param name="parentInfoAddViewModel"></param>
        /// <returns></returns>
        public ParentInfoAddViewModel AddParentForStudent(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    if (parentInfoAddViewModel.parentInfo.ParentId > 0)
                    {
                        var parentInfo = this.context?.ParentInfo.FirstOrDefault(x => x.ParentId == parentInfoAddViewModel.parentInfo.ParentId);

                        if (parentInfo != null)
                        {
                            if (parentInfo.Associationship != null)
                            {
                                parentInfo.Associationship = parentInfo.Associationship + " | " + parentInfoAddViewModel.parentInfo.TenantId + "#" + parentInfoAddViewModel.parentInfo.SchoolId + "#" + parentInfoAddViewModel.parentInfo.StudentId;
                            }
                            else
                            {
                                parentInfo.Associationship = parentInfoAddViewModel.parentInfo.TenantId + "#" + parentInfoAddViewModel.parentInfo.SchoolId + "#" + parentInfoAddViewModel.parentInfo.StudentId;
                            }
                            parentInfoAddViewModel.parentInfo.LastUpdated = DateTime.UtcNow;
                            parentInfoAddViewModel.parentInfo.StudentId = parentInfo.StudentId;

                        }

                    }
                    else
                    {
                        int? ParentId = Utility.GetMaxPK(this.context, new Func<ParentInfo, int>(x => x.ParentId));
                        parentInfoAddViewModel.parentInfo.ParentId = (int)ParentId;
                        parentInfoAddViewModel.parentInfo.Associationship = parentInfoAddViewModel.parentInfo.TenantId + "#" + parentInfoAddViewModel.parentInfo.SchoolId + "#" + parentInfoAddViewModel.parentInfo.StudentId;
                        parentInfoAddViewModel.parentInfo.LastUpdated = DateTime.UtcNow;
                        //Add LoginEmail in ParentInfo Table
                        if (!string.IsNullOrWhiteSpace(parentInfoAddViewModel.PasswordHash) && !string.IsNullOrWhiteSpace(parentInfoAddViewModel.parentInfo.LoginEmail))
                        {
                            parentInfoAddViewModel.parentInfo.LoginEmail = parentInfoAddViewModel.parentInfo.LoginEmail;                  
                        }
                        else
                        {
                            parentInfoAddViewModel.parentInfo.LoginEmail = null;
                        }
                        this.context?.ParentInfo.Add(parentInfoAddViewModel.parentInfo);
                    }

                    this.context?.SaveChanges();

                    if (!string.IsNullOrWhiteSpace(parentInfoAddViewModel.PasswordHash) && !string.IsNullOrWhiteSpace(parentInfoAddViewModel.parentInfo.LoginEmail))
                    {
                        UserMaster userMaster = new UserMaster();

                        var decrypted = Utility.Decrypt(parentInfoAddViewModel.PasswordHash);
                        string passwordHash = Utility.GetHashedPassword(decrypted);

                        var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.EmailAddress == parentInfoAddViewModel.parentInfo.LoginEmail);

                        if (loginInfo == null)
                        {
                            var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.Profile == "Parent");

                            userMaster.SchoolId = parentInfoAddViewModel.parentInfo.SchoolId;
                            userMaster.TenantId = parentInfoAddViewModel.parentInfo.TenantId;
                            userMaster.UserId = parentInfoAddViewModel.parentInfo.StudentId;
                            userMaster.LangId = 1;
                            userMaster.MembershipId = membership.MembershipId;
                            userMaster.EmailAddress = parentInfoAddViewModel.parentInfo.LoginEmail;
                            userMaster.PasswordHash = passwordHash;
                            userMaster.Name = parentInfoAddViewModel.parentInfo.Firstname;

                            this.context?.UserMaster.Add(userMaster);
                            this.context?.SaveChanges();
                        }
                    }
                    transaction.Commit();
                    parentInfoAddViewModel._failure = false;
                }
                catch (Exception es)
                {
                    transaction.Rollback();
                    parentInfoAddViewModel._message = es.Message;
                    parentInfoAddViewModel._failure = true;
                    parentInfoAddViewModel._tenantName = parentInfoAddViewModel._tenantName;
                    parentInfoAddViewModel._token = parentInfoAddViewModel._token;
                }
            }
            return parentInfoAddViewModel;
        }
        /// <summary>
        /// View Parent List For Student
        /// </summary>
        /// <param name="parentInfoList"></param>
        /// <returns></returns>
        public ParentInfoListModel ViewParentListForStudent(ParentInfoListModel parentInfoList)
        {
            ParentInfoListModel parentInfoListViewModel = new ParentInfoListModel();
            try
            {
                var AssociationshipData = parentInfoList.TenantId + "#" + parentInfoList.SchoolId + "#" + parentInfoList.StudentId;
                var parentList = this.context?.ParentInfo.Where(x => x.Associationship.Contains(AssociationshipData)).ToList();
                if (parentList.Count > 0)
                {
                    parentInfoListViewModel.parentInfoList = parentList;
                    parentInfoListViewModel._tenantName = parentInfoList._tenantName;
                    parentInfoListViewModel._token = parentInfoList._token;
                    parentInfoListViewModel._failure = false;
                }
                else
                {
                    parentInfoListViewModel._failure = true;
                    parentInfoListViewModel._message = NORECORDFOUND;
                    return parentInfoListViewModel;
                }
            }
            catch (Exception es)
            {
                parentInfoListViewModel._failure = true;
                parentInfoListViewModel._message = es.Message;
            }
            return parentInfoListViewModel;
        }
        /// <summary>
        /// Update Parent Info
        /// </summary>
        /// <param name="parentInfoAddViewModel"></param>
        /// <returns></returns>
        public ParentInfoAddViewModel UpdateParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel parentInfoUpdateModel = new ParentInfoAddViewModel();
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var parentInfoUpdate = this.context?.ParentInfo.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.ParentId == parentInfoAddViewModel.parentInfo.ParentId);

                    parentInfoUpdate.Relationship = parentInfoAddViewModel.parentInfo.Relationship;
                    parentInfoUpdate.Salutation = parentInfoAddViewModel.parentInfo.Salutation;
                    parentInfoUpdate.Firstname = parentInfoAddViewModel.parentInfo.Firstname;
                    parentInfoUpdate.Middlename = parentInfoAddViewModel.parentInfo.Middlename;
                    parentInfoUpdate.Lastname = parentInfoAddViewModel.parentInfo.Lastname;
                    parentInfoUpdate.HomePhone = parentInfoAddViewModel.parentInfo.HomePhone;
                    parentInfoUpdate.WorkPhone = parentInfoAddViewModel.parentInfo.WorkPhone;
                    parentInfoUpdate.Mobile = parentInfoAddViewModel.parentInfo.Mobile;
                    parentInfoUpdate.PersonalEmail = parentInfoAddViewModel.parentInfo.PersonalEmail;
                    parentInfoUpdate.WorkEmail = parentInfoAddViewModel.parentInfo.WorkEmail;
                    parentInfoUpdate.StudentAddressSame = parentInfoAddViewModel.parentInfo.StudentAddressSame;
                    parentInfoUpdate.AddressLineOne = parentInfoAddViewModel.parentInfo.AddressLineOne;
                    parentInfoUpdate.AddressLineTwo = parentInfoAddViewModel.parentInfo.AddressLineTwo;
                    parentInfoUpdate.Country = parentInfoAddViewModel.parentInfo.Country;
                    parentInfoUpdate.City = parentInfoAddViewModel.parentInfo.City;
                    parentInfoUpdate.State = parentInfoAddViewModel.parentInfo.State;
                    parentInfoUpdate.Zip = parentInfoAddViewModel.parentInfo.Zip;
                    parentInfoUpdate.IsCustodian = parentInfoAddViewModel.parentInfo.IsCustodian;
                    parentInfoUpdate.IsPortalUser = parentInfoAddViewModel.parentInfo.IsPortalUser;
                    parentInfoUpdate.Suffix = parentInfoAddViewModel.parentInfo.Suffix;
                    parentInfoUpdate.LoginEmail = parentInfoAddViewModel.parentInfo.LoginEmail;
                    parentInfoUpdate.BusPickup = parentInfoAddViewModel.parentInfo.BusPickup;
                    parentInfoUpdate.BusDropoff = parentInfoAddViewModel.parentInfo.BusDropoff;
                    parentInfoUpdate.ContactType = parentInfoAddViewModel.parentInfo.ContactType;
                    parentInfoUpdate.Associationship = parentInfoAddViewModel.parentInfo.Associationship;
                    parentInfoUpdate.LastUpdated = DateTime.UtcNow;
                    parentInfoUpdate.UpdatedBy = parentInfoAddViewModel.parentInfo.UpdatedBy;
                    parentInfoUpdate.BusNo = parentInfoAddViewModel.parentInfo.BusNo;
                    //Add LoginEmail in ParentInfo Table
                    if (!string.IsNullOrWhiteSpace(parentInfoAddViewModel.PasswordHash) && !string.IsNullOrWhiteSpace(parentInfoAddViewModel.parentInfo.LoginEmail))
                    {
                        parentInfoUpdate.LoginEmail = parentInfoAddViewModel.parentInfo.LoginEmail;
                    }
                    parentInfoUpdate.IsPortalUser = parentInfoAddViewModel.parentInfo.IsPortalUser;
                    this.context?.SaveChanges();

                    if (!string.IsNullOrWhiteSpace(parentInfoAddViewModel.PasswordHash) && !string.IsNullOrWhiteSpace(parentInfoAddViewModel.parentInfo.LoginEmail))
                    {
                        UserMaster userMaster = new UserMaster();

                        var decrypted = Utility.Decrypt(parentInfoAddViewModel.PasswordHash);
                        string passwordHash = Utility.GetHashedPassword(decrypted);

                        var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.EmailAddress == parentInfoAddViewModel.parentInfo.LoginEmail);

                        if (loginInfo == null)
                        {
                            var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.Profile == "Parent");

                            userMaster.SchoolId = parentInfoAddViewModel.parentInfo.SchoolId;
                            userMaster.TenantId = parentInfoAddViewModel.parentInfo.TenantId;
                            userMaster.UserId = parentInfoAddViewModel.parentInfo.StudentId;
                            userMaster.LangId = 1;
                            userMaster.MembershipId = membership.MembershipId;
                            userMaster.EmailAddress = parentInfoAddViewModel.parentInfo.LoginEmail;
                            userMaster.PasswordHash = null;
                            userMaster.Name = parentInfoAddViewModel.parentInfo.Firstname;

                            this.context?.UserMaster.Add(userMaster);
                            this.context?.SaveChanges();                            
                        }
                    }
                    transaction.Commit();
                    parentInfoAddViewModel._failure = false;
                    parentInfoAddViewModel._message = "Entity Updated";
                }
                catch (Exception es)
                {
                    transaction.Rollback();
                    parentInfoAddViewModel._failure = true;
                    parentInfoAddViewModel._message = es.Message;
                }
            }
            return parentInfoAddViewModel;
        }
        private static string ToFullAddress(string Address1, string Address2, string City, string State, string Country, string Zip)
        {
            string address = "";
            if (!string.IsNullOrWhiteSpace(Address1))
            {


                return address == null
                      ? null
                      : $"{Address1?.Trim()}{(!string.IsNullOrWhiteSpace(Address2) ? $", {Address2?.Trim()}" : string.Empty)}, {City?.Trim()}, {State?.Trim()} {Zip?.Trim()}";
            }
            return address;
        }
        /// <summary>
        /// Get All Parent Info
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public GetAllParentInfoListForView GetAllParentInfoList(PageResult pageResult)
        {
            GetAllParentInfoListForView parentInfoListModel = new GetAllParentInfoListForView();
            int resultData;
            IQueryable<ParentInfo> transactionIQ = null;
            var ParentInfoList = this.context?.ParentInfo.ToList().Where(x => x.TenantId == pageResult.TenantId && x.SchoolId==pageResult.SchoolId);
            
            try
            {
                if (pageResult.FilterParams == null || pageResult.FilterParams.Count == 0)
                {
                    transactionIQ = ParentInfoList.AsQueryable();
                }
                else
                {
                    if (pageResult.FilterParams != null && pageResult.FilterParams.ElementAt(0).ColumnName == null && pageResult.FilterParams.Count == 1)
                    {
                        string Columnvalue = pageResult.FilterParams.ElementAt(0).FilterValue;
                        transactionIQ = ParentInfoList.Where(x => x.Firstname != null && x.Firstname.ToLower().Contains(Columnvalue.ToLower()) ||
                                        x.Lastname != null && x.Lastname.ToLower().Contains(Columnvalue.ToLower()) ||
                                        x.PersonalEmail != null && x.PersonalEmail.ToLower().Contains(Columnvalue.ToLower()) ||
                                        x.Mobile != null && x.Mobile.Contains(Columnvalue)).AsQueryable();
                    }
                    else
                    {
                        transactionIQ = Utility.FilteredData(pageResult.FilterParams, ParentInfoList).AsQueryable();
                    }
                    transactionIQ = transactionIQ.Distinct();
                }
                int totalCount = transactionIQ.Count();
                transactionIQ = transactionIQ.Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize);
                var parentInfo = transactionIQ.AsNoTracking().Select(y => new GetParentInfoForView
                {
                    SchoolId = y.SchoolId,
                    TenantId = y.TenantId,
                    Firstname = y.Firstname,
                    Lastname = y.Lastname,
                    PersonalEmail=y.PersonalEmail,
                    Mobile=y.Mobile,
                    UserProfile=y.UserProfile,
                    Associationship=y.Associationship,
                    AddressLineOne = ToFullAddress(y.AddressLineOne, y.AddressLineTwo,
                        int.TryParse(y.City, out resultData) == true ? this.context.City.Where(x => x.Id == Convert.ToInt32(y.City)).FirstOrDefault().Name : y.City,
                        int.TryParse(y.State, out resultData) == true ? this.context.State.Where(x => x.Id == Convert.ToInt32(y.State)).FirstOrDefault().Name : y.State,
                        int.TryParse(y.Country, out resultData) == true ? this.context.Country.Where(x => x.Id == Convert.ToInt32(y.Country)).FirstOrDefault().Name : string.Empty, y.Zip)
                }).ToList();                
                parentInfoListModel.parentInfoForView = parentInfo;                
                foreach (var ParentInfo in parentInfoListModel.parentInfoForView)
                { 
                    var AssociationshipData = ParentInfo.Associationship;
                    string[] studentAssociateWithParents = AssociationshipData.Split(" | ", StringSplitOptions.RemoveEmptyEntries);

                    foreach (var studentAssociateWithParent in studentAssociateWithParents)
                    {                        
                        char studentId = studentAssociateWithParent.Last();
                        var student = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == int.Parse(studentId.ToString()));
                        var studentForView = new GetStudentForView() {StudentId=student.StudentId,StudentInternalId=student.StudentInternalId,SchoolId=student.SchoolId,TenantId=student.TenantId ,FirstGivenName = student.FirstGivenName, MiddleName = student.MiddleName, LastFamilyName = student.LastFamilyName };

                        ParentInfo.getStudentForView.Add(studentForView);
                    }
                }
                parentInfoListModel.TenantId = pageResult.TenantId;
                parentInfoListModel.TotalCount = totalCount;
                parentInfoListModel.PageNumber = pageResult.PageNumber;
                parentInfoListModel._pageSize = pageResult.PageSize;
                parentInfoListModel._tenantName = pageResult._tenantName;
                parentInfoListModel._token = pageResult._token;
                parentInfoListModel._failure = false;
            }
            catch (Exception es)
            {
                parentInfoListModel._message = es.Message;
                parentInfoListModel._failure = true;
                parentInfoListModel._tenantName = pageResult._tenantName;
                parentInfoListModel._token = pageResult._token;
            }
            return parentInfoListModel;
        }
        /// <summary>
        /// Delete Parent Info
        /// </summary>
        /// <param name="parentInfoAddViewModel"></param>
        /// <returns></returns>
        public ParentInfoAddViewModel DeleteParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            try
            {
                var ParentInfo = this.context?.ParentInfo.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.ParentId == parentInfoAddViewModel.parentInfo.ParentId);
                this.context?.ParentInfo.Remove(ParentInfo);
                this.context?.SaveChanges();
                parentInfoAddViewModel._failure = false;
                parentInfoAddViewModel._message = "Deleted";
            }
            catch (Exception es)
            {
                parentInfoAddViewModel._failure = true;
                parentInfoAddViewModel._message = es.Message;
            }
            return parentInfoAddViewModel;
        }
        /// <summary>
        /// Search ParentInfo For Student
        /// </summary>
        /// <param name="getAllParentInfoListForView"></param>
        /// <returns></returns>
        public GetAllParentInfoListForView SearchParentInfoForStudent(GetAllParentInfoListForView getAllParentInfoListForView)
        {
            GetAllParentInfoListForView parentInfoListModel = new GetAllParentInfoListForView();
            try
            {
                int resultData;
                var ParentInfoList = this.context?.ParentInfo.ToList().Where(x => x.TenantId == getAllParentInfoListForView.TenantId && (getAllParentInfoListForView.Firstname == null || (x.Firstname == getAllParentInfoListForView.Firstname)) && (getAllParentInfoListForView.Lastname == null || (x.Lastname == getAllParentInfoListForView.Lastname)) && (getAllParentInfoListForView.Email == null || (x.PersonalEmail == getAllParentInfoListForView.Email)) && (getAllParentInfoListForView.Mobile == null || (x.Mobile == getAllParentInfoListForView.Mobile)) && (getAllParentInfoListForView.StreetAddress == null || (x.AddressLineOne == getAllParentInfoListForView.StreetAddress)) && (getAllParentInfoListForView.City == null || (x.City == getAllParentInfoListForView.City)) && (getAllParentInfoListForView.State == null || (x.State == getAllParentInfoListForView.State)) && (getAllParentInfoListForView.Zip == null || (x.Zip == getAllParentInfoListForView.Zip)));                
                var parentInfo = ParentInfoList.Select(y => new GetParentInfoForView
                {
                    SchoolId = y.SchoolId,
                    Firstname = y.Firstname,
                    Lastname = y.Lastname,
                    Mobile=y.Mobile,
                    WorkPhone=y.WorkPhone,
                    HomePhone=y.HomePhone,
                    PersonalEmail=y.PersonalEmail,
                    WorkEmail=y.WorkEmail,
                    LoginEmail=y.LoginEmail,
                    UserProfile=y.UserProfile,
                    IsPortalUser=y.IsPortalUser,
                    IsCustodian=y.IsCustodian,
                    TenantId = y.TenantId,
                    AddressLineOne = ToFullAddress(y.AddressLineOne, y.AddressLineTwo,
                int.TryParse(y.City, out resultData) == true ? this.context.City.Where(x => x.Id == Convert.ToInt32(y.City)).FirstOrDefault().Name : y.City,
                int.TryParse(y.State, out resultData) == true ? this.context.State.Where(x => x.Id == Convert.ToInt32(y.State)).FirstOrDefault().Name : y.State,
                int.TryParse(y.Country, out resultData) == true ? this.context.Country.Where(x => x.Id == Convert.ToInt32(y.Country)).FirstOrDefault().Name : string.Empty, y.Zip),
                }).ToList();
                parentInfoListModel.TenantId = getAllParentInfoListForView.TenantId;
                parentInfoListModel.parentInfoForView = parentInfo;              
                parentInfoListModel._tenantName = getAllParentInfoListForView._tenantName;
                parentInfoListModel._token = getAllParentInfoListForView._token;
                parentInfoListModel._failure = false;
            }
            catch (Exception es)
            {
                parentInfoListModel._message = es.Message;
                parentInfoListModel._failure = true;
                parentInfoListModel._tenantName = getAllParentInfoListForView._tenantName;
                parentInfoListModel._token = getAllParentInfoListForView._token;
            }
            return parentInfoListModel;
        }
        /// <summary>
        /// View Parent Info By Id 
        /// </summary>
        /// <param name="parentInfoAddViewModel"></param>
        /// <returns></returns>
        public ParentInfoAddViewModel ViewParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel parentInfoViewModel = new ParentInfoAddViewModel();
            try
            {                
                var parentInfo = this.context?.ParentInfo.FirstOrDefault( x =>x.TenantId== parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId== parentInfoAddViewModel.parentInfo.SchoolId && x.ParentId== parentInfoAddViewModel.parentInfo.ParentId);
                if (parentInfo!= null)
                {
                    parentInfoViewModel.parentInfo = parentInfo;
                    var AssociationshipData = parentInfo.Associationship;
                    string[] studentAssociateWithParents = AssociationshipData.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var studentAssociateWithParent in studentAssociateWithParents)
                    {
                        char studentId = studentAssociateWithParent.Last();
                        var student = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == int.Parse(studentId.ToString()));
                        var studentForView = new GetStudentForView() { StudentId = student.StudentId, StudentInternalId=student.StudentInternalId, FirstGivenName = student.FirstGivenName, MiddleName = student.MiddleName, LastFamilyName = student.LastFamilyName };

                        parentInfoViewModel.getStudentForView.Add(studentForView);
                    }
                    parentInfoViewModel._tenantName = parentInfoAddViewModel._tenantName;
                    parentInfoViewModel._token = parentInfoAddViewModel._token;
                    parentInfoViewModel._failure = false;
                }
                else
                {
                    parentInfoViewModel._failure = true;
                    parentInfoViewModel._message = NORECORDFOUND;
                    return parentInfoViewModel;
                }
            }
            catch (Exception es)
            {
                parentInfoViewModel._failure = true;
                parentInfoViewModel._message = es.Message;
            }
            return parentInfoViewModel;
        }
        /// <summary>
        /// Add Parent Info
        /// </summary>
        /// <param name="parentInfoAddViewModel"></param>
        /// <returns></returns>
        public ParentInfoAddViewModel AddParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            try
            {
                int? ParentId = Utility.GetMaxPK(this.context, new Func<ParentInfo, int>(x => x.ParentId));
                parentInfoAddViewModel.parentInfo.ParentId = (int)ParentId;
                parentInfoAddViewModel.parentInfo.LastUpdated = DateTime.UtcNow;
                parentInfoAddViewModel.parentInfo.LoginEmail = parentInfoAddViewModel.parentInfo.LoginEmail;
                this.context?.ParentInfo.Add(parentInfoAddViewModel.parentInfo);
                this.context?.SaveChanges();
                parentInfoAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                parentInfoAddViewModel._failure = true;
                parentInfoAddViewModel._message = es.Message;
            }
            return parentInfoAddViewModel;
        }

        /// <summary>
        /// Remove Associated Parent
        /// </summary>
        /// <param name="parentInfoDeleteViewModel"></param>
        /// <returns></returns>
        public ParentInfoDeleteViewModel RemoveAssociatedParent(ParentInfoDeleteViewModel parentInfoDeleteViewModel)
        {
            try
            {
                string AssociateParentAfterDel;
                var AssociationshipDataToDel = parentInfoDeleteViewModel.parentInfo.TenantId + "#" + parentInfoDeleteViewModel.parentInfo.SchoolId + "#" + parentInfoDeleteViewModel.StudentId;

                var ParentInfo = this.context?.ParentInfo.FirstOrDefault(x => x.TenantId == parentInfoDeleteViewModel.parentInfo.TenantId && x.SchoolId == parentInfoDeleteViewModel.parentInfo.SchoolId && x.ParentId == parentInfoDeleteViewModel.parentInfo.ParentId);

                if (ParentInfo != null)
                {
                    var AssociationshipData = ParentInfo.Associationship;
                    string[] studentAssociateWithParents = AssociationshipData.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
                    studentAssociateWithParents = studentAssociateWithParents.Where(w => w != AssociationshipDataToDel).ToArray();
                    if (studentAssociateWithParents.Count() > 1)
                    {
                        AssociateParentAfterDel = string.Join(" | ", studentAssociateWithParents);
                    }
                    else if (studentAssociateWithParents.Count() == 1)
                    {
                        AssociateParentAfterDel = string.Concat(studentAssociateWithParents);
                    }
                    else
                    {
                        AssociateParentAfterDel = null;
                    }
                    ParentInfo.Associationship = AssociateParentAfterDel;
                    this.context?.SaveChanges();
                    parentInfoDeleteViewModel._message = "Associationship Remove Successfully";
                }
            }
            catch (Exception es)
            {
                parentInfoDeleteViewModel._failure = true;
                parentInfoDeleteViewModel._message = es.Message;
            }
            return parentInfoDeleteViewModel;
        }
    }
}
