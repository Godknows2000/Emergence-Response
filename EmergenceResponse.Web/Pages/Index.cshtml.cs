using EmergenceResponse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmergenceResponse.Web.Pages
{
    [AllowAnonymous]
    public class IndexModel : SysPageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Emergency> Emergencies { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if(User.Identity.IsAuthenticated && CurrentUser.MemberId != null)
            {

                var query = Db.Emergencies.Include(c => c.ServiceProvider).Where(c => c.CreatorId == CurrentUser.MemberId);
                Emergencies = query.OrderByDescending(c => c.CreationDate).ToList();
            }

        }
    }
}