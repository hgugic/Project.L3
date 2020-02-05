using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public interface IPagingParams
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
    }
}
