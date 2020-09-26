using opensis.data.Models;
using opensis.data.ViewModels.Section;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Section.Interfaces
{
    public interface ISectionService
    {
        public SectionAddViewModel SaveSection(SectionAddViewModel section);
        public SectionAddViewModel UpdateSection(SectionAddViewModel section);
        public SectionAddViewModel ViewSection(SectionAddViewModel section);
        public SectionListViewModel GetAllsection(SectionListViewModel section);
        public SectionAddViewModel DeleteSection(SectionAddViewModel section);
    }
}
