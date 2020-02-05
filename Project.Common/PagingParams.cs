using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public class PagingParams : IPagingParams
    {
        public PagingParams(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
