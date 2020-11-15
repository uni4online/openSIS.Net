using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
   public partial class StudentMaster
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public string AlternateId { get; set; }
        public int? DistrictId { get; set; }
        public int? StateId { get; set; }
        public string AdmissionNumber { get; set; }
        public string RollNumber { get; set; }
        public string Salutation { get; set; }
        public string FirstGivenName { get; set; }
        public string MiddleName { get; set; }
        public string LastFamilyName { get; set; }
        public string Suffix { get; set; }
        public string PreferredName { get; set; }
        public string PreviousName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string OtherGovtIssuedNumber { get; set; }
        public byte[] StudentPhoto { get; set; }
        public DateTime? Dob { get; set; }
        public string StudentPortalId { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string MaritalStatus { get; set; }
        public int? CountryOfBirth { get; set; }
        public int? Nationality { get; set; }
        public int? FirstLanguageId { get; set; }
        public int? SecondLanguageId { get; set; }
        public int? ThirdLanguageId { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string PersonalEmail { get; set; }
        public string SchoolEmail { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Linkedin { get; set; }
        public string HomeAddressLineOne { get; set; }
        public string HomeAddressLineTwo { get; set; }
        public string HomeAddressCountry { get; set; }
        public string HomeAddressCity { get; set; }
        public string HomeAddressState { get; set; }
        public string HomeAddressZip { get; set; }
        public string BusNo { get; set; }
        public bool? SchoolBusPickUp { get; set; }
        public bool? SchoolBusDropOff { get; set; }
        public bool? MailingAddressSameToHome { get; set; }
        public string MailingAddressLineOne { get; set; }
        public string MailingAddressLineTwo { get; set; }
        public string MailingAddressCountry { get; set; }
        public string MailingAddressCity { get; set; }
        public string MailingAddressState { get; set; }
        public string MailingAddressZip { get; set; }
        public string PrimaryContactRelationship { get; set; }
        public string PrimaryContactFirstname { get; set; }
        public string PrimaryContactLastname { get; set; }
        public string PrimaryContactHomePhone { get; set; }
        public string PrimaryContactWorkPhone { get; set; }
        public string PrimaryContactMobile { get; set; }
        public string PrimaryContactEmail { get; set; }
        public bool? IsPrimaryCustodian { get; set; }
        public bool? IsPrimaryPortalUser { get; set; }
        public string PrimaryPortalUserId { get; set; }
        public bool? PrimaryContactStudentAddressSame { get; set; }
        public string PrimaryContactAddressLineOne { get; set; }
        public string PrimaryContactAddressLineTwo { get; set; }
        public string PrimaryContactCountry { get; set; }
        public string PrimaryContactCity { get; set; }
        public string PrimaryContactState { get; set; }
        public string PrimaryContactZip { get; set; }
        public string SecondaryContactRelationship { get; set; }
        public string SecondaryContactFirstname { get; set; }
        public string SecondaryContactLastname { get; set; }
        public string SecondaryContactHomePhone { get; set; }
        public string SecondaryContactWorkPhone { get; set; }
        public string SecondaryContactMobile { get; set; }
        public string SecondaryContactEmail { get; set; }
        public bool? IsSecondaryCustodian { get; set; }
        public bool? IsSecondaryPortalUser { get; set; }
        public string SecondaryPortalUserId { get; set; }
        public bool? SecondaryContactStudentAddressSame { get; set; }
        public string SecondaryContactAddressLineOne { get; set; }
        public string SecondaryContactAddressLineTwo { get; set; }
        public string SecondaryContactCountry { get; set; }
        public string SecondaryContactCity { get; set; }
        public string SecondaryContactState { get; set; }
        public string SecondaryContactZip { get; set; }
        public virtual Language FirstLanguage { get; set; }
        public virtual SchoolMaster SchoolMaster { get; set; }
        public virtual Language SecondLanguage { get; set; }
        public virtual Language ThirdLanguage { get; set; }
        public virtual StudentEnrollment StudentEnrollment { get; set; }
    }
}
