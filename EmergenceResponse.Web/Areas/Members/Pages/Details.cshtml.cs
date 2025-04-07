using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmergenceResponse.Web.Areas.Members.Pages
{
    public class DetailsModel : SysPageModel
    {
        public Member Member { get; set; }
        public void OnGet(Guid id)
        {
            Member = Db.Members.FirstOrDefault(c => c.Id == id);
            Title = PageTitle = Member.Name;
            
        }
    }
}
