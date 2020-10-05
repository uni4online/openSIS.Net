using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace opensis.data.Migrations.SqlServerMigrations
{
    public partial class MSSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    stateid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    countrycode = table.Column<string>(unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gradelevels",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    grade_id = table.Column<int>(nullable: false),
                    short_name = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    title = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    next_grade_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Gradelevels", x => new { x.tenant_id, x.school_id, x.grade_id });
                });

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    lang_id = table.Column<int>(nullable: false),
                    lcid = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
                    locale = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    language_code = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Language", x => x.lang_id);
                });

            migrationBuilder.CreateTable(
                name: "notice",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    notice_id = table.Column<int>(nullable: false),
                    target_membership_ids = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "Signifies group of user for whom notice is visible. to be saved as comma separated values. if user's membership_id falls in any of the value, he can see the notice."),
                    title = table.Column<string>(unicode: false, nullable: false),
                    body = table.Column<string>(unicode: false, nullable: false),
                    valid_from = table.Column<DateTime>(type: "date", nullable: false),
                    valid_to = table.Column<DateTime>(type: "date", nullable: false),
                    isactive = table.Column<bool>(nullable: false),
                    created_by = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Notice", x => new { x.tenant_id, x.school_id, x.notice_id });
                });

            migrationBuilder.CreateTable(
                name: "plans",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    plan_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    max_api_checks = table.Column<int>(nullable: true),
                    features = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Plans", x => new { x.tenant_id, x.school_id, x.plan_id });
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    room_id = table.Column<int>(nullable: false),
                    title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    capacity = table.Column<int>(nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    sort_order = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Rooms", x => new { x.tenant_id, x.school_id, x.room_id });
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    section_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    sort_order = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Sections", x => new { x.tenant_id, x.school_id, x.section_id });
                });

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    countryid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "school_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    school_internal_id = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    school_alt_id = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
                    school_state_id = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
                    school_district_id = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    school_level = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    school_classification = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    school_name = table.Column<string>(maxLength: 100, nullable: true),
                    alternate_name = table.Column<string>(maxLength: 100, nullable: true),
                    street_address_1 = table.Column<string>(maxLength: 150, nullable: true),
                    street_address_2 = table.Column<string>(maxLength: 150, nullable: true),
                    city = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    county = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    division = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    state = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    district = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    zip = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
                    country = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    current_period_ends = table.Column<DateTime>(type: "datetime", nullable: true),
                    max_api_checks = table.Column<int>(nullable: true),
                    features = table.Column<string>(unicode: false, nullable: true),
                    plan_id = table.Column<int>(nullable: true),
                    created_by = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(unicode: false, fixedLength: true, maxLength: 50, nullable: true),
                    date_modifed = table.Column<DateTime>(type: "datetime", nullable: true),
                    longitude = table.Column<double>(nullable: true),
                    latitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_School_Master", x => new { x.tenant_id, x.school_id });
                    table.ForeignKey(
                        name: "FK_Table_School_Master_Table_Plans",
                        columns: x => new { x.tenant_id, x.school_id, x.plan_id },
                        principalTable: "Plans",
                        principalColumns: new[] { "tenant_id", "school_id", "plan_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "membership",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    membership_id = table.Column<int>(nullable: false, comment: "can be considered as profileid of Opensis1"),
                    profile = table.Column<string>(unicode: false, maxLength: 30, nullable: false, comment: "E.g. admin,student,teacher"),
                    title = table.Column<string>(unicode: false, maxLength: 100, nullable: false, comment: "e.g. Administrator,Student,Teacher,Dept. head"),
                    access = table.Column<string>(unicode: false, nullable: true),
                    weekly_update = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_membership_1", x => new { x.tenant_id, x.school_id, x.membership_id });
                    table.ForeignKey(
                        name: "FK_Table_membership_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_calendars",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    calender_id = table.Column<int>(nullable: false),
                    title = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true),
                    academic_year = table.Column<decimal>(type: "decimal(4, 0)", nullable: true),
                    default_calender = table.Column<bool>(nullable: true),
                    days = table.Column<string>(unicode: false, maxLength: 7, nullable: true),
                    rollover_id = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_School_Calendars", x => new { x.tenant_id, x.school_id, x.calender_id });
                    table.ForeignKey(
                        name: "FK_Table_School_Calendars_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_detail",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: true),
                    affiliation = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    associations = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    locale = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    lowest_grade_level = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    highest_grade_level = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    date_school_opened = table.Column<DateTime>(type: "date", nullable: true),
                    date_school_closed = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<bool>(nullable: true),
                    gender = table.Column<string>(fixedLength: true, maxLength: 6, nullable: true),
                    internet = table.Column<bool>(nullable: true),
                    electricity = table.Column<bool>(nullable: true),
                    telephone = table.Column<string>(fixedLength: true, maxLength: 20, nullable: true),
                    fax = table.Column<string>(fixedLength: true, maxLength: 20, nullable: true),
                    website = table.Column<string>(fixedLength: true, maxLength: 150, nullable: true),
                    email = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    twitter = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    facebook = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    instagram = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    youtube = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    linkedin = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    name_of_principal = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    name_of_assistant_principal = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    school_logo = table.Column<byte[]>(maxLength: 50, nullable: true),
                    running_water = table.Column<bool>(nullable: true),
                    main_source_of_drinking_water = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true),
                    currently_available = table.Column<bool>(nullable: true),
                    female_toilet_type = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    total_female_toilets = table.Column<short>(nullable: true),
                    total_female_toilets_usable = table.Column<short>(nullable: true),
                    female_toilet_accessibility = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    male_toilet_type = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    total_male_toilets = table.Column<short>(nullable: true),
                    total_male_toilets_usable = table.Column<short>(nullable: true),
                    male_toilet_accessibility = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    comon_toilet_type = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    total_common_toilets = table.Column<short>(nullable: true),
                    total_common_toilets_usable = table.Column<short>(nullable: true),
                    common_toilet_accessibility = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    handwashing_available = table.Column<bool>(nullable: true),
                    soap_and_water_available = table.Column<bool>(nullable: true),
                    hygene_education = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_Table_School_Detail_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_periods",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    period_id = table.Column<int>(nullable: false),
                    academic_year = table.Column<decimal>(type: "decimal(4, 0)", nullable: true),
                    sort_order = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    short_name = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    length = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    block = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    ignore_scheduling = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    attendance = table.Column<bool>(nullable: true),
                    rollover_id = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    start_time = table.Column<TimeSpan>(nullable: true),
                    end_time = table.Column<TimeSpan>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_School_Periods", x => new { x.tenant_id, x.school_id, x.period_id });
                    table.ForeignKey(
                        name: "FK_Table_School_Periods_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_years",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    marking_period_id = table.Column<int>(nullable: false),
                    academic_year = table.Column<decimal>(type: "decimal(4, 0)", nullable: true),
                    title = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    short_name = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    sort_order = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_start_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_end_date = table.Column<DateTime>(type: "date", nullable: true),
                    does_grades = table.Column<bool>(nullable: true),
                    does_exam = table.Column<bool>(nullable: true),
                    does_comments = table.Column<bool>(nullable: true),
                    rollover_id = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_School_Years", x => new { x.tenant_id, x.school_id, x.marking_period_id });
                    table.ForeignKey(
                        name: "FK_Table_School_Years_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_master",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    emailaddress = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    passwordhash = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    lang_id = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    membership_id = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_User_Master_1", x => new { x.tenant_id, x.school_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_Table_User_Master_Table_Language",
                        column: x => x.lang_id,
                        principalTable: "Language",
                        principalColumn: "lang_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Table_User_Master_Table_Membership",
                        columns: x => new { x.tenant_id, x.school_id, x.membership_id },
                        principalTable: "membership",
                        principalColumns: new[] { "tenant_id", "school_id", "membership_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "semesters",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    marking_period_id = table.Column<int>(nullable: false),
                    academic_year = table.Column<decimal>(type: "decimal(4, 0)", nullable: true),
                    year_id = table.Column<int>(nullable: true),
                    title = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    short_name = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    sort_order = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_start_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_end_date = table.Column<DateTime>(type: "date", nullable: true),
                    does_grades = table.Column<bool>(nullable: true),
                    does_exam = table.Column<bool>(nullable: true),
                    does_comments = table.Column<bool>(nullable: true),
                    rollover_id = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Semesters", x => new { x.tenant_id, x.school_id, x.marking_period_id });
                    table.ForeignKey(
                        name: "FK_Table_Semesters_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Table_Semesters_Table_School_Years",
                        columns: x => new { x.tenant_id, x.school_id, x.year_id },
                        principalTable: "school_years",
                        principalColumns: new[] { "tenant_id", "school_id", "marking_period_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "quarters",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    marking_period_id = table.Column<int>(nullable: false),
                    academic_year = table.Column<decimal>(type: "decimal(4, 0)", nullable: true),
                    semester_id = table.Column<int>(nullable: true),
                    title = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    short_name = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    sort_order = table.Column<decimal>(type: "decimal(10, 0)", nullable: true),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_start_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_end_date = table.Column<DateTime>(type: "date", nullable: true),
                    does_grades = table.Column<bool>(nullable: true),
                    does_exam = table.Column<bool>(nullable: true),
                    does_comments = table.Column<bool>(nullable: true),
                    rollover_id = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Quarters", x => new { x.tenant_id, x.school_id, x.marking_period_id });
                    table.ForeignKey(
                        name: "FK_Table_Quarters_Table_School_Master",
                        columns: x => new { x.tenant_id, x.school_id },
                        principalTable: "school_master",
                        principalColumns: new[] { "tenant_id", "school_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Table_Quarters_Table_Semesters",
                        columns: x => new { x.tenant_id, x.school_id, x.semester_id },
                        principalTable: "Semesters",
                        principalColumns: new[] { "tenant_id", "school_id", "marking_period_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "progress_periods",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(nullable: false),
                    school_id = table.Column<int>(nullable: false),
                    marking_period_id = table.Column<int>(nullable: false),
                    academic_year = table.Column<decimal>(type: "decimal(4, 0)", nullable: false),
                    quarter_id = table.Column<int>(nullable: false),
                    title = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    short_name = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    sort_order = table.Column<int>(nullable: true),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_start_date = table.Column<DateTime>(type: "date", nullable: true),
                    post_end_date = table.Column<DateTime>(type: "date", nullable: true),
                    does_grades = table.Column<bool>(nullable: true),
                    does_exam = table.Column<bool>(nullable: true),
                    does_comments = table.Column<bool>(nullable: true),
                    rollover_id = table.Column<int>(nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Progress_periods", x => new { x.tenant_id, x.school_id, x.marking_period_id, x.academic_year, x.quarter_id });
                    table.ForeignKey(
                        name: "FK_Table_Progress_periods_Table_Quarters",
                        columns: x => new { x.tenant_id, x.school_id, x.quarter_id },
                        principalTable: "Quarters",
                        principalColumns: new[] { "tenant_id", "school_id", "marking_period_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "city",
                columns: new[] { "id", "name", "stateid" },
                values: new object[,]
                {
                    { 1, "Eshkashem", 1 },
                    { 2, "Bala Murghab", 2 },
                    { 3, "Tirana", 3 },
                    { 4, "kraste", 4 },
                    { 5, "Albani", 5 },
                    { 6, "Aïn Bénian", 6 },
                    { 7, "Lucknow", 7 },
                    { 8, "Kolkata", 8 }
                });

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "id", "countrycode", "name" },
                values: new object[,]
                {
                    { 4, "004", "India" },
                    { 3, "003", "India" },
                    { 2, "002", "India" },
                    { 1, "001", "Afghanistan" }
                });

            migrationBuilder.InsertData(
                table: "state",
                columns: new[] { "id", "countryid", "name" },
                values: new object[,]
                {
                    { 1, 1, "Badakhshan" },
                    { 2, 1, "Badghis" },
                    { 3, 2, "Berat" },
                    { 4, 2, "Bulqize" },
                    { 5, 3, "Adrar" },
                    { 6, 3, "Ain Defla" },
                    { 7, 4, "Uttar Pradesh" },
                    { 8, 4, "West Bengal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_progress_periods_tenant_id_school_id_quarter_id",
                table: "progress_periods",
                columns: new[] { "tenant_id", "school_id", "quarter_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Quarters_tenant_id_school_id_semester_id",
                table: "Quarters",
                columns: new[] { "tenant_id", "school_id", "semester_id" });

            migrationBuilder.CreateIndex(
                name: "IX_school_detail_tenant_id_school_id",
                table: "school_detail",
                columns: new[] { "tenant_id", "school_id" });

            migrationBuilder.CreateIndex(
                name: "IX_school_master_tenant_id_school_id_plan_id",
                table: "school_master",
                columns: new[] { "tenant_id", "school_id", "plan_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_tenant_id_school_id_year_id",
                table: "Semesters",
                columns: new[] { "tenant_id", "school_id", "year_id" });

            migrationBuilder.CreateIndex(
                name: "IX_user_master_lang_id",
                table: "user_master",
                column: "lang_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_master_tenant_id_school_id_membership_id",
                table: "user_master",
                columns: new[] { "tenant_id", "school_id", "membership_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "gradelevels");

            migrationBuilder.DropTable(
                name: "notice");

            migrationBuilder.DropTable(
                name: "progress_periods");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "school_calendars");

            migrationBuilder.DropTable(
                name: "school_detail");

            migrationBuilder.DropTable(
                name: "school_periods");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "user_master");

            migrationBuilder.DropTable(
                name: "quarters");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "membership");

            migrationBuilder.DropTable(
                name: "semesters");

            migrationBuilder.DropTable(
                name: "school_years");

            migrationBuilder.DropTable(
                name: "school_master");

            migrationBuilder.DropTable(
                name: "plans");
        }
    }
}
