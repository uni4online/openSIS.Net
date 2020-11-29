using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Student
{
    public class StudentDocumentAddViewModel : CommonFields
    {
        public List<StudentDocuments> studentDocuments { get; set; }
    }
}
