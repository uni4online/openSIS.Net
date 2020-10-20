using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels
{
    public enum FilterOptions
    {
        StartsWith = 1,
        EndsWith,
        Contains,
        DoesNotContain,
        IsEmpty,
        IsNotEmpty,
        IsGreaterThan,
        IsGreaterThanOrEqualTo,
        IsLessThan,
        IsLessThanOrEqualTo,
        IsEqualTo,
        IsNotEqualTo
    }
}
