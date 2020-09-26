using opensis.data.Models;
using opensis.data.ViewModels.Section;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface ISectionRepositiory
    {
        //public TableSections AddSection(Section section);
        SectionAddViewModel AddSection(SectionAddViewModel section);
        SectionAddViewModel UpdateSection(SectionAddViewModel section);
        public SectionAddViewModel ViewSection(SectionAddViewModel section);

        public SectionListViewModel GetAllsection(SectionListViewModel section);
        public SectionAddViewModel DeleteSection(SectionAddViewModel section);
    }
}
