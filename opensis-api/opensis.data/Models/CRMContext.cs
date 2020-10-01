using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public class CRMContext : DbContext
    {
        private DbContextOptions contextOptions;


        public CRMContext() : base()
        {
        }

        public CRMContext(DbContextOptions options) : base(options)
        {
            this.contextOptions = options;
        }



        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Gradelevels> Gradelevels { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<ProgressPeriods> ProgressPeriods { get; set; }
        public virtual DbSet<Quarters> Quarters { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<SchoolCalendars> SchoolCalendars { get; set; }
        public virtual DbSet<SchoolDetail> SchoolDetail { get; set; }
        public virtual DbSet<SchoolMaster> SchoolMaster { get; set; }
        public virtual DbSet<SchoolPeriods> SchoolPeriods { get; set; }
        public virtual DbSet<SchoolYears> SchoolYears { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<Semesters> Semesters { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("stateid");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryCode)
                    .HasColumnName("countrycode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);
            });

            modelBuilder.Entity<Gradelevels>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.GradeId })
                    .HasName("PK_Table_Gradelevels");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.GradeId).HasColumnName("grade_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.NextGradeId).HasColumnName("next_grade_id");

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LangId)
                    .HasName("PK_Table_Language");

                entity.Property(e => e.LangId)
                    .HasColumnName("lang_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LanguageCode)
                    .HasColumnName("language_code")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Lcid)
                    .HasColumnName("lcid")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MembershipId })
                    .HasName("PK_Table_membership_1");

                entity.ToTable("membership");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.MembershipId)
                    .HasColumnName("membership_id")
                    .HasComment("can be considered as profileid of Opensis1");

                entity.Property(e => e.Access)
                    .HasColumnName("access")
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Profile)
                    .HasColumnName("profile")
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("E.g. admin,student,teacher");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("e.g. Administrator,Student,Teacher,Dept. head");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WeeklyUpdate)
                    .HasColumnName("weekly_update")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.Membership)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_membership_Table_School_Master");
            });

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.NoticeId })
                    .HasName("PK_Table_Notice");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.NoticeId).HasColumnName("notice_id");

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.TargetMembershipIds)
                    .IsRequired()
                    .HasColumnName("target_membership_ids")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Signifies group of user for whom notice is visible. to be saved as comma separated values. if user's membership_id falls in any of the value, he can see the notice.");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom)
                    .HasColumnName("valid_from")
                    .HasColumnType("date");

                entity.Property(e => e.ValidTo)
                    .HasColumnName("valid_to")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Plans>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.PlanId })
                    .HasName("PK_Table_Plans");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.Features).HasColumnName("features");

                entity.Property(e => e.MaxApiChecks).HasColumnName("max_api_checks");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProgressPeriods>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId, e.AcademicYear, e.QuarterId })
                    .HasName("PK_Table_Progress_periods");

                entity.ToTable("progress_periods");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("marking_period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.QuarterId).HasColumnName("quarter_id");

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
                    .HasColumnName("short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Quarters)
                    .WithMany(p => p.ProgressPeriods)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.QuarterId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Progress_periods_Table_Quarters");
            });

            modelBuilder.Entity<Quarters>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId })
                    .HasName("PK_Table_Quarters");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("marking_period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

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

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.Quarters)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Quarters_Table_School_Master");

                entity.HasOne(d => d.Semesters)
                    .WithMany(p => p.Quarters)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.SemesterId })
                    .HasConstraintName("FK_Table_Quarters_Table_Semesters");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.RoomId })
                    .HasName("PK_Table_Rooms");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

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

            modelBuilder.Entity<SchoolCalendars>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.CalenderId })
                    .HasName("PK_Table_School_Calendars");

                entity.ToTable("school_calendars");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.CalenderId).HasColumnName("calender_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.Days)
                    .HasColumnName("days")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultCalender).HasColumnName("default_calender");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.RolloverId).HasColumnName("rollover_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.SchoolCalendars)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_School_Calendars_Table_School_Master");
            });

            modelBuilder.Entity<SchoolDetail>(entity =>
            {
                entity.ToTable("school_detail");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Affiliation)
                    .HasColumnName("affiliation")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Associations)
                .HasColumnName("associations")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.CommonToiletAccessibility)
                    .HasColumnName("common_toilet_accessibility")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.ComonToiletType)
                    .HasColumnName("comon_toilet_type")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.CurrentlyAvailable).HasColumnName("currently_available");

                entity.Property(e => e.DateSchoolClosed)
                    .HasColumnName("date_school_closed")
                    .HasColumnType("date");

                entity.Property(e => e.DateSchoolOpened)
                    .HasColumnName("date_school_opened")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Facebook)
                .HasColumnName("facebook")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Fax)
                .HasColumnName("fax")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.FemaleToiletAccessibility)
                    .HasColumnName("female_toilet_accessibility")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.FemaleToiletType)
                    .HasColumnName("female_toilet_type")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender)
                .HasColumnName("gender")
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.HandwashingAvailable).HasColumnName("handwashing_available");

                entity.Property(e => e.HighestGradeLevel)
                    .HasColumnName("highest_grade_level")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.HygeneEducation)
                    .HasColumnName("hygene_education")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Instagram)
                .HasColumnName("instagram")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.LinkedIn)
                .HasColumnName("linkedin")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Locale)
                .HasColumnName("locale")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.LowestGradeLevel)
                    .HasColumnName("lowestgradelevel")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.MainSourceOfDrinkingWater)
                    .HasColumnName("main_source_of_drinking_water")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.MaleToiletAccessibility)
                    .HasColumnName("male_toilet_accessibility")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.MaleToiletType)
                    .HasColumnName("male_toilet_type")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.NameOfAssistantPrincipal)
                    .HasColumnName("name_of_assistant_principal")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.NameOfPrincipal)
                    .HasColumnName("name_of_principal")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.RunningWater).HasColumnName("running_water");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.SchoolLogo)
                    .HasColumnName("school_logo")
                    .HasMaxLength(50);

                entity.Property(e => e.SoapAndWaterAvailable).HasColumnName("soap_and_water_available");

                entity.Property(e => e.Telephone)
                .HasColumnName("telephone")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.TotalCommonToilets).HasColumnName("total_common_toilets");

                entity.Property(e => e.TotalCommonToiletsUsable).HasColumnName("total_common_toilets_usable");

                entity.Property(e => e.TotalFemaleToilets).HasColumnName("total_female_toilets");

                entity.Property(e => e.TotalFemaleToiletsUsable).HasColumnName("total_female_toilets_usable");

                entity.Property(e => e.TotalMaleToilets).HasColumnName("total_male_toilets");

                entity.Property(e => e.TotalMaleToiletsUsable).HasColumnName("total_male_toilets_usable");

                entity.Property(e => e.Twitter)
                .HasColumnName("twitter")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Website)
                .HasColumnName("website")
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.Youtube)
                .HasColumnName("youtube")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.SchoolDetail)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .HasConstraintName("FK_Table_School_Detail_Table_School_Master");
            });

            modelBuilder.Entity<SchoolMaster>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId })
                    .HasName("PK_Table_School_Master");

                entity.ToTable("school_master");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.AlternateName)
                    .HasColumnName("alternate_name")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Country)
                .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.County)
                .HasColumnName("county")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CurrentPeriodEnds)
                    .HasColumnName("current_period_ends")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateModifed)
                    .HasColumnName("date_modifed")
                    .HasColumnType("datetime");

                entity.Property(e => e.District)
                .HasColumnName("district")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Division)
                .HasColumnName("division")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Features).HasColumnName("features").IsUnicode(false);

                entity.Property(e => e.MaxApiChecks).HasColumnName("max_api_checks");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.SchoolAltId)
                    .HasColumnName("school_alt_id")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SchoolClassification)
                    .HasColumnName("school_classification")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SchoolDistrictId)
                    .HasColumnName("school_district_id")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SchoolInternalId)
                    .HasColumnName("school_internal_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolLevel)
                    .HasColumnName("school_level")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SchoolName)
                    .HasColumnName("school_name")
                    .HasMaxLength(100);

                entity.Property(e => e.SchoolStateId)
                    .HasColumnName("school_state_id")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.State)
                .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress1)
                    .HasColumnName("street_address_1")
                    .HasMaxLength(150);

                entity.Property(e => e.StreetAddress2)
                    .HasColumnName("street_address_2")
                    .HasMaxLength(150);

                entity.Property(e => e.Zip)
                .HasColumnName("zip")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Plans)
                    .WithMany(p => p.SchoolMaster)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.PlanId })
                    .HasConstraintName("FK_Table_School_Master_Table_Plans");
            });

            modelBuilder.Entity<SchoolPeriods>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.PeriodId })
                    .HasName("PK_Table_School_Periods");

                entity.ToTable("school_periods");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.PeriodId).HasColumnName("period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.Attendance).HasColumnName("attendance");

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

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.SchoolPeriods)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_School_Periods_Table_School_Master");
            });

            modelBuilder.Entity<SchoolYears>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId })
                    .HasName("PK_Table_School_Years");

                entity.ToTable("school_years");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("marking_period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

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
                    .HasColumnName("short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.SchoolYears)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_School_Years_Table_School_Master");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.SectionId })
                    .HasName("PK_Table_Sections");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Semesters>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.MarkingPeriodId })
                    .HasName("PK_Table_Semesters");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.MarkingPeriodId).HasColumnName("marking_period_id");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academic_year")
                    .HasColumnType("decimal(4, 0)");

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
                    .HasColumnName("short_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.YearId).HasColumnName("year_id");

                entity.HasOne(d => d.SchoolMaster)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Semesters_Table_School_Master");

                entity.HasOne(d => d.SchoolYears)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.YearId })
                    .HasConstraintName("FK_Table_Semesters_Table_School_Years");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryid");

                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.SchoolId, e.UserId })
                    .HasName("PK_Table_User_Master_1");

                entity.ToTable("user_master");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EmailAddress)
                .HasColumnName("emailaddress")
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LangId)
                    .HasColumnName("lang_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.MembershipId).HasColumnName("membership_id");

                entity.Property(e => e.Name)
                .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PasswordHash)
                .HasColumnName("passwordhash")
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.UserMaster)
                    .HasForeignKey(d => d.LangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_User_Master_Table_Language");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.UserMaster)
                    .HasForeignKey(d => new { d.TenantId, d.SchoolId, d.MembershipId })
                    .HasConstraintName("FK_Table_User_Master_Table_Membership");
            });

            CountryData(modelBuilder);
            StateData(modelBuilder);
            CityData(modelBuilder);

        }
        private void CountryData(ModelBuilder mb)
        {
            mb.Entity<Country>().HasData(new Country
            {
                Id = 1,
                Name = "Afghanistan",
                CountryCode = "001"
            },
            new Country
            {
                Id = 2,
                Name = "India",
                CountryCode = "002"
            }
            ,
            new Country
            {
                Id = 3,
                Name = "India",
                CountryCode = "003"
            }
            ,
            new Country
            {
                Id = 4,
                Name = "India",
                CountryCode = "004"
            }
            );
        }

        private void StateData(ModelBuilder mb)
        {
            mb.Entity<State>().HasData(new State
            {
                Id = 1,
                Name = "Badakhshan",
                CountryId = 1
            },
            new State
            {
                Id = 2,
                Name = "Badghis",
                CountryId = 1
            }
            ,
            new State
            {
                Id = 3,
                Name = "Berat",
                CountryId = 2
            }
            ,
            new State
            {
                Id = 4,
                Name = "Bulqize",
                CountryId = 2
            }
            ,
            new State
            {
                Id = 5,
                Name = "Adrar",
                CountryId = 3
            }
            ,
            new State
            {
                Id = 6,
                Name = "Ain Defla",
                CountryId = 3
            }
            ,
            new State
            {
                Id = 7,
                Name = "Uttar Pradesh",
                CountryId = 4
            }
            ,
            new State
            {
                Id = 8,
                Name = "West Bengal",
                CountryId = 4
            }
            );
        }

        private void CityData(ModelBuilder mb)
        {
            mb.Entity<City>().HasData(new City
            {
                Id = 1,
                Name = "Eshkashem",
                StateId = 1
            },
            new City
            {
                Id = 2,
                Name = "Bala Murghab",
                StateId = 2
            }
            ,
            new City
            {
                Id = 3,
                Name = "Tirana",
                StateId = 3
            }
            ,
            new City
            {
                Id = 4,
                Name = "kraste",
                StateId = 4
            }
            ,
            new City
            {
                Id = 5,
                Name = "Albani",
                StateId = 5
            }
            ,
            new City
            {
                Id = 6,
                Name = "Aïn Bénian",
                StateId = 6
            }
            ,
            new City
            {
                Id = 7,
                Name = "Lucknow",
                StateId = 7
            }
            ,
            new City
            {
                Id = 8,
                Name = "Kolkata",
                StateId = 8
            }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string[] tenants = new string[] { "TenantA" };
                string connectionString = "Server=DESKTOP-7SUI58K\\SQLEXPRESS;Database={tenant};User Id=sa; Password=admin@123;MultipleActiveResultSets=true";
                optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "opensisv2test"));

                //foreach (string tenant in tenants)
                //{
                //    optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"));
                //}
            }
        }
    }
}
