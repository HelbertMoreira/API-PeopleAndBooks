using System;
using System.Collections.Generic;

namespace PeopleAndBooks.DataConverter.Utils
{
    public class PagedSearchVO<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDiretion { get; set; }
        public Dictionary<string, Object> Filters { get; set; }
        public List<T> List { get; set; }

        public PagedSearchVO(){ }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDiretion)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDiretion = sortDiretion;
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDiretion, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDiretion = sortDiretion;
            Filters = filters;
        }

        public PagedSearchVO(int currentPage, string sortFields, string sortDiretion) 
            : this(currentPage, 10, sortFields, sortDiretion){ }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }
        public int GetPagedSize()
        {
            return PageSize == 0 ? 10 : CurrentPage;
        }
    }
}
