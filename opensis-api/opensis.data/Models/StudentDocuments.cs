using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public partial class StudentDocuments
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int DocumentId { get; set; }
        public byte[] FileUploaded { get; set; }
        public DateTime? UploadedOn { get; set; }
        public string UploadedBy { get; set; }

        public virtual StudentMaster StudentMaster { get; set; }
    }
}
