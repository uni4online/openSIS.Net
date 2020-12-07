using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public StudentRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel AddStudent(StudentAddViewModel student)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    int? MasterStudentId = Utility.GetMaxPK(this.context, new Func<StudentMaster, int>(x => x.StudentId));
                    student.studentMaster.StudentId = (int)MasterStudentId;
                    bool checkInternalID = CheckInternalID(student.studentMaster.TenantId,student.studentMaster.StudentInternalId);
                    if (checkInternalID == true)
                    {
                        int? MasterEnrollmentId = Utility.GetMaxPK(this.context, new Func<StudentEnrollment, int>(x => (int)x.EnrollmentId));
                        var schoolName = this.context?.SchoolMaster.Where(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId).Select(s => s.SchoolName).FirstOrDefault();

                        var StudentEnrollmentData = new StudentEnrollment() { TenantId = student.studentMaster.TenantId, SchoolId = student.studentMaster.SchoolId, StudentId = student.studentMaster.StudentId, EnrollmentId = (int)MasterEnrollmentId, SchoolName = schoolName, RollingOption = "New", EnrollmentDate = DateTime.UtcNow };

                        student.studentMaster.StudentEnrollment.Add(StudentEnrollmentData);

                        //Update StudentPortalId in Studentmaster table.
                        if (!string.IsNullOrWhiteSpace(student.PasswordHash) && !string.IsNullOrWhiteSpace(student.LoginEmail))
                        {
                            student.studentMaster.StudentPortalId = student.LoginEmail;
                        }

                        this.context?.StudentMaster.Add(student.studentMaster);
                        this.context?.SaveChanges();

                        if (student.fieldsCategoryList != null && student.fieldsCategoryList.ToList().Count > 0)
                        {
                            var fieldsCategory = student.fieldsCategoryList.FirstOrDefault(x => x.CategoryId == student.SelectedCategoryId);
                            if (fieldsCategory != null)
                            {
                                foreach (var customFields in fieldsCategory.CustomFields.ToList())
                                {
                                    if (customFields.CustomFieldsValue != null && customFields.CustomFieldsValue.ToList().Count > 0)
                                    {
                                        customFields.CustomFieldsValue.FirstOrDefault().Module = "Student";
                                        customFields.CustomFieldsValue.FirstOrDefault().CategoryId = customFields.CategoryId;
                                        customFields.CustomFieldsValue.FirstOrDefault().FieldId = customFields.FieldId;
                                        customFields.CustomFieldsValue.FirstOrDefault().CustomFieldTitle = customFields.Title;
                                        customFields.CustomFieldsValue.FirstOrDefault().CustomFieldType = customFields.Type;
                                        customFields.CustomFieldsValue.FirstOrDefault().SchoolId = student.studentMaster.SchoolId;
                                        customFields.CustomFieldsValue.FirstOrDefault().TargetId = student.studentMaster.StudentId;
                                        this.context?.CustomFieldsValue.AddRange(customFields.CustomFieldsValue);
                                        this.context?.SaveChanges();
                                    }
                                }

                            }
                        }

                        if (!string.IsNullOrWhiteSpace(student.PasswordHash) && !string.IsNullOrWhiteSpace(student.LoginEmail))
                        {
                            UserMaster userMaster = new UserMaster();

                            var decrypted = Utility.Decrypt(student.PasswordHash);
                            string passwordHash = Utility.GetHashedPassword(decrypted);

                            var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.EmailAddress == student.LoginEmail);

                            if (loginInfo == null)
                            {
                                var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.Profile == "Student");

                                userMaster.SchoolId = student.studentMaster.SchoolId;
                                userMaster.TenantId = student.studentMaster.TenantId;
                                userMaster.UserId = student.studentMaster.StudentId;
                                userMaster.LangId = 1;
                                userMaster.MembershipId = membership.MembershipId;
                                userMaster.EmailAddress = student.LoginEmail;
                                userMaster.PasswordHash = passwordHash;
                                userMaster.Name = student.studentMaster.FirstGivenName;
                                userMaster.LastUpdated = DateTime.UtcNow;
                                userMaster.IsActive = true;

                                this.context?.UserMaster.Add(userMaster);
                                this.context?.SaveChanges();
                            }
                        }
                        student._failure = false;
                    }
                    else
                    {
                        student.studentMaster = null;
                        student._failure = true;
                        student._message = "Student InternalID Already Exist";
                    }
                    transaction.Commit();                    
                }
                catch (Exception es)
                {
                    transaction.Rollback();
                    student._failure = true;
                    student._message = es.Message;
                }
            }
            return student;
        }
        private bool CheckInternalID(Guid TenantId, string InternalID)
        {
            var checkInternalId = this.context?.StudentMaster.Where(x => x.TenantId == TenantId && x.StudentInternalId == InternalID).ToList();
            if (checkInternalId.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var studentUpdate = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.StudentId == student.studentMaster.StudentId);

                    studentUpdate.Salutation = student.studentMaster.Salutation;
                    studentUpdate.Suffix = student.studentMaster.Suffix;
                    studentUpdate.FirstGivenName = student.studentMaster.FirstGivenName;
                    studentUpdate.MiddleName = student.studentMaster.MiddleName;
                    studentUpdate.LastFamilyName = student.studentMaster.LastFamilyName;
                    studentUpdate.PreferredName = student.studentMaster.PreferredName;
                    studentUpdate.PreviousName = student.studentMaster.PreviousName;
                    studentUpdate.AlternateId = student.studentMaster.AlternateId;
                    studentUpdate.DistrictId = student.studentMaster.DistrictId;
                    studentUpdate.StateId = student.studentMaster.StateId;
                    studentUpdate.AdmissionNumber = student.studentMaster.AdmissionNumber;
                    studentUpdate.RollNumber = student.studentMaster.RollNumber;
                    studentUpdate.SocialSecurityNumber = student.studentMaster.SocialSecurityNumber;
                    studentUpdate.OtherGovtIssuedNumber = student.studentMaster.OtherGovtIssuedNumber;
                    studentUpdate.Dob = student.studentMaster.Dob;
                    studentUpdate.Gender = student.studentMaster.Gender;
                    studentUpdate.Race = student.studentMaster.Race;
                    studentUpdate.Ethnicity = student.studentMaster.Ethnicity;
                    studentUpdate.MaritalStatus = student.studentMaster.MaritalStatus;
                    studentUpdate.CountryOfBirth = student.studentMaster.CountryOfBirth;
                    studentUpdate.Nationality = student.studentMaster.Nationality;
                    studentUpdate.FirstLanguageId = student.studentMaster.FirstLanguageId;
                    studentUpdate.SecondLanguageId = student.studentMaster.SecondLanguageId;
                    studentUpdate.ThirdLanguageId = student.studentMaster.ThirdLanguageId;

                    studentUpdate.HomeAddressLineOne = student.studentMaster.HomeAddressLineOne;
                    studentUpdate.HomeAddressLineTwo = student.studentMaster.HomeAddressLineTwo;
                    studentUpdate.HomeAddressCountry = student.studentMaster.HomeAddressCountry;
                    studentUpdate.HomeAddressState = student.studentMaster.HomeAddressState;
                    studentUpdate.HomeAddressCity = student.studentMaster.HomeAddressCity;
                    studentUpdate.HomeAddressZip = student.studentMaster.HomeAddressZip;
                    studentUpdate.BusNo = student.studentMaster.BusNo;
                    studentUpdate.SchoolBusPickUp = student.studentMaster.SchoolBusPickUp;
                    studentUpdate.SchoolBusDropOff = student.studentMaster.SchoolBusDropOff;
                    studentUpdate.MailingAddressSameToHome = student.studentMaster.MailingAddressSameToHome;
                    studentUpdate.MailingAddressLineOne = student.studentMaster.MailingAddressLineOne;
                    studentUpdate.MailingAddressLineTwo = student.studentMaster.MailingAddressLineTwo;
                    studentUpdate.MailingAddressCountry = student.studentMaster.MailingAddressCountry;
                    studentUpdate.MailingAddressState = student.studentMaster.MailingAddressState;
                    studentUpdate.MailingAddressCity = student.studentMaster.MailingAddressCity;
                    studentUpdate.MailingAddressZip = student.studentMaster.MailingAddressZip;
                    studentUpdate.HomePhone = student.studentMaster.HomePhone;
                    studentUpdate.MobilePhone = student.studentMaster.MobilePhone;
                    studentUpdate.PersonalEmail = student.studentMaster.PersonalEmail;
                    studentUpdate.SchoolEmail = student.studentMaster.SchoolEmail;
                    studentUpdate.Twitter = student.studentMaster.Twitter;
                    studentUpdate.Facebook = student.studentMaster.Facebook;
                    studentUpdate.Instagram = student.studentMaster.Instagram;
                    studentUpdate.Youtube = student.studentMaster.Youtube;
                    studentUpdate.Linkedin = student.studentMaster.Linkedin;

                    studentUpdate.StudentPhoto = student.studentMaster.StudentPhoto;

                    studentUpdate.CriticalAlert = student.studentMaster.CriticalAlert;
                    studentUpdate.AlertDescription = student.studentMaster.AlertDescription;
                    studentUpdate.PrimaryCarePhysician = student.studentMaster.PrimaryCarePhysician;
                    studentUpdate.PrimaryCarePhysicianPhone = student.studentMaster.PrimaryCarePhysicianPhone;
                    studentUpdate.MedicalFacility = student.studentMaster.MedicalFacility;
                    studentUpdate.MedicalFacilityPhone = student.studentMaster.MedicalFacilityPhone;
                    studentUpdate.InsuranceCompany = student.studentMaster.InsuranceCompany;
                    studentUpdate.InsuranceCompanyPhone = student.studentMaster.InsuranceCompanyPhone;
                    studentUpdate.PolicyNumber = student.studentMaster.PolicyNumber;
                    studentUpdate.PolicyHolder = student.studentMaster.PolicyHolder;
                    studentUpdate.Dentist = student.studentMaster.Dentist;
                    studentUpdate.DentistPhone = student.studentMaster.DentistPhone;
                    studentUpdate.Vision = student.studentMaster.Vision;
                    studentUpdate.VisionPhone = student.studentMaster.VisionPhone;

                    studentUpdate.Eligibility504 = student.studentMaster.Eligibility504;
                    studentUpdate.EconomicDisadvantage = student.studentMaster.EconomicDisadvantage;
                    studentUpdate.FreeLunchEligibility = student.studentMaster.FreeLunchEligibility;
                    studentUpdate.SpecialEducationIndicator = student.studentMaster.SpecialEducationIndicator;
                    studentUpdate.LepIndicator = student.studentMaster.LepIndicator;
                    studentUpdate.SectionId = student.studentMaster.SectionId;
                    studentUpdate.EstimatedGradDate = student.studentMaster.EstimatedGradDate;
                    studentUpdate.StudentInternalId = student.studentMaster.StudentInternalId;
                    studentUpdate.LastUpdated = DateTime.UtcNow;
                    studentUpdate.UpdatedBy = student.studentMaster.UpdatedBy;

                    var checkInternalId = this.context?.StudentMaster.Where(x => x.TenantId ==student.studentMaster.TenantId && x.StudentInternalId == student.studentMaster.StudentInternalId && x.StudentId != student.studentMaster.StudentId).ToList();
                    if(checkInternalId.Count()>0)
                    {
                        student.studentMaster = null;
                        student._failure = true;
                        student._message = "Student InternalID Already Exist";
                    }
                    else
                    {
                        this.context?.SaveChanges();

                        if (student.fieldsCategoryList != null && student.fieldsCategoryList.ToList().Count > 0)
                        {
                            var fieldsCategory = student.fieldsCategoryList.FirstOrDefault(x => x.CategoryId == student.SelectedCategoryId);
                            if (fieldsCategory != null)
                            {
                                foreach (var customFields in fieldsCategory.CustomFields.ToList())
                                {
                                    var customFieldValueData = this.context?.CustomFieldsValue.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.CategoryId == customFields.CategoryId && x.FieldId == customFields.FieldId && x.Module == "Student" && x.TargetId == student.studentMaster.StudentId);
                                    if (customFieldValueData != null)
                                    {
                                        this.context?.CustomFieldsValue.RemoveRange(customFieldValueData);
                                    }
                                    if (customFields.CustomFieldsValue != null && customFields.CustomFieldsValue.ToList().Count > 0)
                                    {
                                        customFields.CustomFieldsValue.FirstOrDefault().Module = "Student";
                                        customFields.CustomFieldsValue.FirstOrDefault().CategoryId = customFields.CategoryId;
                                        customFields.CustomFieldsValue.FirstOrDefault().FieldId = customFields.FieldId;
                                        customFields.CustomFieldsValue.FirstOrDefault().CustomFieldTitle = customFields.Title;
                                        customFields.CustomFieldsValue.FirstOrDefault().CustomFieldType = customFields.Type;
                                        customFields.CustomFieldsValue.FirstOrDefault().SchoolId = student.studentMaster.SchoolId;
                                        customFields.CustomFieldsValue.FirstOrDefault().TargetId = student.studentMaster.StudentId;
                                        this.context?.CustomFieldsValue.AddRange(customFields.CustomFieldsValue);
                                        this.context?.SaveChanges();
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(student.PasswordHash) && !string.IsNullOrWhiteSpace(student.LoginEmail))
                        {
                            UserMaster userMaster = new UserMaster();

                            var decrypted = Utility.Decrypt(student.PasswordHash);
                            string passwordHash = Utility.GetHashedPassword(decrypted);

                            var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.EmailAddress == student.LoginEmail);

                            if (loginInfo == null)
                            {
                                var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.Profile == "Student");

                                userMaster.SchoolId = student.studentMaster.SchoolId;
                                userMaster.TenantId = student.studentMaster.TenantId;
                                userMaster.UserId = student.studentMaster.StudentId;
                                userMaster.LangId = 1;
                                userMaster.MembershipId = membership.MembershipId;
                                userMaster.EmailAddress = student.LoginEmail;
                                userMaster.PasswordHash = passwordHash;
                                userMaster.Name = student.studentMaster.FirstGivenName;
                                userMaster.LastUpdated = DateTime.UtcNow;
                                userMaster.IsActive = true;

                                this.context?.UserMaster.Add(userMaster);
                                this.context?.SaveChanges();


                                //Update StudentPortalId in Studentmaster table.
                                var studentPortalId = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.StudentId == student.studentMaster.StudentId);
                                studentUpdate.StudentPortalId = student.LoginEmail;

                                this.context?.SaveChanges();
                            }
                        }
                        student._failure = false;
                    }
                    transaction.Commit();
                   
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    student.studentMaster = null;
                    student._failure = true;
                    student._message = ex.Message;

                }
            }
            return student;

        }

        /// <summary>
        /// Get All Student With Pagination,sorting,searching
        /// </summary>
        /// <param name="pageResult"></param>
        /// <returns></returns>

        public StudentListModel GetAllStudentList(PageResult pageResult)
        {
            
            StudentListModel studentListModel = new StudentListModel();
            IQueryable<StudentMaster> transactionIQ = null;
            
            var StudentMasterList = this.context?.StudentMaster.Include(x=>x.StudentEnrollment).Where(x => x.TenantId == pageResult.TenantId && x.SchoolId == pageResult.SchoolId);           

            try
            {
                if (pageResult.FilterParams == null || pageResult.FilterParams.Count == 0)
                {

                    transactionIQ = StudentMasterList;
                }
                else
                {
                    if (pageResult.FilterParams != null && pageResult.FilterParams.ElementAt(0).ColumnName == null && pageResult.FilterParams.Count == 1)
                    {
                        string Columnvalue = pageResult.FilterParams.ElementAt(0).FilterValue;
                        transactionIQ = StudentMasterList.Where(x =>x.FirstGivenName!=null && x.FirstGivenName.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.MiddleName != null && x.MiddleName.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.LastFamilyName != null && x.LastFamilyName.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.StudentInternalId != null && x.StudentInternalId.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.AlternateId != null && x.AlternateId.Contains(Columnvalue) ||
                                                                    x.HomePhone !=null && x.HomePhone.Contains(Columnvalue) || 
                                                                    x.MobilePhone !=null && x.MobilePhone.Contains(Columnvalue)||
                                                                    x.PersonalEmail !=null && x.PersonalEmail.Contains(Columnvalue));

                        //search for gradeLavelTitle
                        //var childGrader = StudentMasterList.Where(x => x.StudentEnrollment.FirstOrDefault().EnrollmentDate);
                         var childGradeFilter = StudentMasterList.Where(x => x.StudentEnrollment.FirstOrDefault() != null ? x.StudentEnrollment.FirstOrDefault().GradeLevelTitle.ToLower().Contains(Columnvalue.ToLower()) : string.Empty.Contains(Columnvalue));
                        if (childGradeFilter.ToList().Count > 0)
                        {
                            transactionIQ = transactionIQ.Concat(childGradeFilter);
                        }
                        //search for Section
                        /*var childSectionFilter = StudentMasterList.Where(x => x.Sections != null ? x.Sections.Name.ToLower().Contains(Columnvalue.ToLower()) : string.Empty.Contains(Columnvalue));
                        //if (childSectionFilter.ToList().Count > 0)
                        //{
                        //    transactionIQ = transactionIQ.Concat(childSectionFilter);
                        //}*/
                    }
                    else
                    {
                        transactionIQ = Utility.FilteredData(pageResult.FilterParams, StudentMasterList).AsQueryable();
                    }
                    //transactionIQ = transactionIQ.Distinct();
                }

                if (pageResult.SortingModel != null)
                {
                    switch (pageResult.SortingModel.SortColumn.ToLower())
                    {
                        //Sorting For GradeLevelTitle
                        case "GradeLevelTitle":

                            if (pageResult.SortingModel.SortDirection.ToLower() == "asc")
                            {

                                transactionIQ = transactionIQ.OrderBy(a => a.StudentEnrollment != null ? a.StudentEnrollment.FirstOrDefault().GradeLevelTitle : null);
                            }
                            else
                            {
                                transactionIQ = transactionIQ.OrderByDescending(a => a.StudentEnrollment != null ? a.StudentEnrollment.FirstOrDefault().GradeLevelTitle : null);
                            }
                            break;
                            //sorting for Section
                        /*case "name":

                        //    if (pageResult.SortingModel.SortDirection.ToLower() == "asc")
                        //    {
                        //        transactionIQ = transactionIQ.OrderBy(b => b.Sections != null ? b.Sections.Name : null);
                        //    }
                        //    else
                        //    {
                        //        transactionIQ = transactionIQ.OrderByDescending(b => b.Sections != null ? b.Sections.Name : null);
                        //    }
                        //    break;*/
                        default:
                            transactionIQ = Utility.Sort(transactionIQ, pageResult.SortingModel.SortColumn, pageResult.SortingModel.SortDirection.ToLower());
                            break;
                    }
                }

                int totalCount = transactionIQ.Count();
                transactionIQ = transactionIQ.Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize);
                var studentList = transactionIQ.AsNoTracking().Select(s => new GetStudentListForView
                {
                    SchoolId = s.SchoolId,
                    StudentId = s.StudentId,
                    TenantId = s.TenantId,
                    GradeLevelTitle=s.StudentEnrollment.OrderByDescending(s => s.EnrollmentDate).FirstOrDefault().GradeLevelTitle,                  
                    StudentInternalId=s.StudentInternalId,
                    FirstGivenName=s.FirstGivenName,
                    MiddleName=s.MiddleName,
                    LastFamilyName=s.LastFamilyName,
                    AlternateId=s.AlternateId,
                    MobilePhone=s.MobilePhone,
                    PersonalEmail=s.PersonalEmail,                   
                }).ToList();

                studentListModel.TenantId = pageResult.TenantId;
                studentListModel.SchoolId = pageResult.SchoolId;
                studentListModel.getStudentListForViews = studentList;
                studentListModel.studentMaster = null;
                studentListModel.TotalCount = totalCount;
                studentListModel.PageNumber = pageResult.PageNumber;
                studentListModel._pageSize = pageResult.PageSize;
                studentListModel._tenantName = pageResult._tenantName;
                studentListModel._token = pageResult._token;
                studentListModel._failure = false;
            }
            catch (Exception es)
            {
                studentListModel._message = es.Message;
                studentListModel._failure = true;
                studentListModel._tenantName = pageResult._tenantName;
                studentListModel._token = pageResult._token;
            }
            return studentListModel;

        }

        /// <summary>
        /// Add StudentDocument
        /// </summary>
        /// <param name="studentDocumentAddViewModel"></param>
        /// <returns></returns>
        public StudentDocumentAddViewModel AddStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            try
            {
                int? MasterDocumentId = 0;

                if (studentDocumentAddViewModel.studentDocuments != null && studentDocumentAddViewModel.studentDocuments.ToList().Count > 0)
                {
                    MasterDocumentId = Utility.GetMaxPK(this.context, new Func<StudentDocuments, int>(x => x.DocumentId));

                    foreach (var data in studentDocumentAddViewModel.studentDocuments.ToList())
                    {
                        data.DocumentId = (int)MasterDocumentId;
                        data.UploadedOn = DateTime.UtcNow;
                        this.context?.StudentDocuments.Add(data);
                        MasterDocumentId++;
                    }

                    this.context?.SaveChanges();
                }

                studentDocumentAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                studentDocumentAddViewModel._failure = true;
                studentDocumentAddViewModel._message = es.Message;
            }
            return studentDocumentAddViewModel;
        }

        /// <summary>
        /// Update StudentDocument
        /// </summary>
        /// <param name="studentDocumentAddViewModel"></param>
        /// <returns></returns>
        public StudentDocumentAddViewModel UpdateStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            try
            {
                var studentDocumentUpdate = this.context?.StudentDocuments.FirstOrDefault(x => x.TenantId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().TenantId && x.SchoolId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().SchoolId && x.StudentId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().StudentId && x.DocumentId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().DocumentId);
                studentDocumentUpdate.FileUploaded = studentDocumentAddViewModel.studentDocuments.FirstOrDefault().FileUploaded;
                studentDocumentUpdate.UploadedOn = DateTime.UtcNow;
                studentDocumentUpdate.UploadedBy = studentDocumentAddViewModel.studentDocuments.FirstOrDefault().UploadedBy;
                this.context?.SaveChanges();
                studentDocumentAddViewModel._failure = false;
                studentDocumentAddViewModel._message = "Updated Successfully";
            }
            catch (Exception es)
            {
                studentDocumentAddViewModel._failure = true;
                studentDocumentAddViewModel._message = es.Message;
            }
            return studentDocumentAddViewModel;
        }

        /// <summary>
        /// Get All StudentDocuments List
        /// </summary>
        /// <param name="studentDocumentListViewModel"></param>
        /// <returns></returns>
        public StudentDocumentListViewModel GetAllStudentDocumentsList(StudentDocumentListViewModel studentDocumentListViewModel)
        {
            StudentDocumentListViewModel studentDocumentsList = new StudentDocumentListViewModel();
            try
            {

                var StudentDocumentsAll = this.context?.StudentDocuments.Where(x => x.TenantId == studentDocumentListViewModel.TenantId && x.SchoolId == studentDocumentListViewModel.SchoolId && x.StudentId == studentDocumentListViewModel.StudentId).OrderByDescending(x=>x.DocumentId).ToList();
                studentDocumentsList.studentDocumentsList = StudentDocumentsAll;
                studentDocumentsList._tenantName = studentDocumentListViewModel._tenantName;
                studentDocumentsList._token = studentDocumentListViewModel._token;
                studentDocumentsList._failure = false;
            }
            catch (Exception es)
            {
                studentDocumentsList._message = es.Message;
                studentDocumentsList._failure = true;
                studentDocumentsList._tenantName = studentDocumentListViewModel._tenantName;
                studentDocumentsList._token = studentDocumentListViewModel._token;
            }
            return studentDocumentsList;
        }

        /// <summary>
        /// Delete StudentDocument
        /// </summary>
        /// <param name="studentDocumentAddViewModel"></param>
        /// <returns></returns>
        public StudentDocumentAddViewModel DeleteStudentDocument(StudentDocumentAddViewModel studentDocumentAddViewModel)
        {
            try
            {
                var studentDocumentDelete = this.context?.StudentDocuments.FirstOrDefault(x => x.TenantId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().TenantId && x.SchoolId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().SchoolId && x.StudentId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().StudentId && x.DocumentId == studentDocumentAddViewModel.studentDocuments.FirstOrDefault().DocumentId);
                this.context?.StudentDocuments.Remove(studentDocumentDelete);
                this.context?.SaveChanges();
                studentDocumentAddViewModel._failure = false;
                studentDocumentAddViewModel._message = "Deleted Successfully";
            }
            catch (Exception es)
            {
                studentDocumentAddViewModel._failure = true;
                studentDocumentAddViewModel._message = es.Message;
            }
            return studentDocumentAddViewModel;
        }
        /// <summary>
        /// Add Student Login Info
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public LoginInfoAddModel AddStudentLoginInfo(LoginInfoAddModel login)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(login.userMaster.PasswordHash) && !string.IsNullOrWhiteSpace(login.userMaster.EmailAddress))
                {
                    var decrypted = Utility.Decrypt(login.userMaster.PasswordHash);
                    string passwordHash = Utility.GetHashedPassword(decrypted);

                    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == login.userMaster.TenantId && x.SchoolId == login.userMaster.SchoolId && x.EmailAddress == login.userMaster.EmailAddress);

                    if (loginInfo == null)
                    {
                        var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == login.userMaster.TenantId && x.SchoolId == login.userMaster.SchoolId && x.Profile == "Student");

                        login.userMaster.UserId = login.StudentId;
                        login.userMaster.LangId = 1;
                        login.userMaster.MembershipId = membership.MembershipId;
                        login.userMaster.PasswordHash = passwordHash;
                        login.userMaster.LastUpdated = DateTime.UtcNow;
                        login.userMaster.IsActive = true;

                        if (login.userMaster.UserSecretQuestions != null)
                        {
                            login.userMaster.UserSecretQuestions.UserId = login.StudentId;
                            login.userMaster.UserSecretQuestions.LastUpdated = DateTime.UtcNow;
                        }

                        this.context?.UserMaster.Add(login.userMaster);
                        this.context?.SaveChanges();

                        //Update StudentPortalId in Studentmaster table.
                        var student = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == login.userMaster.TenantId && x.SchoolId == login.userMaster.SchoolId && x.StudentId == login.StudentId);
                        student.StudentPortalId = login.userMaster.EmailAddress;

                        this.context?.SaveChanges();
                    }
                }
                login._failure = false;
            }
            catch (Exception es)
            {
                login._failure = true;
                login._message = es.Message;
            }

            return login;
        }
        /// <summary>
        /// Add Student Comment
        /// </summary>
        /// <param name="studentCommentAddViewModel"></param>
        /// <returns></returns>
        public StudentCommentAddViewModel AddStudentComment(StudentCommentAddViewModel studentCommentAddViewModel)
        {
            try
            {
                int? MasterCommentId = Utility.GetMaxPK(this.context, new Func<StudentComments, int>(x => x.CommentId));
                studentCommentAddViewModel.studentComments.CommentId = (int)MasterCommentId;
                studentCommentAddViewModel.studentComments.LastUpdated = DateTime.UtcNow;
                this.context?.StudentComments.Add(studentCommentAddViewModel.studentComments);
                this.context?.SaveChanges();
                studentCommentAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                studentCommentAddViewModel._failure = true;
                studentCommentAddViewModel._message = es.Message;
            }
            return studentCommentAddViewModel;
        }
        /// <summary>
        /// Update Student Comment
        /// </summary>
        /// <param name="studentCommentAddViewModel"></param>
        /// <returns></returns>
        public StudentCommentAddViewModel UpdateStudentComment(StudentCommentAddViewModel studentCommentAddViewModel)
        {
            try
            {
                var studentCommentUpdate = this.context?.StudentComments.FirstOrDefault(x => x.TenantId == studentCommentAddViewModel.studentComments.TenantId && x.SchoolId == studentCommentAddViewModel.studentComments.SchoolId && x.StudentId == studentCommentAddViewModel.studentComments.StudentId && x.CommentId == studentCommentAddViewModel.studentComments.CommentId);

                studentCommentUpdate.Comment = studentCommentAddViewModel.studentComments.Comment;
                studentCommentUpdate.LastUpdated = DateTime.UtcNow;
                studentCommentUpdate.UpdatedBy = studentCommentAddViewModel.studentComments.UpdatedBy;
                this.context?.SaveChanges();
                studentCommentAddViewModel._failure = false;
                studentCommentAddViewModel._message = "Updated Successfully";
            }
            catch (Exception es)
            {
                studentCommentAddViewModel._failure = true;
                studentCommentAddViewModel._message = es.Message;
            }
            return studentCommentAddViewModel;
        }
        /// <summary>
        /// Get All Student Comments List
        /// </summary>
        /// <param name="studentCommentListViewModel"></param>
        /// <returns></returns>
        public StudentCommentListViewModel GetAllStudentCommentsList(StudentCommentListViewModel studentCommentListViewModel)
        {
            StudentCommentListViewModel studentCommentsList = new StudentCommentListViewModel();
            try
            {

                var StudentCommentsAll = this.context?.StudentComments.Where(x => x.TenantId == studentCommentListViewModel.TenantId && x.SchoolId == studentCommentListViewModel.SchoolId && x.StudentId == studentCommentListViewModel.StudentId).OrderByDescending(x => x.CommentId).ToList();
                studentCommentsList.studentCommentsList = StudentCommentsAll;
                studentCommentsList._tenantName = studentCommentListViewModel._tenantName;
                studentCommentsList._token = studentCommentListViewModel._token;
                studentCommentsList._failure = false;
            }
            catch (Exception es)
            {
                studentCommentsList._message = es.Message;
                studentCommentsList._failure = true;
                studentCommentsList._tenantName = studentCommentListViewModel._tenantName;
                studentCommentsList._token = studentCommentListViewModel._token;
            }
            return studentCommentsList;
        }
        /// <summary>
        /// Delete Student Comment
        /// </summary>
        /// <param name="studentCommentAddViewModel"></param>
        /// <returns></returns>
        public StudentCommentAddViewModel DeleteStudentComment(StudentCommentAddViewModel studentCommentAddViewModel)
        {
            try
            {
                var studentCommentDelete = this.context?.StudentComments.FirstOrDefault(x => x.TenantId == studentCommentAddViewModel.studentComments.TenantId && x.SchoolId == studentCommentAddViewModel.studentComments.SchoolId && x.StudentId == studentCommentAddViewModel.studentComments.StudentId && x.CommentId == studentCommentAddViewModel.studentComments.CommentId);
                this.context?.StudentComments.Remove(studentCommentDelete);
                this.context?.SaveChanges();
                studentCommentAddViewModel._failure = false;
                studentCommentAddViewModel._message = "Deleted Successfully";
            }
            catch (Exception es)
            {
                studentCommentAddViewModel._failure = true;
                studentCommentAddViewModel._message = es.Message;
            }
            return studentCommentAddViewModel;
        }


        /// <summary>
        /// Add Student Enrollment
        /// </summary>
        /// <param name="studentEnrollmentAddView"></param>
        /// <returns></returns>
        public StudentEnrollmentListModel AddStudentEnrollment(StudentEnrollmentListModel studentEnrollmentListModel)
        {
            try
            {
                int? EnrollmentId = null;
                EnrollmentId = Utility.GetMaxPK(this.context, new Func<StudentEnrollment, int>(x => x.EnrollmentId));
                foreach (var studentEnrollment in studentEnrollmentListModel.studentEnrollments)
                {                  
                    
                    studentEnrollment.EnrollmentId = (int)EnrollmentId;
                    studentEnrollment.LastUpdated = DateTime.UtcNow;
                    this.context?.StudentEnrollment.AddRange(studentEnrollment);
                    EnrollmentId++;
                }
                this.context?.SaveChanges();
                studentEnrollmentListModel._failure = false;
            }
            catch (Exception es)
            {
                studentEnrollmentListModel._failure = true;
                studentEnrollmentListModel._message = es.Message;
            }

            return studentEnrollmentListModel;
        }

        public StudentEnrollmentListModel GetAllStudentEnrollment(StudentEnrollmentListModel studentEnrollmentListModel)
        {
            StudentEnrollmentListModel studentEnrollmentListView = new StudentEnrollmentListModel();
            try
            {

                var studentEnrollmentList = this.context?.StudentEnrollment.Where(x => x.TenantId == studentEnrollmentListModel.TenantId && x.StudentId == studentEnrollmentListModel.StudentId).ToList();
                if (studentEnrollmentList.Count>0)
                {
                    studentEnrollmentListView.studentEnrollments = studentEnrollmentList;
                    studentEnrollmentListView.studentEnrollments = studentEnrollmentList;
                    studentEnrollmentListView._tenantName = studentEnrollmentListModel._tenantName;
                    studentEnrollmentListView._token = studentEnrollmentListModel._token;
                    studentEnrollmentListView._failure = false;
                }
                else
                {
                    studentEnrollmentListView.studentEnrollments = null;
                    studentEnrollmentListView._tenantName = studentEnrollmentListModel._tenantName;
                    studentEnrollmentListView._token = studentEnrollmentListModel._token;
                    studentEnrollmentListView._failure = true;
                    studentEnrollmentListView._message = NORECORDFOUND;
                }                
            }
            catch (Exception es)
            {
                studentEnrollmentListView._message = es.Message;
                studentEnrollmentListView._failure = true;
                studentEnrollmentListView._tenantName = studentEnrollmentListModel._tenantName;
                studentEnrollmentListView._token = studentEnrollmentListModel._token;
            }
            return studentEnrollmentListView;
        }
        /// <summary>
        /// Update Enrollment
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        //    public StudentAddViewModel UpdateEnrollment(StudentAddViewModel student)
        //    {
        //        try
        //        {
        //            var studentUpdate = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == student.studentEnrollment.TenantId && x.SchoolId == student.studentEnrollment.SchoolId && x.StudentId == student.studentEnrollment.StudentId);

        //            studentUpdate.TenantId = student.studentEnrollment.TenantId;
        //            studentUpdate.SchoolId = student.studentEnrollment.SchoolId;
        //            studentUpdate.StudentId = student.studentEnrollment.StudentId;
        //            studentUpdate.EnrollmentId = student.studentEnrollment.EnrollmentId;
        //            studentUpdate.GradeId = student.studentEnrollment.GradeId;
        //            studentUpdate.SectionId = student.studentEnrollment.SectionId;
        //            studentUpdate.StartDate = student.studentEnrollment.StartDate;
        //            studentUpdate.EndDate = student.studentEnrollment.EndDate;
        //            studentUpdate.EnrollmentCode = student.studentEnrollment.EnrollmentCode;
        //            studentUpdate.DropCode = student.studentEnrollment.DropCode;
        //            studentUpdate.NextSchool = student.studentEnrollment.NextSchool;
        //            studentUpdate.CalendarId = student.studentEnrollment.CalendarId;
        //            studentUpdate.LastSchool = student.studentEnrollment.LastSchool;
        //            studentUpdate.LastUpdated = DateTime.UtcNow;
        //            studentUpdate.UpdatedBy = student.studentEnrollment.UpdatedBy;

        //            this.context?.SaveChanges();
        //            student._failure = false;

        //        }
        //        catch (Exception ex)
        //        {
        //            student.studentEnrollment = null;
        //            student._failure = true;
        //            student._message = ex.Message;

        //        }
        //        return student;

        //    }



        /// <summary>
        /// View Student By Id
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        public StudentAddViewModel ViewStudent(StudentAddViewModel student)
        {
            StudentAddViewModel studentView = new StudentAddViewModel();
            try
            {

                var studentData = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.StudentId == student.studentMaster.StudentId);
                if (studentData != null)
                {
                    studentView.studentMaster = studentData;

                    var customFields = this.context?.FieldsCategory.Where(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.Module == "Student").OrderByDescending(x=>x.IsSystemCategory).ThenBy(x=>x.SortOrder)
                        .Select(y => new FieldsCategory
                        {
                            TenantId = y.TenantId,
                            SchoolId = y.SchoolId,
                            CategoryId = y.CategoryId,
                            IsSystemCategory = y.IsSystemCategory,
                            Search = y.Search,
                            Title = y.Title,
                            Module = y.Module,
                            SortOrder = y.SortOrder,
                            Required = y.Required,
                            Hide = y.Hide,
                            LastUpdate = y.LastUpdate,
                            UpdatedBy = y.UpdatedBy,
                            CustomFields = y.CustomFields.Select(z => new CustomFields
                            {
                                TenantId = z.TenantId,
                                SchoolId = z.SchoolId,
                                CategoryId = z.CategoryId,
                                FieldId = z.FieldId,
                                Module = z.Module,
                                Type = z.Type,
                                Search = z.Search,
                                Title = z.Title,
                                SortOrder = z.SortOrder,
                                SelectOptions = z.SelectOptions,
                                SystemField = z.SystemField,
                                Required = z.Required,
                                DefaultSelection = z.DefaultSelection,
                                LastUpdate = z.LastUpdate,
                                UpdatedBy = z.UpdatedBy,
                                CustomFieldsValue = z.CustomFieldsValue.Where(w => w.TargetId == student.studentMaster.StudentId).ToList()
                            }).OrderByDescending(x=>x.SystemField).ThenBy(x=>x.SortOrder).ToList()
                        }).ToList();
                    studentView.fieldsCategoryList = customFields;
                }
                else
                {
                    studentView._failure = true;
                    studentView._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                studentView._failure = true;
                studentView._message = es.Message;
            }
            return studentView;
        }

        //    /// <summary>
        //    /// Delete Student
        //    /// </summary>
        //    /// <param name="student"></param>
        //    /// <returns></returns>

        //    public StudentAddViewModel DeleteStudent(StudentAddViewModel student)
        //    {
        //        try
        //        {
        //            var studentDel = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == student.studentEnrollment.TenantId && x.SchoolId == student.studentEnrollment.SchoolId && x.StudentId == student.studentEnrollment.StudentId);
        //            this.context?.StudentEnrollment.Remove(studentDel);
        //            this.context?.SaveChanges();
        //            student._failure = false;
        //            student._message = "Deleted";
        //        }
        //        catch (Exception es)
        //        {
        //            student._failure = true;
        //            student._message = es.Message;
        //        }
        //        return student;
        //    }

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
        /// Search Sibling For Student
        /// </summary>
        /// <param name="studentSiblingListViewModel"></param>
        /// <returns></returns>
        public SiblingSearchForStudentListModel SearchSiblingForStudent(SiblingSearchForStudentListModel studentSiblingListViewModel)
        {
            SiblingSearchForStudentListModel StudentSiblingList = new SiblingSearchForStudentListModel();
            try
            {
                int resultData;
                var StudentSibling = this.context?.StudentMaster.Where(x => x.FirstGivenName == studentSiblingListViewModel.FirstGivenName && x.LastFamilyName == studentSiblingListViewModel.LastFamilyName && x.TenantId == studentSiblingListViewModel.TenantId && (studentSiblingListViewModel.SchoolId == null || (x.SchoolId == studentSiblingListViewModel.SchoolId)) && (studentSiblingListViewModel.Dob == null || (x.Dob == studentSiblingListViewModel.Dob)) && (studentSiblingListViewModel.StudentInternalId == null || (x.StudentInternalId.ToLower().Trim() == studentSiblingListViewModel.StudentInternalId.ToLower().Trim()))).ToList();               
                if (StudentSibling.Count > 0)
                {
                    var siblingsOfStudent = StudentSibling.Select(s => new GetStudentForView
                    {
                        FirstGivenName = s.FirstGivenName,
                        LastFamilyName = s.LastFamilyName,
                        Dob = s.Dob,
                        StudentId = s.StudentId,
                        StudentInternalId=s.StudentInternalId,
                        SchoolId=s.SchoolId,
                        TenantId=s.TenantId,
                        SchoolName = this.context?.SchoolMaster.Where(x=>x.SchoolId== s.SchoolId)?.Select(e=>e.SchoolName).FirstOrDefault(),
                        Address= ToFullAddress(s.HomeAddressLineOne, s.HomeAddressLineTwo,
                    int.TryParse(s.HomeAddressCity, out resultData) == true ? this.context.City.Where(x => x.Id == Convert.ToInt32(s.HomeAddressCity)).FirstOrDefault().Name : s.HomeAddressCity,
                    int.TryParse(s.HomeAddressState, out resultData) == true ? this.context.State.Where(x => x.Id == Convert.ToInt32(s.HomeAddressState)).FirstOrDefault().Name : s.HomeAddressState,
                    int.TryParse(s.HomeAddressCountry, out resultData) == true ? this.context.Country.Where(x => x.Id == Convert.ToInt32(s.HomeAddressCountry)).FirstOrDefault().Name : string.Empty, s.HomeAddressZip),
                    }).ToList();
                    StudentSiblingList.getStudentForView = siblingsOfStudent;
                    StudentSiblingList._tenantName = studentSiblingListViewModel._tenantName;
                    StudentSiblingList._token = studentSiblingListViewModel._token;
                    StudentSiblingList._failure = false;
                }
                else
                {
                    StudentSiblingList._failure = true;
                    StudentSiblingList._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                StudentSiblingList._message = es.Message;
                StudentSiblingList._failure = true;
                StudentSiblingList._tenantName = studentSiblingListViewModel._tenantName;
                StudentSiblingList._token = studentSiblingListViewModel._token;
            }
            return StudentSiblingList;
        }

        /// <summary>
        /// Association Sibling
        /// </summary>
        /// <param name="siblingAddUpdateForStudentModel"></param>
        /// <returns></returns>
        public SiblingAddUpdateForStudentModel AssociationSibling(SiblingAddUpdateForStudentModel siblingAddUpdateForStudentModel)
        {
            SiblingAddUpdateForStudentModel siblingAddUpdateForStudent = new SiblingAddUpdateForStudentModel();
            try
            {
                if (siblingAddUpdateForStudentModel.studentMaster.StudentId > 0)
                {
                    var studentAssociateTo = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.studentMaster.StudentId);
                    var studentAssociateBy = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.StudentId);
                    if (studentAssociateTo != null)
                    {
                        if (studentAssociateTo.Associationship != null)
                        {
                            studentAssociateTo.Associationship = studentAssociateTo.Associationship + " | " + siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.SchoolId + "#" + siblingAddUpdateForStudentModel.StudentId;
                        }
                        else
                        {
                            studentAssociateTo.Associationship = siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.SchoolId + "#" + siblingAddUpdateForStudentModel.StudentId;
                        }
                        //this.context?.SaveChanges();
                    }
                    else
                    {
                        siblingAddUpdateForStudentModel._failure = true;
                        siblingAddUpdateForStudentModel._message = NORECORDFOUND;
                    }

                    if (studentAssociateBy != null)
                    {
                        if (studentAssociateBy.Associationship != null)
                        {
                            studentAssociateBy.Associationship = studentAssociateBy.Associationship + " | " + siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.studentMaster.SchoolId + "#" + siblingAddUpdateForStudentModel.studentMaster.StudentId;
                        }
                        else
                        {
                            studentAssociateBy.Associationship = siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.studentMaster.SchoolId + "#" + siblingAddUpdateForStudentModel.studentMaster.StudentId;
                        }
                        //this.context?.SaveChanges();
                    }
                    else
                    {
                        siblingAddUpdateForStudentModel._failure = true;
                        siblingAddUpdateForStudentModel._message = NORECORDFOUND;
                    }
                    this.context?.SaveChanges();
                }
            }
            catch (Exception es)
            {

                siblingAddUpdateForStudentModel._message = es.Message;
                siblingAddUpdateForStudentModel._failure = true;
                siblingAddUpdateForStudentModel._tenantName = siblingAddUpdateForStudentModel._tenantName;
                siblingAddUpdateForStudentModel._token = siblingAddUpdateForStudentModel._token;
            }
            return siblingAddUpdateForStudentModel;
        }

        /// <summary>
        /// View All Sibling
        /// </summary>
        /// <param name="studentListModel"></param>
        /// <returns></returns>
        public StudentListModel ViewAllSibling(StudentListModel studentListModel)
        {
            StudentListModel studentList = new StudentListModel();
            try
            {
                var Associationship = studentListModel.TenantId + "#" + studentListModel.SchoolId + "#" + studentListModel.StudentId;
                var studentAssociationship = this.context?.StudentMaster.Where(x => x.Associationship.Contains(Associationship)).Include(x=>x.SchoolMaster).ToList();
                if (studentAssociationship.Count > 0)
                {
                    studentList.studentMaster = studentAssociationship;
                    studentList._tenantName = studentListModel._tenantName;
                    studentList._token = studentListModel._token;
                    studentList._failure = false;
                }
                else
                {
                    studentList._failure = true;
                    studentList._message = NORECORDFOUND;                    
                }
            }
            catch (Exception es)
            {
                studentList._message = es.Message;
                studentList._failure = true;
                studentList._tenantName = studentListModel._tenantName;
                studentList._token = studentListModel._token;
            }
            return studentList;
        }

        /// <summary>
        /// Remove Sibling
        /// </summary>
        /// <param name="siblingAddUpdateForStudentModel"></param>
        /// <returns></returns>
        public SiblingAddUpdateForStudentModel RemoveSibling(SiblingAddUpdateForStudentModel siblingAddUpdateForStudentModel)
        {
            try
            {
                string StudentAssociateToAfterDel;
                string StudentAssociateByAfterDel;
                var StudentAssociateTo = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.studentMaster.StudentId);
                var StudentAssociateBy = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.StudentId);
                var StudentAssociateToDataDel = siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.studentMaster.SchoolId + "#" + siblingAddUpdateForStudentModel.studentMaster.StudentId;
                var StudentAssociateByDataDel = siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.SchoolId + "#" + siblingAddUpdateForStudentModel.StudentId;

                if (StudentAssociateTo != null)
                {
                    var AssociationshipToData = StudentAssociateTo.Associationship;

                    string[] StudentAssociateToWithSiblings = AssociationshipToData.Split(" | ", StringSplitOptions.RemoveEmptyEntries);

                    StudentAssociateToWithSiblings = StudentAssociateToWithSiblings.Where(w => w != StudentAssociateByDataDel).ToArray();

                    if (StudentAssociateToWithSiblings.Count() > 1)
                    {
                        StudentAssociateToAfterDel = string.Join(" | ", StudentAssociateToWithSiblings);
                    }
                    else if (StudentAssociateToWithSiblings.Count() == 1)
                    {
                        StudentAssociateToAfterDel = string.Concat(StudentAssociateToWithSiblings);
                    }
                    else
                    {
                        StudentAssociateToAfterDel = null;
                    }
                    StudentAssociateTo.Associationship = StudentAssociateToAfterDel;
                }

                if (StudentAssociateBy != null)
                {
                    var AssociationshipByData = StudentAssociateBy.Associationship;

                    string[] StudentAssociateByWithSiblings = AssociationshipByData.Split(" | ", StringSplitOptions.RemoveEmptyEntries);

                    StudentAssociateByWithSiblings = StudentAssociateByWithSiblings.Where(w => w != StudentAssociateToDataDel).ToArray();

                    if (StudentAssociateByWithSiblings.Count() > 1)
                    {
                        StudentAssociateByAfterDel = string.Join(" | ", StudentAssociateByWithSiblings);
                    }
                    else if (StudentAssociateByWithSiblings.Count() == 1)
                    {
                        StudentAssociateByAfterDel = string.Concat(StudentAssociateByWithSiblings);
                    }
                    else
                    {
                        StudentAssociateByAfterDel = null;
                    }
                    StudentAssociateBy.Associationship = StudentAssociateByAfterDel;
                }
                this.context?.SaveChanges();
                siblingAddUpdateForStudentModel._message = "Associationship Remove Successfully";
            }

            catch (Exception es)
            {
                siblingAddUpdateForStudentModel._failure = true;
                siblingAddUpdateForStudentModel._message = es.Message;
            }
            return siblingAddUpdateForStudentModel;
        }

        /// <summary>
        ///  Check Student InternalId Exist or Not
        /// </summary>
        /// <param name="checkStudentInternalIdViewModel"></param>
        /// <returns></returns>
        public CheckStudentInternalIdViewModel CheckStudentInternalId(CheckStudentInternalIdViewModel checkStudentInternalIdViewModel)
        {
            var checkInternalId = this.context?.StudentMaster.Where(x =>x.TenantId== checkStudentInternalIdViewModel.TenantId && x.StudentInternalId == checkStudentInternalIdViewModel.StudentInternalId).ToList();
            if(checkInternalId.Count()>0)
            {
                checkStudentInternalIdViewModel.IsValidInternalId = false;
                checkStudentInternalIdViewModel._message = "Student Internal Id Already Exist";
            }
            else
            {
                checkStudentInternalIdViewModel.IsValidInternalId = true;
                checkStudentInternalIdViewModel._message = "Student Internal Id Is Valid";
            }
            return checkStudentInternalIdViewModel;
        }
    }
}



