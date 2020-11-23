using opensis.data.ViewModels.ParentInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.ParentInfo.Interfaces
{
    public interface IParentInfoRegisterService
    {
        public ParentInfoAddViewModel SaveParentInfo(ParentInfoAddViewModel parentInfoAddViewModel);
        //public ParentInfoAddViewModel ViewParentInfo(ParentInfoAddViewModel parentInfoAddViewModel);
        public ParentInfoAddViewModel UpdateParentInfo(ParentInfoAddViewModel parentInfoAddViewModel);
    }
}
