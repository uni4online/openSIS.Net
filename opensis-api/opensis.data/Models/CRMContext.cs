using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public class CRMContext : DbContext
    {
        private DbContextOptions contextOptions;

        public CRMContext(): base()
        {
        }

        public CRMContext(DbContextOptions options) : base(options)
        {
            this.contextOptions = options;
        }

        public DbSet<Schools> tblSchool { get; set; }
        public DbSet<Users> tblUser { get; set; }

        public virtual DbSet<TableCity> TableCity { get; set; }
        public virtual DbSet<TableCountry> TableCountry { get; set; }
        public virtual DbSet<TableLanguage> TableLanguage { get; set; }
        public virtual DbSet<TableMembership> TableMembership { get; set; }
        public virtual DbSet<TableNotice> TableNotice { get; set; }
        public virtual DbSet<TablePlans> TablePlans { get; set; }
        public virtual DbSet<TableQuarters> TableQuarters { get; set; }
        public virtual DbSet<TableSchoolCalendars> TableSchoolCalendars { get; set; }
        public virtual DbSet<TableSchoolDetail> TableSchoolDetail { get; set; }
        public virtual DbSet<TableSchoolMaster> TableSchoolMaster { get; set; }
        public virtual DbSet<TableSchoolPeriods> TableSchoolPeriods { get; set; }
        public virtual DbSet<TableSchoolYears> TableSchoolYears { get; set; }
        public virtual DbSet<TableSemesters> TableSemesters { get; set; }
        public virtual DbSet<TableState> TableState { get; set; }
        public virtual DbSet<TableUserMaster> TableUserMaster { get; set; }
        public virtual DbSet<TableRooms> TableRooms { get; set; }
        public virtual DbSet<TableSections> TableSections { get; set; }
        public virtual DbSet<TableGradelevels> TableGradelevels { get; set; }
        public virtual DbSet<TableProgressPeriods> TableProgressPeriods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schools>().HasKey(e => e.school_id);
            
            modelBuilder.Entity<TableSections>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.SectionId });

                entity.ToTable("Table_Sections");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.SectionId).HasColumnName("Section_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableCity>(entity =>
            {
                entity.ToTable("Table_City");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");
            });

            modelBuilder.Entity<TableCountry>(entity =>
            {
                entity.ToTable("Table_Country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TableLanguage>(entity =>
            {
                entity.HasKey(e => e.LangId);

                entity.ToTable("Table_Language");

                entity.Property(e => e.LangId)
                    .HasColumnName("Lang_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LanguageCode)
                    .HasColumnName("Language_Code")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Lcid)
                    .HasColumnName("LCID")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Locale)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TableMembership>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MembershipId })
                    .HasName("PK_Table_membership_1");

                entity.ToTable("Table_membership");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.MembershipId)
                    .HasColumnName("Membership_id")
                    .HasComment("can be considered as profileid of Opensis1");

                entity.Property(e => e.Access)
                    .HasColumnName("access")
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("Last_Updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Profile)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("E.g. admin,student,teacher");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("e.g. Administrator,Student,Teacher,Dept. head");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("Updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WeeklyUpdate)
                    .HasColumnName("weekly_update")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableMembership)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_membership_Table_School_Master");
            });
            modelBuilder.Entity<TableProgressPeriods>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId, e.AcademicYear, e.QuarterId });

                entity.ToTable("Table_Progress_periods");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("Marking_period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("Academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.QuarterId).HasColumnName("Quarter_id");

                entity.Property(e => e.DoesComments).HasColumnName("does_comments");

                entity.Property(e => e.DoesExam).HasColumnName("does_exam");

                entity.Property(e => e.DoesGrades).HasColumnName("does_grades");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostEndDate)
                    .HasColumnName("post_end_date")
                    .HasColumnType("date");

                entity.Property(e => e.PostStartDate)
                    .HasColumnName("post_start_date")
                    .HasColumnType("date");

                entity.Property(e => e.RolloverId).HasColumnName("rollover_id");

                entity.Property(e => e.ShortName)
                    .HasColumnName("Short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TableRooms>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.RoomId });

                entity.ToTable("Table_Rooms");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TableGradelevels>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.GradeId });

                entity.ToTable("Table_Gradelevels");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.GradeId).HasColumnName("Grade_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.NextGradeId).HasColumnName("next_grade_id");

                entity.Property(e => e.ShortName)
                    .HasColumnName("Short_name")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableNotice>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.NoticeId });

                entity.ToTable("Table_Notice");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.Property(e => e.NoticeId).HasColumnName("Notice_Id");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("Created_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.TargetMembershipIds)
                    .IsRequired()
                    .HasColumnName("Target_Membership_ids")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Signifies group of user for whom notice is visible. to be saved as comma separated values. if user's membership_id falls in any of the value, he can see the notice.");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom)
                    .HasColumnName("valid_from")
                    .HasColumnType("date");

                entity.Property(e => e.ValidTo)
                    .HasColumnName("valid_to")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TablePlans>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.PlanId });

                entity.ToTable("Table_Plans");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.PlanId).HasColumnName("Plan_id");

                entity.Property(e => e.Features).HasColumnName("features");

                entity.Property(e => e.MaxApiChecks).HasColumnName("max_api_checks");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableQuarters>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId });

                entity.ToTable("Table_Quarters");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("Marking_Period_Id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("Academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.DoesComments).HasColumnName("does_comments");

                entity.Property(e => e.DoesExam).HasColumnName("does_exam");

                entity.Property(e => e.DoesGrades).HasColumnName("does_grades");

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .HasColumnType("date");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostEndDate)
                    .HasColumnName("Post_End_Date")
                    .HasColumnType("date");

                entity.Property(e => e.PostStartDate)
                    .HasColumnName("Post_Start_Date")
                    .HasColumnType("date");

                entity.Property(e => e.RolloverId)
                    .HasColumnName("rollover_id")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.SemesterId)
                    .HasColumnName("Semester_id")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.ShortName)
                    .HasColumnName("Short_Name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("Sort_Order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableQuarters)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Quarters_Table_School_Master");
            });

            modelBuilder.Entity<TableSchoolCalendars>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.CalenderId });

                entity.ToTable("Table_School_Calendars");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.CalenderId).HasColumnName("Calender_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("Academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.Days)
                    .HasColumnName("days")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultCalender)
                    .HasColumnName("default_calender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.RolloverId)
                    .HasColumnName("rollover_id")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableSchoolCalendars)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_School_Calendars_Table_School_Master");
            });

            modelBuilder.Entity<TableSchoolDetail>(entity =>
            {
                entity.ToTable("Table_School_Detail");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Affiliation)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Associations)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.CommonToiletAccessibility)
                    .HasColumnName("Common_Toilet_Accessibility")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.ComonToiletType)
                    .HasColumnName("Comon_Toilet_Type")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.CurrentlyAvailable).HasColumnName("Currently_Available");

                entity.Property(e => e.DateSchoolClosed)
                    .HasColumnName("Date_School_Closed")
                    .HasColumnType("date");

                entity.Property(e => e.DateSchoolOpened)
                    .HasColumnName("Date_School_Opened")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.FemaleToiletAccessibility)
                    .HasColumnName("Female_Toilet_Accessibility")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.FemaleToiletType)
                    .HasColumnName("Female_Toilet_Type")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.HandwashingAvailable).HasColumnName("Handwashing_Available");

                entity.Property(e => e.HighestGradeLevel)
                    .HasColumnName("Highest_Grade_Level")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.HygeneEducation)
                    .HasColumnName("Hygene_Education")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Instagram)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.LinkedIn)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Locale)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.LowestGradeLevel)
                    .HasColumnName("Lowest_Grade_Level")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.MainSourceOfDrinkingWater)
                    .HasColumnName("Main_Source_of_Drinking_Water")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.MaleToiletAccessibility)
                    .HasColumnName("Male_Toilet_Accessibility")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.MaleToiletType)
                    .HasColumnName("Male_Toilet_Type")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.NameOfAssistantPrincipal)
                    .HasColumnName("Name_of_Assistant_Principal")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.NameOfPrincipal)
                    .HasColumnName("Name_of_Principal")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.RunningWater).HasColumnName("Running_Water");

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.Property(e => e.SchoolLogo)
                    .HasColumnName("School_Logo");
               

                entity.Property(e => e.SoapAndWaterAvailable).HasColumnName("Soap_and_Water_Available");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

                entity.Property(e => e.TotalCommonToilets).HasColumnName("Total_Common_Toilets");

                entity.Property(e => e.TotalCommonToiletsUsable).HasColumnName("Total_Common_Toilets_Usable");

                entity.Property(e => e.TotalFemaleToilets).HasColumnName("Total_Female_Toilets");

                entity.Property(e => e.TotalFemaleToiletsUsable).HasColumnName("Total_Female_Toilets_Usable");

                entity.Property(e => e.TotalMaleToilets).HasColumnName("Total_Male_Toilets");

                entity.Property(e => e.TotalMaleToiletsUsable).HasColumnName("Total_Male_Toilets_Usable");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.Youtube)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableSchoolDetail)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .HasConstraintName("FK_Table_School_Detail_Table_School_Master");
            });

            modelBuilder.Entity<TableSchoolMaster>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId });

                entity.ToTable("Table_School_Master");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.Property(e => e.AlternateName)
                    .HasColumnName("Alternate_Name")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.County)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CurrentPeriodEnds)
                    .HasColumnName("Current_Period_ends")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("Date_Created")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateModifed)
                    .HasColumnName("Date_Modifed")
                    .HasColumnType("datetime");

                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Division)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Features).IsUnicode(false);

                entity.Property(e => e.MaxApiChecks).HasColumnName("Max_api_checks");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("Modified_By")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlanId).HasColumnName("Plan_id");

                entity.Property(e => e.SchoolAltId)
                    .HasColumnName("School_Alt_Id")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SchoolClassification)
                    .HasColumnName("School_Classification")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SchoolDistrictId)
                    .HasColumnName("School_District_Id")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SchoolInternalId)
                    .HasColumnName("School_Internal_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolLevel)
                    .HasColumnName("School_Level")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SchoolName)
                    .HasColumnName("School_Name")
                    .HasMaxLength(100);

                entity.Property(e => e.SchoolStateId)
                    .HasColumnName("School_State_Id")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress1)
                    .HasColumnName("Street_Address_1")
                    .HasMaxLength(150);

                entity.Property(e => e.StreetAddress2)
                    .HasColumnName("Street_Address_2")
                    .HasMaxLength(150);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.TablePlans)
                    .WithMany(p => p.TableSchoolMaster)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.PlanId })
                    .HasConstraintName("FK_Table_School_Master_Table_Plans");
            });

            modelBuilder.Entity<TableSchoolPeriods>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.PeriodId });

                entity.ToTable("Table_School_Periods");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.PeriodId).HasColumnName("period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.Attendance)
                    .HasColumnName("attendance")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Block)
                    .HasColumnName("block")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.IgnoreScheduling)
                    .HasColumnName("ignore_scheduling")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.RolloverId)
                    .HasColumnName("rollover_id")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableSchoolPeriods)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_School_Periods_Table_School_Master");
            });

            modelBuilder.Entity<TableSchoolYears>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId });

                entity.ToTable("Table_School_Years");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("Marking_Period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("Academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.DoesComments).HasColumnName("does_comments");

                entity.Property(e => e.DoesExam).HasColumnName("does_exam");

                entity.Property(e => e.DoesGrades).HasColumnName("does_grades");

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .HasColumnType("date");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostEndDate)
                    .HasColumnName("Post_end_Date")
                    .HasColumnType("date");

                entity.Property(e => e.PostStartDate)
                    .HasColumnName("Post_Start_Date")
                    .HasColumnType("date");

                entity.Property(e => e.RolloverId)
                    .HasColumnName("rollover_id")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.ShortName)
                    .HasColumnName("Short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("Sort_order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableSchoolYears)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_School_Years_Table_School_Master");
            });

            modelBuilder.Entity<TableSemesters>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId });

                entity.ToTable("Table_Semesters");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("Marking_Period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("Academic_Year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.DoesComments).HasColumnName("does_comments");

                entity.Property(e => e.DoesExam).HasColumnName("does_exam");

                entity.Property(e => e.DoesGrades).HasColumnName("does_grades");

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostEndDate)
                    .HasColumnName("Post_end_date")
                    .HasColumnType("date");

                entity.Property(e => e.PostStartDate)
                    .HasColumnName("Post_start_date")
                    .HasColumnType("date");

                entity.Property(e => e.RolloverId)
                    .HasColumnName("rollover_id")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.ShortName)
                    .HasColumnName("Short_Name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("Sort_order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.YearId)
                    .HasColumnName("Year_id")
                    .HasColumnType("decimal(10, 0)");

                entity.HasOne(d => d.TableSchoolMaster)
                    .WithMany(p => p.TableSemesters)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Semesters_Table_School_Master");
            });

            modelBuilder.Entity<TableState>(entity =>
            {
                entity.ToTable("Table_State");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TableUserMaster>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.UserId })
                    .HasName("PK_Table_User_Master_1");

                entity.ToTable("Table_User_Master");

                entity.Property(e => e.TenantId).HasColumnName("Tenant_Id");

                entity.Property(e => e.SchoolId).HasColumnName("School_id");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LangId)
                    .HasColumnName("lang_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.MembershipId).HasColumnName("Membership_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.TableUserMaster)
                    .HasForeignKey(d => d.LangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_User_Master_Table_Language");

                entity.HasOne(d => d.TableMembership)
                    .WithMany(p => p.TableUserMaster)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.MembershipId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_User_Master_Table_membership1");
            });
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string[] tenants = new string[] { "TenantA" };
                string connectionString = "Server=DESKTOP-OS2L82E\\SQLEXPRESS2019;Database={tenant};User Id=sa; Password=admin@123;MultipleActiveResultSets=true";
                optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"));

                //foreach (string tenant in tenants)
                //{
                //    optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"));
                //}
            }
        }
    }
}
