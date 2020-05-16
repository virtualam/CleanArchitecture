using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utility
{
    public class PaginationFilter : IPaginationFilter
    {
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public static PaginationFilter Initialize(int pageNumber, int pageSize)
        {
            var paginationFilter = new PaginationFilter();
            if (pageNumber > 0 && pageSize > 0)
            {
                paginationFilter = new PaginationFilter(pageNumber, pageSize);
            }
            return paginationFilter;
        }
    }
}
