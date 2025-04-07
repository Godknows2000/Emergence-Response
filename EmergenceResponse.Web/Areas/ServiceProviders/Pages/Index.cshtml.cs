using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace EmergenceResponse.Web.Areas.ServiceProviders.Pages
{
    public class IndexModel : SysListPageModel<Data.ServiceProvider>
    {

        public void OnGet(int? p, int? ps, string? q)
        {
            var query = Db.ServiceProviders.AsQueryable();
            List = query.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);
            Title = PageTitle = "Service providers";
        }
    }
}
