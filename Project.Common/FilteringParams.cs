using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public class FilteringParams : IFilteringParams
    {
        public FilteringParams(string search)
        {
            Search = search;

        }

        public FilteringParams(string search, Guid makeId)
        {
            Search = search;
            MakeId = makeId;
        }
        public string Search { get; set; }
        public Guid? MakeId { get; set; }
    }
}
