using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.ParentInfos;
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
        public ParentInfoAddViewModel AddParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            try
            {
                if (parentInfoAddViewModel.parentInfo.ParentId.ToString()!= null)
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
                    parentInfoAddViewModel.parentInfo.PortalUserId = parentInfoAddViewModel.parentInfo.Email;
                    this.context?.ParentInfo.Add(parentInfoAddViewModel.parentInfo);
                }
                
                    this.context?.SaveChanges();

                if (!string.IsNullOrWhiteSpace(parentInfoAddViewModel.PasswordHash) && !string.IsNullOrWhiteSpace(parentInfoAddViewModel.parentInfo.Email))
                {
                    UserMaster userMaster = new UserMaster();

                    var decrypted = Utility.Decrypt(parentInfoAddViewModel.PasswordHash);
                    string passwordHash = Utility.GetHashedPassword(decrypted);

                    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.EmailAddress == parentInfoAddViewModel.parentInfo.Email);

                    if (loginInfo == null)
                    {
                        var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.Profile == "Parent");

                        userMaster.SchoolId = parentInfoAddViewModel.parentInfo.SchoolId;
                        userMaster.TenantId = parentInfoAddViewModel.parentInfo.TenantId;
                        userMaster.UserId = parentInfoAddViewModel.parentInfo.StudentId;
                        userMaster.LangId = 1;
                        userMaster.MembershipId = membership.MembershipId;
                        userMaster.EmailAddress = parentInfoAddViewModel.parentInfo.Email;
                        userMaster.PasswordHash = passwordHash;
                        userMaster.Name = parentInfoAddViewModel.parentInfo.Firstname;

                        this.context?.UserMaster.Add(userMaster);
                        this.context?.SaveChanges();
                    }
                }
                               
                
            }
            catch (Exception es)
            {

                parentInfoAddViewModel._message = es.Message;
                parentInfoAddViewModel._failure = true;
                parentInfoAddViewModel._tenantName = parentInfoAddViewModel._tenantName;
                parentInfoAddViewModel._token = parentInfoAddViewModel._token;
            }
            //this.context?.SaveChanges();
            parentInfoAddViewModel._failure = false;


            return parentInfoAddViewModel;
        }
        //public ParentInfoAddViewModel ViewParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        //{
        //    ParentInfoAddViewModel parentInfoUpdateModel = new ParentInfoAddViewModel();
        //    try
        //    {
        //        var parentInfoView = this.context?.ParentInfo.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.StudentId == parentInfoAddViewModel.parentInfo.StudentId);
        //        if (parentInfoView != null)
        //        {
        //            parentInfoAddViewModel.parentInfo = parentInfoView;
        //            parentInfoAddViewModel._failure = false;
        //        }
        //        else
        //        {
        //            parentInfoAddViewModel._failure = true;
        //            parentInfoAddViewModel._message = NORECORDFOUND;
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        parentInfoAddViewModel._failure = true;
        //        parentInfoAddViewModel._message = es.Message;
        //    }
        //    return parentInfoAddViewModel;
        //}
        public ParentInfoAddViewModel UpdateParentInfo(ParentInfoAddViewModel parentInfoAddViewModel)
        {
            ParentInfoAddViewModel parentInfoUpdateModel = new ParentInfoAddViewModel();
            try
            {
                var parentInfoUpdate = this.context?.ParentInfo.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.ParentId == parentInfoAddViewModel.parentInfo.ParentId);
                
                parentInfoUpdate.Relationship = parentInfoAddViewModel.parentInfo.Relationship;
                parentInfoUpdate.Firstname= parentInfoAddViewModel.parentInfo.Firstname;
                parentInfoUpdate.Lastname = parentInfoAddViewModel.parentInfo.Lastname;
                parentInfoUpdate.HomePhone = parentInfoAddViewModel.parentInfo.HomePhone;
                parentInfoUpdate.WorkPhone = parentInfoAddViewModel.parentInfo.WorkPhone;
                parentInfoUpdate.Mobile = parentInfoAddViewModel.parentInfo.Mobile;
                parentInfoUpdate.Email = parentInfoAddViewModel.parentInfo.Mobile;
                parentInfoUpdate.StudentAddressSame = parentInfoAddViewModel.parentInfo.StudentAddressSame;
                parentInfoUpdate.AddressLineOne = parentInfoAddViewModel.parentInfo.AddressLineOne;
                parentInfoUpdate.AddressLineTwo = parentInfoAddViewModel.parentInfo.AddressLineTwo;
                parentInfoUpdate.Country = parentInfoAddViewModel.parentInfo.Country;
                parentInfoUpdate.City = parentInfoAddViewModel.parentInfo.City;
                parentInfoUpdate.State = parentInfoAddViewModel.parentInfo.State;
                parentInfoUpdate.Zip = parentInfoAddViewModel.parentInfo.Zip;
                parentInfoUpdate.IsCustodian = parentInfoAddViewModel.parentInfo.IsCustodian;
                parentInfoUpdate.IsPortalUser = parentInfoAddViewModel.parentInfo.IsPortalUser;
                //parentInfoUpdate.PortalUserId = parentInfoAddViewModel.parentInfo.Email;
                parentInfoUpdate.BusPickup = parentInfoAddViewModel.parentInfo.BusPickup;
                parentInfoUpdate.BusDropoff = parentInfoAddViewModel.parentInfo.BusDropoff;
                parentInfoUpdate.ContactType = parentInfoAddViewModel.parentInfo.ContactType;
                //parentInfoUpdate.Associationship=parentInfoUpdate.Associationship+"#"+ parentInfoAddViewModel.parentInfo.TenantId + "#" + parentInfoAddViewModel.parentInfo.SchoolId + "#" + parentInfoAddViewModel.parentInfo.StudentId;
                parentInfoUpdate.LastUpdated = DateTime.UtcNow;
                parentInfoUpdate.UpdatedBy = parentInfoAddViewModel.parentInfo.UpdatedBy;
                parentInfoUpdate.BusNo = parentInfoAddViewModel.parentInfo.BusNo;
                this.context?.SaveChanges();

                if (!string.IsNullOrWhiteSpace(parentInfoAddViewModel.PasswordHash) && !string.IsNullOrWhiteSpace(parentInfoAddViewModel.parentInfo.Email))
                {
                    UserMaster userMaster = new UserMaster();

                    var decrypted = Utility.Decrypt(parentInfoAddViewModel.PasswordHash);
                    string passwordHash = Utility.GetHashedPassword(decrypted);

                    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.EmailAddress == parentInfoAddViewModel.parentInfo.Email);

                    if (loginInfo == null)
                    {
                        var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == parentInfoAddViewModel.parentInfo.TenantId && x.SchoolId == parentInfoAddViewModel.parentInfo.SchoolId && x.Profile == "Parent");

                        userMaster.SchoolId = parentInfoAddViewModel.parentInfo.SchoolId;
                        userMaster.TenantId = parentInfoAddViewModel.parentInfo.TenantId;
                        userMaster.UserId = parentInfoAddViewModel.parentInfo.StudentId;
                        userMaster.LangId = 1;
                        userMaster.MembershipId = membership.MembershipId;
                        userMaster.EmailAddress = parentInfoAddViewModel.parentInfo.Email;
                        userMaster.PasswordHash = passwordHash;
                        userMaster.Name = parentInfoAddViewModel.parentInfo.Firstname;

                        this.context?.UserMaster.Add(userMaster);
                        this.context?.SaveChanges();
                    }
                }
                parentInfoAddViewModel._failure = false;
                parentInfoAddViewModel._message = "Entity Updated";
            }
            catch (Exception es)
            {
                parentInfoAddViewModel._failure = true;
                parentInfoAddViewModel._message = es.Message;
            }
            return parentInfoAddViewModel;
        }
    }
}
