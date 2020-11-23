using opensis.data.ViewModels.ParentInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface IParentInfoRepository
    {
        public ParentInfoAddViewModel AddParentInfo(ParentInfoAddViewModel parentInfoAddViewModel);
        //public ParentInfoAddViewModel ViewParentInfo(ParentInfoAddViewModel parentInfoAddViewModel);
        public ParentInfoAddViewModel UpdateParentInfo(ParentInfoAddViewModel parentInfoAddViewModel);
    }
}
