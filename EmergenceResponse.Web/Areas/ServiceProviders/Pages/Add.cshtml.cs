using EmergenceResponse.Data;
using EmergenceResponse.Lib;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmergenceResponse.Web.Areas.ServiceProviders.Pages
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public Data.ServiceProvider ServiceProvider { get; set; }
        [BindProperty]
        public Location Location{get;set;}
        public void OnGet()
        {
            Title = PageTitle = "Add service provider";
        }
        public async Task<IActionResult> OnPost()
        {
            Location.Id = Guid.NewGuid();
            Db.Locations.Add(Location);

            ServiceProvider.Id = Guid.NewGuid();
            ServiceProvider.CreationDate = DateTime.Now;
            
            ServiceProvider.LocationId = Location.Id;
            ServiceProvider.StatusId = (int)ServiceProviderStatus.AWAITING_ACTIVATION;
            

            Db.ServiceProviders.Add(ServiceProvider);
            await Db.SaveChangesAsync();

            return RedirectToPage("Details", new {id = ServiceProvider.Id});
        }
    }
}
