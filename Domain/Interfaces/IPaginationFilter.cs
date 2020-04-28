using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IPaginationFilter
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
