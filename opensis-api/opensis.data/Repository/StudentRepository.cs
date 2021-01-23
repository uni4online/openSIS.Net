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
                    //int? MasterStudentId = Utility.GetMaxPK(this.context, new Func<StudentMaster, int>(x => x.StudentId));
                    int? MasterStudentId = 1;

                    var studentData = this.context?.StudentMaster.Where(x => x.SchoolId == student.studentMaster.SchoolId && x.TenantId == student.studentMaster.TenantId).OrderByDescending(x => x.StudentId).FirstOrDefault();

                    if (studentData != null)
                    {
                        MasterStudentId = studentData.StudentId + 1;
                    }

                    student.studentMaster.StudentId = (int)MasterStudentId;
                    Guid GuidId = Guid.NewGuid();
                    var GuidIdExist = this.context?.StudentMaster.FirstOrDefault(x => x.StudentGuid == GuidId);
                    if (GuidIdExist != null)
                    {
                        student._failure = true;
                        student._message = "Guid is already exist, Please try again.";
                        return student;
                    }
                    student.studentMaster.StudentGuid = GuidId;
                    student.studentMaster.IsActive = true;
                    student.studentMaster.EnrollmentType = "Internal";

                    if (!string.IsNullOrEmpty(student.studentMaster.StudentInternalId))
                    {
                        bool checkInternalID = CheckInternalID(student.studentMaster.TenantId, student.studentMaster.StudentInternalId, student.studentMaster.SchoolId);
                        if (checkInternalID == false)
                        {
                            student.studentMaster = null;
                            student.fieldsCategoryList = null;
                            student._failure = true;
                            student._message = "Student InternalID Already Exist";
                            return student;
                        }
                    }
                    else
                    {
                        student.studentMaster.StudentInternalId = MasterStudentId.ToString();
                    }

                    var schoolName = this.context?.SchoolMaster.Where(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId).Select(s => s.SchoolName).FirstOrDefault();

                    //Insert data into Enrollment table
                    int? calenderId = null;
                    string enrollmentCode = null;

                    var defaultCalender = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.AcademicYear.ToString() == student.AcademicYear && x.DefaultCalender == true);

                    if (defaultCalender != null)
                    {
                        calenderId = defaultCalender.CalenderId;
                    }

                    var enrollmentType = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.Type.ToLower() == "Add".ToLower());

                    if (enrollmentType != null)
                    {
                        enrollmentCode = enrollmentType.Title;
                    }

                    var gradeLevel = this.context?.Gradelevels.Where(x => x.SchoolId == student.studentMaster.SchoolId).OrderBy(x => x.GradeId).FirstOrDefault();
                    var StudentEnrollmentData = new StudentEnrollment() { TenantId = student.studentMaster.TenantId, SchoolId = student.studentMaster.SchoolId, StudentId = student.studentMaster.StudentId, EnrollmentId = 1, SchoolName = schoolName, RollingOption = "Next grade at current school", EnrollmentCode = enrollmentCode, CalenderId = calenderId, GradeLevelTitle = (gradeLevel != null) ? gradeLevel.Title : null, EnrollmentDate = DateTime.UtcNow, StudentGuid = GuidId, IsActive = true };

                    //Add student portal access
                    if (!string.IsNullOrWhiteSpace(student.PasswordHash) && !string.IsNullOrWhiteSpace(student.LoginEmail))
                    {
                        UserMaster userMaster = new UserMaster();

                        var decrypted = Utility.Decrypt(student.PasswordHash);
                        string passwordHash = Utility.GetHashedPassword(decrypted);

                        var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.EmailAddress == student.LoginEmail);

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
                            userMaster.IsActive = student.PortalAccess;
                            student.studentMaster.StudentPortalId = student.LoginEmail;
                            this.context?.UserMaster.Add(userMaster);
                            this.context?.SaveChanges();
                        }
                        else
                        {
                            student.studentMaster = null;
                            student.fieldsCategoryList = null;
                            student._failure = true;
                            student._message = "Student Login Email Already Exist";
                            return student;
                        }
                    }

                    this.context?.StudentMaster.Add(student.studentMaster);
                    this.context?.StudentEnrollment.Add(StudentEnrollmentData);
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

                    student._failure = false;

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
        private bool CheckInternalID(Guid TenantId, string InternalID,int SchoolId)
        {
            if (InternalID != null && InternalID != "")
            {
                var checkInternalId = this.context?.StudentMaster.Where(x => x.TenantId == TenantId && x.StudentInternalId == InternalID && x.SchoolId == SchoolId).ToList();
                if (checkInternalId.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
                    var checkInternalId = this.context?.StudentMaster.Where(x => x.TenantId == student.studentMaster.TenantId && x.StudentInternalId == student.studentMaster.StudentInternalId && x.StudentInternalId != null && x.StudentId != student.studentMaster.StudentId ).ToList();
                    if(checkInternalId.Count()>0)
                    {
                        student.studentMaster = null;
                        student.fieldsCategoryList = null;
                        student._failure = true;
                        student._message = "Student InternalID Already Exist";
                    }
                    else
                    {
                        var studentUpdate = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.StudentId == student.studentMaster.StudentId);

                        if(string.IsNullOrEmpty(student.studentMaster.StudentInternalId))
                        {
                            student.studentMaster.StudentInternalId = studentUpdate.StudentInternalId;
                        }

                        //Add or Update student portal access
                        if (studentUpdate.StudentPortalId != null)
                        {
                            if (!string.IsNullOrWhiteSpace(student.LoginEmail))
                            {
                                if (studentUpdate.StudentPortalId != student.LoginEmail)
                                {
                                    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.EmailAddress == student.LoginEmail);

                                    if (loginInfo != null)
                                    {
                                        student.studentMaster = null;
                                        student.fieldsCategoryList = null;
                                        student._failure = true;
                                        student._message = "Student Login Email Already Exist";
                                        return student;
                                    }
                                    else
                                    {
                                        var loginInfoData = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.EmailAddress == studentUpdate.StudentPortalId);

                                        loginInfoData.EmailAddress = student.LoginEmail;
                                        loginInfoData.IsActive = student.PortalAccess;

                                        this.context?.UserMaster.Add(loginInfoData);
                                        this.context?.SaveChanges();

                                        //Update StudentPortalId in Studentmaster table.
                                        //studentUpdate.StudentPortalId = student.LoginEmail;
                                        student.studentMaster.StudentPortalId = student.LoginEmail;
                                    }
                                }
                                else
                                {
                                    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.EmailAddress == studentUpdate.StudentPortalId);

                                    loginInfo.IsActive = student.PortalAccess;

                                    this.context?.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(student.LoginEmail) && !string.IsNullOrWhiteSpace(student.PasswordHash))
                            {
                                var decrypted = Utility.Decrypt(student.PasswordHash);
                                string passwordHash = Utility.GetHashedPassword(decrypted);

                                UserMaster userMaster = new UserMaster();

                                var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.EmailAddress == student.LoginEmail);

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
                                    userMaster.IsActive = student.PortalAccess;

                                    this.context?.UserMaster.Add(userMaster);
                                    this.context?.SaveChanges();


                                    //Update StudentPortalId in Studentmaster table.
                                    //studentUpdate.StudentPortalId = student.LoginEmail;
                                    student.studentMaster.StudentPortalId = student.LoginEmail;
                                }
                                else
                                {
                                    student.studentMaster = null;
                                    student.fieldsCategoryList = null;
                                    student._failure = true;
                                    student._message = "Student Login Email Already Exist";
                                    return student;
                                }
                            }
                        }

                        student.studentMaster.Associationship = studentUpdate.Associationship;
                        student.studentMaster.EnrollmentType = studentUpdate.EnrollmentType;
                        student.studentMaster.IsActive = studentUpdate.IsActive;
                        student.studentMaster.StudentGuid = studentUpdate.StudentGuid;
                        student.studentMaster.LastUpdated = DateTime.UtcNow;
                        this.context.Entry(studentUpdate).CurrentValues.SetValues(student.studentMaster);
                        this.context?.SaveChanges();

                        

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

                        //if (!string.IsNullOrWhiteSpace(student.PasswordHash) && !string.IsNullOrWhiteSpace(student.LoginEmail))
                        //{
                        //    UserMaster userMaster = new UserMaster();

                        //    var decrypted = Utility.Decrypt(student.PasswordHash);
                        //    string passwordHash = Utility.GetHashedPassword(decrypted);

                        //    var loginInfo = this.context?.UserMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.EmailAddress == student.LoginEmail);

                        //    if (loginInfo == null)
                        //    {
                        //        var membership = this.context?.Membership.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.Profile == "Student");

                        //        userMaster.SchoolId = student.studentMaster.SchoolId;
                        //        userMaster.TenantId = student.studentMaster.TenantId;
                        //        userMaster.UserId = student.studentMaster.StudentId;
                        //        userMaster.LangId = 1;
                        //        userMaster.MembershipId = membership.MembershipId;
                        //        userMaster.EmailAddress = student.LoginEmail;
                        //        userMaster.PasswordHash = passwordHash;
                        //        userMaster.Name = student.studentMaster.FirstGivenName;
                        //        userMaster.LastUpdated = DateTime.UtcNow;
                        //        userMaster.IsActive = true;

                        //        this.context?.UserMaster.Add(userMaster);
                        //        this.context?.SaveChanges();


                        //        //Update StudentPortalId in Studentmaster table.
                        //        var studentPortalId = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == student.studentMaster.TenantId && x.SchoolId == student.studentMaster.SchoolId && x.StudentId == student.studentMaster.StudentId);
                        //        studentUpdate.StudentPortalId = student.LoginEmail;

                        //        this.context?.SaveChanges();
                        //    }
                        //}
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
            string Columnvalue = pageResult.FilterParams.ElementAt(0).FilterValue;

            var studentDataList = this.context?.StudentMaster.Include(x => x.StudentEnrollment).Where(x => x.TenantId == pageResult.TenantId && x.SchoolId == pageResult.SchoolId && x.IsActive != false);

            try
            {
                if (pageResult.FilterParams == null || pageResult.FilterParams.Count == 0)
                {
                    transactionIQ = studentDataList;
                }
                else
                {
                    if (pageResult.FilterParams != null && pageResult.FilterParams.ElementAt(0).ColumnName == null && pageResult.FilterParams.Count == 1)
                    {
                        
                        transactionIQ = studentDataList.Where(x => x.FirstGivenName != null && x.FirstGivenName.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.MiddleName != null && x.MiddleName.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.LastFamilyName != null && x.LastFamilyName.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.StudentInternalId != null && x.StudentInternalId.ToLower().Contains(Columnvalue.ToLower()) ||
                                                                    x.AlternateId != null && x.AlternateId.Contains(Columnvalue) ||
                                                                    x.HomePhone != null && x.HomePhone.Contains(Columnvalue) ||
                                                                    x.MobilePhone != null && x.MobilePhone.Contains(Columnvalue) ||
                                                                    x.PersonalEmail != null && x.PersonalEmail.Contains(Columnvalue) ||
                                                                    x.SchoolEmail != null && x.SchoolEmail.Contains(Columnvalue)
                                                                    ).AsQueryable();
                        //for GradeLevel Searching
                        //var gradeLevelFilter = studentDataList.Where(x => x.StudentEnrollment.ToList().Count > 0 ? x.StudentEnrollment.FirstOrDefault().GradeLevelTitle.ToLower().Contains(Columnvalue.ToLower()) : string.Empty.Contains(Columnvalue));

                        var gradeLevelFilter = studentDataList.Where(x=>x.StudentEnrollment.Any(e=>e.GradeLevelTitle.ToLower().Contains(Columnvalue.ToLower()) && e.IsActive == true));

                        if (gradeLevelFilter.ToList().Count > 0)
                        {
                            transactionIQ = transactionIQ.Concat(gradeLevelFilter);
                        }
                    }
                    else
                    {
                        transactionIQ = Utility.FilteredData(pageResult.FilterParams, studentDataList).AsQueryable();
                    }
                    //transactionIQ = transactionIQ.Distinct();
                }

                if (pageResult.SortingModel != null)
                {
                    switch (pageResult.SortingModel.SortColumn.ToLower())
                    {
                        //For GradeLevel Sorting
                        case "gradeleveltitle":

                            if (pageResult.SortingModel.SortDirection.ToLower() == "asc")
                            {

                                transactionIQ = transactionIQ.OrderBy(a => a.StudentEnrollment.Count > 0 ? a.StudentEnrollment.FirstOrDefault().GradeLevelTitle : null);
                            }
                            else
                            {
                                transactionIQ = transactionIQ.OrderByDescending(a => a.StudentEnrollment.Count > 0 ? a.StudentEnrollment.FirstOrDefault().GradeLevelTitle : null);
                            }
                            break;

                        default:
                            transactionIQ = Utility.Sort(transactionIQ, pageResult.SortingModel.SortColumn, pageResult.SortingModel.SortDirection.ToLower());
                            break;
                    }
                    //transactionIQ = Utility.Sort(transactionIQ, pageResult.SortingModel.SortColumn, pageResult.SortingModel.SortDirection.ToLower());
                }
                int totalCount = transactionIQ.Count();
                if (pageResult.PageNumber > 0 && pageResult.PageSize > 0)
                {
                    transactionIQ = transactionIQ.Skip((pageResult.PageNumber - 1) * pageResult.PageSize).Take(pageResult.PageSize);
                }
                

                studentListModel.TenantId = pageResult.TenantId;
                studentListModel.SchoolId = pageResult.SchoolId;
                studentListModel.studentMaster = transactionIQ.ToList();
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

                studentDocumentAddViewModel.studentDocuments.FirstOrDefault().UploadedOn = DateTime.UtcNow;
                this.context.Entry(studentDocumentUpdate).CurrentValues.SetValues(studentDocumentAddViewModel.studentDocuments.FirstOrDefault());
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
                if (StudentDocumentsAll.Count > 0)
                {
                    studentDocumentsList.studentDocumentsList = StudentDocumentsAll;
                    studentDocumentsList._tenantName = studentDocumentListViewModel._tenantName;
                    studentDocumentsList._token = studentDocumentListViewModel._token;
                    studentDocumentsList._failure = false;
                }
                else
                {
                    studentDocumentsList.studentDocumentsList = null;
                    studentDocumentsList._tenantName = studentDocumentListViewModel._tenantName;
                    studentDocumentsList._token = studentDocumentListViewModel._token;
                    studentDocumentsList._failure = true;
                    studentDocumentsList._message = NORECORDFOUND;
                }
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

                studentCommentAddViewModel.studentComments.LastUpdated = DateTime.UtcNow;
                this.context.Entry(studentCommentUpdate).CurrentValues.SetValues(studentCommentAddViewModel.studentComments);
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
                if (StudentCommentsAll.Count > 0)
                {
                    studentCommentsList.studentCommentsList = StudentCommentsAll;
                    studentCommentsList._tenantName = studentCommentListViewModel._tenantName;
                    studentCommentsList._token = studentCommentListViewModel._token;
                    studentCommentsList._failure = false;
                }
                else
                {
                    studentCommentsList.studentCommentsList = null;
                    studentCommentsList._tenantName = studentCommentListViewModel._tenantName;
                    studentCommentsList._token = studentCommentListViewModel._token;
                    studentCommentsList._failure = true;
                    studentCommentsList._message = NORECORDFOUND;
                }
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
                    studentEnrollment.CalenderId = studentEnrollmentListModel.CalenderId;
                    studentEnrollment.RollingOption = studentEnrollmentListModel.RollingOption;
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
        /// <summary>
        /// Update Student Enrollment
        /// </summary>
        /// <param name="studentEnrollmentListModel"></param>
        /// <returns></returns>
        public StudentEnrollmentListModel UpdateStudentEnrollment(StudentEnrollmentListModel studentEnrollmentListModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    int? EnrollmentId = 1;
                    //EnrollmentId = Utility.GetMaxPK(this.context, new Func<StudentEnrollment, int>(x => x.EnrollmentId));

                    var studentEnrollmentData = this.context?.StudentEnrollment.Where(x=>x.StudentGuid==studentEnrollmentListModel.StudentGuid).OrderByDescending(x => x.EnrollmentId).FirstOrDefault();

                    if (studentEnrollmentData != null)
                    {
                        EnrollmentId = studentEnrollmentData.EnrollmentId + 1;
                    }

                    foreach (var studentEnrollmentList in studentEnrollmentListModel.studentEnrollments)
                    {
                        //Update Existing Enrollment Data
                        if (studentEnrollmentList.EnrollmentId > 0) 
                        {
                            var studentEnrollmentUpdate = this.context?.StudentEnrollment.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.SchoolId && x.StudentId == studentEnrollmentList.StudentId && x.EnrollmentId == studentEnrollmentList.EnrollmentId);
                            if (studentEnrollmentUpdate != null)
                            {
                                StudentEnrollment studentEnrollment = new StudentEnrollment();
                                if (studentEnrollmentList.ExitCode != null)
                                {
                                    //This block for Roll Over,Drop (Transfer),Enroll (Transfer)
                                    var studentExitCode = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.SchoolId && x.EnrollmentCode.ToString() == studentEnrollmentList.ExitCode); //fetching enrollemnt code type 

                                    if (studentExitCode.Type.ToLower() == "Drop (Transfer)".ToLower())
                                    {     
                                        //This block for student drop(transfer) & enroll(transfer) new school

                                        //update student's existing enrollment details 
                                        studentEnrollmentUpdate.ExitCode = studentExitCode.Title;
                                        studentEnrollmentUpdate.ExitDate = studentEnrollmentList.ExitDate;
                                        studentEnrollmentUpdate.TransferredGrade = studentEnrollmentList.TransferredGrade;
                                        studentEnrollmentUpdate.TransferredSchoolId = studentEnrollmentList.TransferredSchoolId;
                                        studentEnrollmentUpdate.SchoolTransferred = studentEnrollmentList.SchoolTransferred;
                                        studentEnrollmentUpdate.LastUpdated = DateTime.UtcNow;
                                        studentEnrollmentUpdate.UpdatedBy = studentEnrollmentList.UpdatedBy;

                                        //fetching enrollment code where student enroll(transfer).
                                        var studentTransferIn = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.TransferredSchoolId && x.Type.ToLower() == "Enroll (Transfer)".ToLower());

                                        if(studentTransferIn != null)
                                        {
                                            //fetching student details from studentMaster table
                                            var studentData = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == studentEnrollmentListModel.TenantId && x.SchoolId == studentEnrollmentListModel.SchoolId && x.StudentId == studentEnrollmentListModel.StudentId);
                                            if (studentData != null)
                                            {
                                                //fetching all student's active details from studentMaster table
                                                var otherSchoolEnrollment= this.context?.StudentMaster.Where(x => x.TenantId == studentData.TenantId  && x.StudentGuid == studentData.StudentGuid).ToList();
                                                if(otherSchoolEnrollment.Count > 0)
                                                {
                                                    foreach(var enrollmentData in otherSchoolEnrollment)
                                                    {
                                                        //this loop for update student's IsActive details and make it false for previos school
                                                        enrollmentData.IsActive = false;
                                                        this.context?.SaveChanges();
                                                    }
                                                }
                                                
                                                //generate StudentId where student enroll(Transfer) & save data
                                                int? MasterStudentId = 0;

                                                var studentDataForTransferredSchool = this.context?.StudentMaster.Where(x => x.SchoolId == studentEnrollmentList.TransferredSchoolId && x.TenantId == studentEnrollmentList.TenantId).OrderByDescending(x => x.StudentId).FirstOrDefault();

                                                if (studentDataForTransferredSchool != null)
                                                {
                                                    MasterStudentId = studentDataForTransferredSchool.StudentId + 1;
                                                }
                                                else
                                                {
                                                    MasterStudentId = 1;
                                                }
                                                
                                                studentData.SchoolId = (int)studentEnrollmentList.TransferredSchoolId;
                                                studentData.StudentId = (int)MasterStudentId;
                                                studentData.EnrollmentType = "Internal";
                                                studentData.IsActive = true;
                                                studentData.LastUpdated = DateTime.UtcNow;
                                                this.context?.StudentMaster.Add(studentData);

                                                //Student Protal Access
                                                if (studentData.StudentPortalId != null)
                                                {
                                                    var userMasterData = this.context?.UserMaster.FirstOrDefault(x => x.EmailAddress == studentData.StudentPortalId && x.TenantId == studentData.TenantId);
                                                    if (userMasterData != null)
                                                    {
                                                        userMasterData.IsActive = false;
                                                        UserMaster userMaster = new UserMaster();
                                                        userMaster.TenantId = studentData.TenantId;
                                                        userMaster.SchoolId = (int)studentEnrollmentList.TransferredSchoolId;
                                                        userMaster.UserId = (int)MasterStudentId;
                                                        userMaster.Name = userMasterData.Name;
                                                        userMaster.EmailAddress = userMasterData.EmailAddress;
                                                        userMaster.PasswordHash = userMasterData.PasswordHash;
                                                        userMaster.LangId = userMasterData.LangId;
                                                        var membershipsId = this.context?.Membership.Where(x => x.SchoolId == (int)studentEnrollmentList.TransferredSchoolId && x.TenantId == studentEnrollmentList.TenantId && x.Title == "Student").Select(x => x.MembershipId).FirstOrDefault();
                                                        userMaster.MembershipId = (int)membershipsId;
                                                        userMaster.LastUpdated = DateTime.UtcNow;
                                                        userMaster.UpdatedBy = studentEnrollmentList.UpdatedBy;
                                                        userMaster.IsActive = true;
                                                        this.context?.UserMaster.Add(userMaster);
                                                    }
                                                }
                                                this.context?.SaveChanges();

                                                //fetch default calender for enroll(transfer) school and save details in StudentEnrollment table.
                                                int? calenderId = null;

                                                var defaultCalender = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.TransferredSchoolId && x.AcademicYear == studentEnrollmentListModel.AcademicYear && x.DefaultCalender == true);

                                                if (defaultCalender != null)
                                                {
                                                    calenderId = defaultCalender.CalenderId;
                                                }

                                                studentEnrollmentList.TenantId = studentEnrollmentList.TenantId;
                                                studentEnrollmentList.SchoolId = (int)studentEnrollmentList.TransferredSchoolId;
                                                studentEnrollmentList.StudentId = (int)MasterStudentId;
                                                studentEnrollmentList.EnrollmentId = (int)EnrollmentId;
                                                studentEnrollmentList.EnrollmentDate = studentEnrollmentList.EnrollmentDate;
                                                studentEnrollmentList.EnrollmentCode = studentTransferIn.Title;
                                                studentEnrollmentList.ExitCode = null;
                                                studentEnrollmentList.ExitDate = null;
                                                studentEnrollmentList.SchoolName = studentEnrollmentList.SchoolTransferred;
                                                studentEnrollmentList.SchoolTransferred = null;
                                                studentEnrollmentList.TransferredSchoolId = null;
                                                studentEnrollmentList.GradeLevelTitle = studentEnrollmentList.TransferredGrade;
                                                studentEnrollmentList.TransferredGrade = null;
                                                studentEnrollmentList.CalenderId = calenderId;
                                                studentEnrollmentList.RollingOption = studentEnrollmentListModel.RollingOption;
                                                studentEnrollmentList.LastUpdated = DateTime.UtcNow;
                                                this.context?.StudentEnrollment.AddRange(studentEnrollmentList);
                                                EnrollmentId++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //This block save data for student's Roll over or Drop in same grade details
                                        studentEnrollmentUpdate.ExitCode = studentExitCode.Title;
                                        studentEnrollmentUpdate.ExitDate = studentEnrollmentList.ExitDate;
                                        studentEnrollmentUpdate.TransferredGrade = studentEnrollmentList.GradeLevelTitle;                                
                                        studentEnrollmentUpdate.LastUpdated = DateTime.UtcNow;
                                        studentEnrollmentUpdate.UpdatedBy = studentEnrollmentList.UpdatedBy;

                                        studentEnrollment.EnrollmentId = (int)EnrollmentId;
                                        studentEnrollment.TenantId = studentEnrollmentList.TenantId;
                                        studentEnrollment.SchoolId = (int)studentEnrollmentList.SchoolId;
                                        studentEnrollment.StudentId = studentEnrollmentList.StudentId;
                                        studentEnrollment.CalenderId = studentEnrollmentListModel.CalenderId;
                                        studentEnrollment.SchoolName = studentEnrollmentList.SchoolName;
                                        studentEnrollment.EnrollmentDate = studentEnrollmentList.ExitDate;
                                        studentEnrollment.EnrollmentCode = studentExitCode.Title;
                                        studentEnrollment.GradeLevelTitle = studentEnrollmentList.GradeLevelTitle;
                                        studentEnrollment.RollingOption = studentEnrollmentListModel.RollingOption;
                                        studentEnrollment.UpdatedBy = studentEnrollmentList.UpdatedBy;
                                        studentEnrollment.LastUpdated = DateTime.UtcNow;
                                        studentEnrollment.StudentGuid = studentEnrollmentUpdate.StudentGuid;
                                        this.context?.StudentEnrollment.Add(studentEnrollment);
                                        EnrollmentId++;
                                    }                                  
                                }
                                else
                                {
                                    //This block for update existing enrollment details only
                                    var studentEnrollmentCode = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.SchoolId && x.EnrollmentCode.ToString() == studentEnrollmentList.EnrollmentCode);

                                    studentEnrollmentUpdate.EnrollmentCode = studentEnrollmentCode.Title;
                                    studentEnrollmentUpdate.EnrollmentDate = studentEnrollmentList.EnrollmentDate;
                                    studentEnrollmentUpdate.GradeLevelTitle = studentEnrollmentList.GradeLevelTitle;
                                    studentEnrollmentUpdate.RollingOption = studentEnrollmentListModel.RollingOption;
                                    studentEnrollmentUpdate.CalenderId = studentEnrollmentListModel.CalenderId;
                                    studentEnrollmentUpdate.LastUpdated = DateTime.UtcNow;
                                    studentEnrollmentUpdate.UpdatedBy = studentEnrollmentList.UpdatedBy;
                                }
                            }
                        }
                        else
                        {
                            //This block for student new enrollment in another school as external school
                            var studentData = this.context?.StudentMaster.FirstOrDefault(x => x.TenantId == studentEnrollmentListModel.TenantId && x.SchoolId == studentEnrollmentListModel.SchoolId && x.StudentId == studentEnrollmentListModel.StudentId);
                            if (studentData != null)
                            {
                                int? MasterStudentId = 0;

                                var studentDataForNewSchool = this.context?.StudentMaster.Where(x => x.SchoolId == studentEnrollmentList.SchoolId && x.TenantId == studentEnrollmentList.TenantId).OrderByDescending(x => x.StudentId).FirstOrDefault();

                                if (studentDataForNewSchool != null)
                                {
                                    MasterStudentId = studentDataForNewSchool.StudentId + 1;
                                }
                                else
                                {
                                    MasterStudentId = 1;
                                }
                              
                                studentData.SchoolId = studentEnrollmentList.SchoolId;
                                studentData.StudentId = (int)MasterStudentId;
                                studentData.EnrollmentType = "External";
                                studentData.IsActive = true;
                                studentData.LastUpdated = DateTime.UtcNow;
                                this.context?.StudentMaster.Add(studentData);
                                this.context?.SaveChanges();                               

                                var studentEnrollmentCode = this.context?.StudentEnrollmentCode.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.SchoolId && x.EnrollmentCode.ToString() == studentEnrollmentList.EnrollmentCode);

                                int? calenderId = null;

                                var defaultCalender = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == studentEnrollmentList.TenantId && x.SchoolId == studentEnrollmentList.SchoolId && x.AcademicYear == studentEnrollmentListModel.AcademicYear && x.DefaultCalender == true);

                                if (defaultCalender != null)
                                {
                                    calenderId = defaultCalender.CalenderId;
                                }

                                studentEnrollmentList.TenantId = studentEnrollmentList.TenantId;
                                studentEnrollmentList.SchoolId = studentEnrollmentList.SchoolId;
                                studentEnrollmentList.StudentId = (int)MasterStudentId;
                                studentEnrollmentList.EnrollmentId = (int)EnrollmentId;
                                studentEnrollmentList.EnrollmentDate = studentEnrollmentList.EnrollmentDate;
                                studentEnrollmentList.EnrollmentCode = studentEnrollmentCode.Title;                               
                                studentEnrollmentList.CalenderId = calenderId;
                                studentEnrollmentList.RollingOption = studentEnrollmentListModel.RollingOption;
                                studentEnrollmentList.LastUpdated = DateTime.UtcNow;
                                this.context?.StudentEnrollment.AddRange(studentEnrollmentList);
                                EnrollmentId++;
                            }
                        }
                    }
                    this.context?.SaveChanges();
                    transaction.Commit();
                    studentEnrollmentListModel._failure = false;
                }
                catch (Exception es)
                {
                    transaction.Rollback();
                    studentEnrollmentListModel._failure = true;
                    studentEnrollmentListModel._message = es.Message;
                }
                return studentEnrollmentListModel;
            }
        }
        /// <summary>
        /// Get All Student Enrollment
        /// </summary>
        /// <param name="studentEnrollmentListViewModel"></param>
        /// <returns></returns>
        public StudentEnrollmentListViewModel GetAllStudentEnrollment(StudentEnrollmentListViewModel studentEnrollmentListViewModel)
        {
            StudentEnrollmentListViewModel studentEnrollmentListView = new StudentEnrollmentListViewModel();
            try
            {
                //fetch default calender id
                int? calenderId = null;                

                var defaultCalender = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == studentEnrollmentListViewModel.TenantId && x.SchoolId == studentEnrollmentListViewModel.SchoolId && x.AcademicYear.ToString() == studentEnrollmentListViewModel.AcademicYear && x.DefaultCalender == true);

                if (defaultCalender != null)
                {
                    calenderId = defaultCalender.CalenderId;
                }

                var studentEnrollmentList = this.context?.StudentEnrollment.Where(x => x.TenantId == studentEnrollmentListViewModel.TenantId && x.StudentGuid == studentEnrollmentListViewModel.StudentGuid).OrderByDescending(x => x.EnrollmentId).ToList();

                if(studentEnrollmentList.Count>0)
                {
                    var studentEnrollment = studentEnrollmentList.Select(y => new StudentEnrollmentListForView
                    {
                        TenantId = y.TenantId,
                        SchoolId = y.SchoolId,
                        StudentId = y.StudentId,
                        GradeLevelTitle = y.GradeLevelTitle,
                        RollingOption = y.RollingOption,
                        SchoolName = y.SchoolName,
                        LastUpdated = y.LastUpdated,
                        SchoolTransferred = y.SchoolTransferred,
                        TransferredGrade = y.TransferredGrade,
                        TransferredSchoolId = y.TransferredSchoolId,
                        UpdatedBy = y.UpdatedBy,
                        AcademicYear = this.context?.SchoolCalendars.FirstOrDefault(z => z.CalenderId == y.CalenderId)?.AcademicYear,
                        CalenderId = y.CalenderId,
                        EnrollmentCode = y.EnrollmentCode,
                        EnrollmentId = y.EnrollmentId,
                        EnrollmentDate = y.EnrollmentDate,
                        ExitCode = y.ExitCode,
                        ExitDate = y.ExitDate,
                        StudentGuid=y.StudentGuid,
                        EnrollmentType=this.context?.StudentEnrollmentCode.FirstOrDefault(s => s.TenantId==y.TenantId && s.SchoolId==y.SchoolId && s.Title == y.EnrollmentCode)?.Type,
                        ExitType = this.context?.StudentEnrollmentCode.FirstOrDefault(s => s.TenantId == y.TenantId && s.SchoolId == y.SchoolId && s.Title == y.ExitCode)?.Type
                    }).ToList();
                    studentEnrollmentListView.studentEnrollmentListForView = studentEnrollment;
                    studentEnrollmentListView.TenantId = studentEnrollmentListViewModel.TenantId;
                    studentEnrollmentListView.CalenderId = calenderId;
                    studentEnrollmentListView.AcademicYear = studentEnrollmentListViewModel.AcademicYear;
                    studentEnrollmentListView.RollingOption = "Next Grade at Current School";
                    studentEnrollmentListView.StudentId = studentEnrollmentListViewModel.StudentId;
                    studentEnrollmentListView._tenantName = studentEnrollmentListViewModel._tenantName;
                    studentEnrollmentListView._token = studentEnrollmentListViewModel._token;
                    studentEnrollmentListView._failure = false;
                }               
                 else
                {
                    studentEnrollmentListView.studentEnrollmentListForView = null;
                    studentEnrollmentListView._tenantName = studentEnrollmentListViewModel._tenantName;
                    studentEnrollmentListView._failure = true;
                    studentEnrollmentListView._message = NORECORDFOUND;
                }                
            }
            catch (Exception es)
            {
                studentEnrollmentListView._message = es.Message;
                studentEnrollmentListView._failure = true;
                studentEnrollmentListView._tenantName = studentEnrollmentListViewModel._tenantName;
                studentEnrollmentListView._token = studentEnrollmentListViewModel._token;
            }
            return studentEnrollmentListView;
        }
       

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

                    if (studentData.StudentPortalId != null)
                    {
                        var userMasterData = this.context?.UserMaster.FirstOrDefault(x => x.EmailAddress == studentData.StudentPortalId);

                        if (userMasterData != null)
                        {
                            studentView.LoginEmail = userMasterData.EmailAddress;
                            studentView.PortalAccess = userMasterData.IsActive;
                        }
                    }
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
                    studentView._tenantName = student._tenantName;
                    studentView._token = student._token;
                }
                else
                {
                    studentView._tenantName = student._tenantName;
                    studentView._token = student._token;
                    studentView._failure = true;
                    studentView._message = NORECORDFOUND;
                }
            }
            catch (Exception es)
            {
                studentView._tenantName = student._tenantName;
                studentView._token = student._token;
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
                var studentData = (from student in this.context?.StudentMaster
                                   join enrollment in this.context?.StudentEnrollment on student.StudentId equals enrollment.StudentId
                                   where student.SchoolId == enrollment.SchoolId && student.TenantId == enrollment.TenantId && enrollment.GradeLevelTitle != null
                                   select new
                                   {
                                       student.TenantId,
                                       student.SchoolId,
                                       student.StudentId,
                                       student.HomeAddressLineOne,
                                       student.HomeAddressLineTwo,
                                       student.HomeAddressCountry,
                                       student.HomeAddressState,
                                       student.HomeAddressCity,
                                       student.HomeAddressZip,
                                       student.StudentInternalId,
                                       student.FirstGivenName,
                                       student.MiddleName,
                                       student.LastFamilyName,
                                       student.Dob,
                                       enrollment.GradeLevelTitle
                                   });
                if (studentData != null && studentData.Count() > 0)
                {
                    var StudentSibling = studentData.Where(x => x.FirstGivenName == studentSiblingListViewModel.FirstGivenName && x.LastFamilyName == studentSiblingListViewModel.LastFamilyName && x.TenantId == studentSiblingListViewModel.TenantId && x.GradeLevelTitle == studentSiblingListViewModel.GradeLevelTitle && (studentSiblingListViewModel.SchoolId == null || (x.SchoolId == studentSiblingListViewModel.SchoolId)) && (studentSiblingListViewModel.Dob == null || (x.Dob == studentSiblingListViewModel.Dob)) && (studentSiblingListViewModel.StudentInternalId == null || (x.StudentInternalId.ToLower().Trim() == studentSiblingListViewModel.StudentInternalId.ToLower().Trim()))).ToList();
                    if (StudentSibling.Count > 0)
                    {
                        var siblingsOfStudent = StudentSibling.Select(s => new GetStudentForView
                        {
                            FirstGivenName = s.FirstGivenName,
                            LastFamilyName = s.LastFamilyName,
                            Dob = s.Dob,
                            StudentId = s.StudentId,
                            StudentInternalId = s.StudentInternalId,
                            SchoolId = s.SchoolId,
                            TenantId = s.TenantId,
                            SchoolName = this.context?.SchoolMaster.Where(x => x.SchoolId == s.SchoolId)?.Select(e => e.SchoolName).FirstOrDefault(),
                            Address = ToFullAddress(s.HomeAddressLineOne, s.HomeAddressLineTwo,
                        int.TryParse(s.HomeAddressCity, out resultData) == true ? this.context.City.Where(x => x.Id == Convert.ToInt32(s.HomeAddressCity)).FirstOrDefault().Name : s.HomeAddressCity,
                        int.TryParse(s.HomeAddressState, out resultData) == true ? this.context.State.Where(x => x.Id == Convert.ToInt32(s.HomeAddressState)).FirstOrDefault().Name : s.HomeAddressState,
                        int.TryParse(s.HomeAddressCountry, out resultData) == true ? this.context.Country.Where(x => x.Id == Convert.ToInt32(s.HomeAddressCountry)).FirstOrDefault().Name : string.Empty, s.HomeAddressZip),
                            GradeLevelTitle = s.GradeLevelTitle
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
                    var studentAssociateTo = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.studentMaster.StudentId && x.SchoolId == siblingAddUpdateForStudentModel.studentMaster.SchoolId);
                    var studentAssociateBy = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.StudentId && x.SchoolId == siblingAddUpdateForStudentModel.SchoolId);
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
                var StudentAssociateTo = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.studentMaster.StudentId && x.SchoolId == siblingAddUpdateForStudentModel.studentMaster.SchoolId);
                var StudentAssociateBy = this.context?.StudentMaster.FirstOrDefault(x => x.StudentId == siblingAddUpdateForStudentModel.StudentId && x.SchoolId == siblingAddUpdateForStudentModel.SchoolId);
                var StudentAssociateToDataDel = siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.studentMaster.SchoolId + "#" + siblingAddUpdateForStudentModel.studentMaster.StudentId;
                var StudentAssociateByDataDel = siblingAddUpdateForStudentModel.studentMaster.TenantId + "#" + siblingAddUpdateForStudentModel.SchoolId + "#" + siblingAddUpdateForStudentModel.StudentId;

                if (StudentAssociateTo != null && StudentAssociateTo.Associationship != null)
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

                if (StudentAssociateBy != null && StudentAssociateBy.Associationship != null)
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
                    siblingAddUpdateForStudentModel._message = "Associationship Remove Successfully";
                }
                this.context?.SaveChanges();
                
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
            var checkInternalId = this.context?.StudentMaster.Where(x => x.TenantId == checkStudentInternalIdViewModel.TenantId && x.SchoolId == checkStudentInternalIdViewModel.SchoolId && x.StudentInternalId == checkStudentInternalIdViewModel.StudentInternalId).ToList();
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



