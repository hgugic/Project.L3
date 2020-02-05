using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public class SortingParams : ISortingParams
    {
        public SortingParams(string sortDirection)
        {
            if (string.IsNullOrEmpty(sortDirection) || sortDirection != "dsc")
            {
                sortDirection = "asc";
            }

            SortDirection = sortDirection;
        }
        public string SortDirection { get; set; }
    }
}
