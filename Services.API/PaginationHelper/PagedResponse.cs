using Infrastructure.RepoServices;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.PaginationHelper
{
    public class PagedResponse
    {
        public PagedResponse() { }

        public PagedResponse(object data)
        {
            Data = data;
        }

        public object Data { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

        public static PagedResponse CreatePaginatedResponse(string path, PaginationFilter paginationFilter, object data)
        {
            var nextPage = paginationFilter.PageNumber >= 1
               ? _PageUri(path, new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
               : null;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? _PageUri(path, new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString()
                : null;

            return new PagedResponse
            {
                Data = data,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                NextPage = data != null ? nextPage : null,
                PreviousPage = previousPage
            };
        }

        private static Uri _PageUri(string path, PaginationFilter paginationFilter = null)
        {
            var uri = new Uri(path);

            if (paginationFilter == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(path, "pageNumber", paginationFilter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationFilter.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
