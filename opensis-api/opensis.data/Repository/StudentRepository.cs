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
            try
            {
                int? MasterStudentId = Utility.GetMaxPK(this.context, new Func<StudentMaster, int>(x => x.StudentId));
                student.studentMaster.StudentId = (int)MasterStudentId;

                int? MasterEnrollmentId = Utility.GetMaxPK(this.context, new Func<StudentEnrollment, int>(x => (int)x.EnrollmentId));
                student.studentMaster.StudentEnrollment = new StudentEnrollment() { TenantId = student.studentMaster.TenantId, SchoolId = student.studentMaster.SchoolId, StudentId = student.studentMaster.StudentId, EnrollmentId = (int)MasterEnrollmentId };

                this.context?.StudentMaster.Add(student.studentMaster);
                this.context?.SaveChanges();
                student._failure = false;
            }
            catch (Exception es)
            {
                student._failure = true;
                student._message = es.Message;
            }

            return student;
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentAddViewModel UpdateStudent(StudentAddViewModel student)
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

                //studentUpdate.PrimaryContactFirstname = student.studentMaster.PrimaryContactFirstname;
                //studentUpdate.PrimaryContactLastname = student.studentMaster.PrimaryContactLastname;
                //studentUpdate.PrimaryContactRelationship = student.studentMaster.PrimaryContactRelationship;
                //studentUpdate.PrimaryContactEmail = student.studentMaster.PrimaryContactEmail;
                //studentUpdate.PrimaryContactMobile = student.studentMaster.PrimaryContactMobile;
                //studentUpdate.PrimaryContactWorkPhone = student.studentMaster.PrimaryContactWorkPhone;
                //studentUpdate.PrimaryContactHomePhone = student.studentMaster.PrimaryContactHomePhone;
                //studentUpdate.PrimaryContactAddressLineOne = student.studentMaster.PrimaryContactAddressLineOne;
                //studentUpdate.PrimaryContactAddressLineTwo = student.studentMaster.PrimaryContactAddressLineTwo;
                //studentUpdate.PrimaryContactCountry = student.studentMaster.PrimaryContactCountry;
                //studentUpdate.PrimaryContactState = student.studentMaster.PrimaryContactState;
                //studentUpdate.PrimaryContactCity = student.studentMaster.PrimaryContactCity;
                //studentUpdate.PrimaryContactZip = student.studentMaster.PrimaryContactZip;

                //studentUpdate.SecondaryContactFirstname = student.studentMaster.SecondaryContactFirstname;
                //studentUpdate.SecondaryContactLastname = student.studentMaster.SecondaryContactLastname;
                //studentUpdate.SecondaryContactRelationship = student.studentMaster.SecondaryContactRelationship;
                //studentUpdate.SecondaryContactEmail = student.studentMaster.SecondaryContactEmail;
                //studentUpdate.SecondaryContactMobile = student.studentMaster.SecondaryContactMobile;
                //studentUpdate.SecondaryContactWorkPhone = student.studentMaster.SecondaryContactWorkPhone;
                //studentUpdate.SecondaryContactHomePhone = student.studentMaster.SecondaryContactHomePhone;
                //studentUpdate.SecondaryContactAddressLineOne = student.studentMaster.SecondaryContactAddressLineOne;
                //studentUpdate.SecondaryContactAddressLineTwo = student.studentMaster.SecondaryContactAddressLineTwo;
                //studentUpdate.SecondaryContactCountry = student.studentMaster.SecondaryContactCountry;
                //studentUpdate.SecondaryContactState = student.studentMaster.SecondaryContactState;
                //studentUpdate.SecondaryContactCity = student.studentMaster.SecondaryContactCity;
                //studentUpdate.SecondaryContactZip = student.studentMaster.SecondaryContactZip;

                this.context?.SaveChanges();

                if(!string.IsNullOrWhiteSpace(student.PasswordHash) && !string.IsNullOrWhiteSpace(student.studentMaster.PersonalEmail))
                {
                    UserMaster userMaster = new UserMaster();

                    var decrypted = Utility.Decrypt(student.PasswordHash);
                    string passwordHash = Utility.GetHashedPassword(decrypted);

                    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.EmailAddress==student.studentMaster.PersonalEmail);

                    if (loginInfo == null)
                    {
                        var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.Profile == "Student");

                        userMaster.SchoolId = student.studentMaster.SchoolId;
                        userMaster.TenantId = student.studentMaster.TenantId;
                        userMaster.UserId = student.studentMaster.StudentId;
                        userMaster.LangId = 1;
                        userMaster.MembershipId = membership.MembershipId;
                        userMaster.EmailAddress = student.studentMaster.PersonalEmail;
                        userMaster.PasswordHash = passwordHash;
                        userMaster.Name = student.studentMaster.FirstGivenName;

                        this.context?.UserMaster.Add(userMaster);
                        this.context?.SaveChanges();
                    }
                }

                student._failure = false;

            }
            catch (Exception ex)
            {
                student.studentMaster = null;
                student._failure = true;
                student._message = ex.Message;

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
            var StudentMasterList = this.context?.StudentMaster.Include(s => s.StudentEnrollment).Include(p => p.StudentEnrollment.Sections).Include(p => p.StudentEnrollment.Gradelevels).Where(x => x.TenantId == pageResult.TenantId && x.SchoolId == pageResult.SchoolId);
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
                        transactionIQ = StudentMasterList.Where(x => x.FirstGivenName.ToLower().Contains(Columnvalue.ToLower()) || x.MiddleName.ToLower().Contains(Columnvalue.ToLower()) || x.LastFamilyName.ToLower().Contains(Columnvalue.ToLower()) || x.StudentId.ToString().Contains(Columnvalue) || x.AlternateId.Contains(Columnvalue) || x.HomePhone.Contains(Columnvalue) || x.MobilePhone.Contains(Columnvalue));
                        
                        var childGradeFilter = StudentMasterList.Where(x => x.StudentEnrollment.Gradelevels != null ? x.StudentEnrollment.Gradelevels.Title.ToLower().Contains(Columnvalue.ToLower()) : string.Empty.Contains(Columnvalue));
                        if (childGradeFilter.ToList().Count > 0)
                        {
                            transactionIQ = transactionIQ.Concat(childGradeFilter);
                        }
                        var childSectionFilter = StudentMasterList.Where(x => x.StudentEnrollment.Sections != null ? x.StudentEnrollment.Sections.Name.ToLower().Contains(Columnvalue.ToLower()) : string.Empty.Contains(Columnvalue));
                        if (childSectionFilter.ToList().Count > 0)
                        {
                            transactionIQ = transactionIQ.Concat(childSectionFilter);
                        }
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
                        case "title":

                            if (pageResult.SortingModel.SortDirection.ToLower() == "asc")
                            {

                                transactionIQ = transactionIQ.OrderBy(a => a.StudentEnrollment.Gradelevels != null ? a.StudentEnrollment.Gradelevels.Title : null);
                            }
                            else
                            {
                                transactionIQ = transactionIQ.OrderByDescending(a => a.StudentEnrollment.Gradelevels != null ? a.StudentEnrollment.Gradelevels.Title : null);
                            }
                            break;

                        case "name":

                            if (pageResult.SortingModel.SortDirection.ToLower() == "asc")
                            {
                                transactionIQ = transactionIQ.OrderBy(b => b.StudentEnrollment.Sections != null ? b.StudentEnrollment.Sections.Name : null);
                            }
                            else
                            {
                                transactionIQ = transactionIQ.OrderByDescending(b => b.StudentEnrollment.Sections != null ? b.StudentEnrollment.Sections.Name : null);
                            }
                            break;
                        default:
                            transactionIQ = Utility.Sort(transactionIQ, pageResult.SortingModel.SortColumn, pageResult.SortingModel.SortDirection.ToLower());
                            break;
                    }
                }

                int totalCount = transactionIQ.Count();
                transactionIQ = transactionIQ.Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize);
                var studentList = transactionIQ.ToList();
                
                studentListModel.TenantId = pageResult.TenantId;
                studentListModel.SchoolId = pageResult.SchoolId;
                studentListModel.studentMaster = studentList;
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
        /// SearchContact For Student
        /// </summary>
        /// <param name="searchContactViewModel"></param>
        /// <returns></returns>

        public SearchContactViewModel SearchContactForStudent(SearchContactViewModel searchContactViewModel)
        {
            SearchContactViewModel contactViewModel = new SearchContactViewModel();
            IQueryable<StudentMaster> transactionIQ = null;
            var StudentMasterList = this.context?.StudentMaster.Where(x => x.TenantId == searchContactViewModel.TenantId && x.SchoolId == searchContactViewModel.SchoolId);
            try
            {
                if (searchContactViewModel.FilterValue != null)
                {
                    string Columnvalue = searchContactViewModel.FilterValue;
                    //transactionIQ = StudentMasterList.Where(x => x.PrimaryContactFirstname.ToLower().Contains(Columnvalue.ToLower()) || x.PrimaryContactLastname.ToLower().Contains(Columnvalue.ToLower()) || x.PrimaryContactHomePhone.ToLower().Contains(Columnvalue.ToLower()) || x.PrimaryContactWorkPhone.ToLower().Contains(Columnvalue.ToLower()) || x.PrimaryContactMobile.ToLower().Contains(Columnvalue.ToLower()) || x.PrimaryContactEmail.ToLower().Contains(Columnvalue.ToLower())
                    //|| x.SecondaryContactFirstname.ToLower().Contains(Columnvalue.ToLower()) || x.SecondaryContactLastname.ToLower().Contains(Columnvalue.ToLower()) || x.SecondaryContactHomePhone.ToLower().Contains(Columnvalue.ToLower()) || x.SecondaryContactWorkPhone.ToLower().Contains(Columnvalue.ToLower()) || x.SecondaryContactMobile.ToLower().Contains(Columnvalue.ToLower()) || x.SecondaryContactEmail.ToLower().Contains(Columnvalue.ToLower()));

                    int checkCount = transactionIQ.Count();
                    if (checkCount < 1)
                    {
                        contactViewModel._message = "NO Data Found";
                        contactViewModel._failure = true;
                        contactViewModel._tenantName = searchContactViewModel._tenantName;
                        contactViewModel._token = searchContactViewModel._token;
                    }
                }
                else
                {
                    contactViewModel._message = "Please Provide Value";
                    contactViewModel._failure = true;
                    contactViewModel._tenantName = searchContactViewModel._tenantName;
                    contactViewModel._token = searchContactViewModel._token;
                }

                var studentList = transactionIQ.ToList();

                contactViewModel.TenantId = searchContactViewModel.TenantId;
                contactViewModel.studentMaster = studentList;
                contactViewModel._tenantName = searchContactViewModel._tenantName;
                contactViewModel._token = searchContactViewModel._token;
                contactViewModel._failure = false;
            }
            catch (Exception es)
            {
                contactViewModel._message = es.Message;
                contactViewModel._failure = true;
                contactViewModel._tenantName = searchContactViewModel._tenantName;
                contactViewModel._token = searchContactViewModel._token;
            }
            return contactViewModel;

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
                int? MasterDocumentId = Utility.GetMaxPK(this.context, new Func<StudentDocuments, int>(x => x.DocumentId));
                studentDocumentAddViewModel.studentDocument.DocumentId = (int)MasterDocumentId;
                studentDocumentAddViewModel.studentDocument.UploadedOn = DateTime.UtcNow;
                this.context?.StudentDocuments.Add(studentDocumentAddViewModel.studentDocument);
                this.context?.SaveChanges();
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
                var studentDocumentUpdate = this.context?.StudentDocuments.FirstOrDefault(x => x.TenantId == studentDocumentAddViewModel.studentDocument.TenantId && x.SchoolId == studentDocumentAddViewModel.studentDocument.SchoolId && x.StudentId == studentDocumentAddViewModel.studentDocument.StudentId && x.DocumentId == studentDocumentAddViewModel.studentDocument.DocumentId);
                studentDocumentUpdate.FileUploaded = studentDocumentAddViewModel.studentDocument.FileUploaded;
                studentDocumentUpdate.UploadedOn = DateTime.UtcNow;
                studentDocumentUpdate.UploadedBy = studentDocumentAddViewModel.studentDocument.UploadedBy;
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

                var StudentDocumentsAll = this.context?.StudentDocuments.Where(x => x.TenantId == studentDocumentListViewModel.TenantId && x.SchoolId == studentDocumentListViewModel.SchoolId && x.StudentId== studentDocumentListViewModel.StudentId).ToList();
                studentDocumentsList.studentDocumentList = StudentDocumentsAll;
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
                var studentDocumentDelete = this.context?.StudentDocuments.FirstOrDefault(x => x.TenantId == studentDocumentAddViewModel.studentDocument.TenantId && x.SchoolId == studentDocumentAddViewModel.studentDocument.SchoolId && x.StudentId == studentDocumentAddViewModel.studentDocument.StudentId && x.DocumentId== studentDocumentAddViewModel.studentDocument.DocumentId);
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
        /// Add Enrollment
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        //  public StudentAddViewModel AddEnrollment(StudentAddViewModel student)
        //  {
        //    try
        //    {
        //        int? MasterStudentId = Utility.GetMaxPK(this.context, new Func<StudentEnrollment, int>(x => x.StudentId));
        //        student.studentEnrollment.StudentId = (int)MasterStudentId;
        //        student.studentEnrollment.LastUpdated = DateTime.UtcNow;
        //        this.context?.StudentEnrollment.Add(student.studentEnrollment);
        //        this.context?.SaveChanges();
        //        student._failure = false;
        //    }
        //    catch (Exception es)
        //    {
        //        student._failure = true;
        //        student._message = es.Message;
        //    }

        //    return student;
        //}

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
        //    /// <summary>
        //    /// View Student By Id
        //    /// </summary>
        //    /// <param name="student"></param>
        //    /// <returns></returns>

        //    public StudentAddViewModel ViewStudent(StudentAddViewModel student)
        //    {
        //        StudentAddViewModel studentView = new StudentAddViewModel();
        //        try
        //        {

        //            var studentById = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == student.studentEnrollment.TenantId && x.SchoolId == student.studentEnrollment.SchoolId && x.StudentId == student.studentEnrollment.StudentId);
        //            if (studentById != null)
        //            {
        //                studentView.studentEnrollment = studentById;                    
        //            }
        //            else
        //            {
        //                studentView._failure = true;
        //                studentView._message = NORECORDFOUND;           
        //            }
        //        }
        //        catch (Exception es)
        //        {
        //            studentView._failure = true;
        //            studentView._message=es.Message;
        //        }
        //        return studentView;
        //    }

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
    }
}



