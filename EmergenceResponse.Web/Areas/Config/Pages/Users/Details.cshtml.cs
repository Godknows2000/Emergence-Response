using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmergenceResponse.Web.Areas.Config.Pages.Users
{
    public class DetailsModel : SysPageModel
    {
        public Member Member { get; set; }
        public void OnGet()
        {
            Member = Db.Members.FirstOrDefault(m => m.Id == CurrentUser.MemberId);

            PageTitle = Title = Member.Name;

        }
    }
}
