using EmergenceResponse.Data;
using EmergenceResponse.Lib;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmergenceResponse.Web.Areas.EmergencyReports.Pages
{
    public class DetailsModel : SysPageModel
    {
        public Emergency Emergency { get; set; }
        public void OnGet(Guid id)
        {
            Emergency = Db.Emergencies
                .Include(c => c.Creator)
                .Include(c => c.ServiceProvider)
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id);
            PageTitle = Emergency.Description;
        }
        public async Task<IActionResult> OnPostApprove(Guid id)
        {
            Emergency = Db.Emergencies.Find(id);
            Emergency.StatusId = (int)EmergencyStatus.ASSIGNMENT_APPROVED;
            Emergency.ApprovalDate = DateTime.Now;
            Db.Update(Emergency);
            await Db.SaveChangesAsync();

            return RedirectToPage("Details", id);
        }
    }
}
