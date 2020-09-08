using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace opensis.data.Models
{
	[Table("Table_School_Detail")]
	public class tblSchoolDetail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public virtual Guid? Tenant_Id { get; set; }
        public virtual int? School_Id { get; set; }
		[ForeignKey("Tenant_Id, School_Id")]
		public tblSchoolMaster SchoolMaster { get; set; }
		public string Affiliation { get; set; }
		public string Associations { get; set; }
		public string Locale { get; set; }
		public string Lowest_Grade_Level { get; set; }
		public string Highest_Grade_Level { get; set; }
		public DateTime? Date_School_Opened { get; set; }
		public DateTime? Date_School_Closed { get; set; }
		public bool? Status { get; set; }
		public string Gender { get; set; }
		public bool? Internet { get; set; }
		public bool? Electricity { get; set; }
		public string Telephone { get; set; }
		public string Fax { get; set; }
		public string Website { get; set; }
		public string Email { get; set; }
		public string Facebook { get; set; }
		public string Twitter { get; set; }
		public string Instagram { get; set; }
		public string Youtube { get; set; }
		public string LinkedIn { get; set; }
		public string Name_of_Principal { get; set; }
		public string Name_of_Assistant_Principal { get; set; }
		public byte[] School_Logo { get; set; }
		public bool? Running_Water { get; set; }
		public string Main_Source_of_Drinking_Water { get; set; }
		public bool? Currently_Available { get; set; }
		public string Female_Toilet_Type { get; set; }
		public Int16? Total_Female_Toilets { get; set; }
		public Int16? Total_Female_Toilets_Usable { get; set; }
		public string Female_Toilet_Accessibility { get; set; }
		public string Male_Toilet_Type { get; set; }
		public Int16? Total_Male_Toilets { get; set; }
		public Int16? Total_Male_Toilets_Usable { get; set; }
		public string Male_Toilet_Accessibility { get; set; }
		public string Comon_Toilet_Type { get; set; }
		public Int16? Total_Common_Toilets { get; set; }
		public Int16? Total_Common_Toilets_Usable { get; set; }
		public string Common_Toilet_Accessibility { get; set; }
		public bool? Handwashing_Available { get; set; }
		public bool? Soap_and_Water_Available { get; set; }
		public string Hygene_Education { get; set; }

	}
}
