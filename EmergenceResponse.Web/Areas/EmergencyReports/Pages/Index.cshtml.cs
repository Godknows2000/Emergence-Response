using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace EmergenceResponse.Web.Areas.EmergencyReports.Pages
{
    public class IndexModel : SysListPageModel<Emergency>
    {
        public void OnGet(int? p, int? ps, Guid? spId)
        {
            var query = Db.Emergencies.Include(c => c.Creator).Include(c => c.ServiceProvider).AsQueryable();

            if(CurrentUser.MemberId != null)
            {
                query = query.Where(c => c.CreatorId == CurrentUser.MemberId);
            }

            if(spId != null)
            {
                query = query.Where(c => c.ServiceProviderId == spId);
            }
            List = query.OrderByDescending(c => c.CreationDate).ToPagedList(p ?? 1, ps ?? DefaultPageSize);

            PageTitle = $"{List.Count} Emergence{ (List.Count==1 ? "" : "s")} Reported";
        }
    }
}
