using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace EmergenceResponse.Web.Areas.Members.Pages
{
    public class IndexModel : SysListPageModel<Member>
    {
        public void OnGet(int? p, int? ps, string? q)
        {
            var query = Db.Members.AsQueryable();
            List = query.OrderBy(c => c.Forename).ToPagedList(p ?? 1, ps ?? DefaultPageSize);
            Title = PageTitle = "Members";
        }
    }
}
