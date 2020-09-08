using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace opensis.data.Models
{
	[Table("Table_School_Master")]
	public class tblSchoolMaster
	{
		[Column(Order = 0)]
		public Guid Tenant_Id { get; set; }
		[Column(Order = 1)]
		public int School_Id { get; set; }
		public string School_Alt_Id { get; set; }
		public string School_State_Id { get; set; }
		public string School_District_Id { get; set; }
		public string School_Level { get; set; }
		public string School_Classification { get; set; }
		public string School_Name { get; set; }
		public string Alternate_Name { get; set; }
		public string Street_Address_1 { get; set; }
		public string Street_Address_2 { get; set; }
		public char City { get; set; }
		public char County { get; set; }
		public char Division { get; set; }
		public char State { get; set; }
		public char District { get; set; }
		public char Zip { get; set; }
        public char Country { get; set; }
        public Geometry GeoPosition { get; set; }
        public DateTime? Current_Period_ends { get; set; }
		public int? Max_api_checks { get; set; }
		public string Features { get; set; }

		//[ForeignKey("tblPlans.id")]
		//public int? Plan_id { get; set; }
		//public virtual tblPlans tblPlans { get; set; }
		public char Created_By { get; set; }
		public DateTime? Date_Created { get; set; }
		public char Modified_By { get; set; }
		public DateTime? Date_Modifed { get; set; }
	}
}
