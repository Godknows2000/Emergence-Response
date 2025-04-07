using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace EmergenceResponse.Web.Pages
{
    public class SysListPageModel<T> : SysPageModel where T : class
    {
        [ViewData]
        public string SearchPlaceholder { get; protected set; }
        public string QueryString { get; protected set; }
        public IPagedList<T> List { get; protected set; }
        protected int DefaultPageSize { get; set; } = 100;

        [ViewData]
        public string DateFiltersCaption { get; protected set; }
    }
}