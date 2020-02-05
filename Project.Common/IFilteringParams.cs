using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public interface IFilteringParams
    {
        string Search { get; set; }
        Guid? MakeId { get; set; }
    }
}
