using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmergenceResponse.Web.Areas.EmergencyReports.Pages
{
    public class DirectionModel : SysPageModel
    {
        public Emergency Emergency { get; set; }
        public async Task OnGet(Guid id)
        {
            Emergency = await Db.Emergencies
                .Include(c => c.Creator)
                .Include(c => c.ServiceProvider).ThenInclude(c => c.Location)
                .Include(c => c.Location)
                .FirstAsync(c => c.Id == id);

            PageTitle = $"{Emergency.ServiceProvider.Name} to {Emergency.Location.Address + " " + Emergency.Location.City}";
        }
    }
}
