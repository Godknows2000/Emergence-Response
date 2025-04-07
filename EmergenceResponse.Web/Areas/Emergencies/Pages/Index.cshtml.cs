using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace EmergenceResponse.Web.Areas.Emergencies.Pages
{
    public class IndexModel : SysListPageModel<Emergency>
    {
        public void OnGet(int? p, int? ps)
        {
            PageTitle = Title = "Emergences";
            var query = Db.Emergencies.AsQueryable();

            List = query.OrderByDescending(c => c.CreationDate).ToPagedList(p ?? 1, ps ?? DefaultPageSize);
        }
    }
}
