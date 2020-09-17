using opensis.data.Models;
using System;
using System.Linq;

namespace opensis.data.Helper
{
    public class Utility<TEntity> where TEntity : class
    {
        public static int? GetMaxPK(CRMContext cRMContext, Func<TEntity, int> columnSelector)
        {
            var GetMaxId = cRMContext?.Set<TEntity>().Max(columnSelector);
            if (GetMaxId==null || GetMaxId<=0)
            {
                GetMaxId = 1;
            }
            else
            {
                GetMaxId = GetMaxId + 1;
            }
            return GetMaxId;
        }

    }
}
