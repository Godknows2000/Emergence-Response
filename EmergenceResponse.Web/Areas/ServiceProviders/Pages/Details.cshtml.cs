using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmergenceResponse.Web.Areas.ServiceProviders.Pages
{
    public class DetailsModel : SysPageModel
    {
        public Data.ServiceProvider ServiceProvider { get; set; }
        public void OnGet(Guid id)
        {
            ServiceProvider = Db.ServiceProviders.Include(c => c.Location).FirstOrDefault(c => c.Id == id);
            Title = PageTitle = ServiceProvider.Name;
        }
    }
}
