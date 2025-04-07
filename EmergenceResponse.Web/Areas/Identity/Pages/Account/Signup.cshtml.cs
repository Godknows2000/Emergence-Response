using EmergenceResponse.Data;
using EmergenceResponse.Web.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wCyber.Lib;

namespace EmergenceResponse.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SignupModel : SysPageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public SignupModel(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Member Member { get; set; }
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string password)
        {
            //Create member
            Member.CreationDate = DateTime.Now;
            Member.Id = Guid.NewGuid();
            Db.Members.Add(Member);
            
            //Create User
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = $"{Member.Forename} {Member.Surname}",
                Email = Member.Email,
                LoginId = Member.Email,
                CreationDate = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsActive = true,
                IsEmailConfirmed = true,
                //RoleId = (int)UserRole._Admin,
                ActivationDate = DateTime.UtcNow,
                MemberId = Member.Id
            };

            Db.Users.Add(user);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

            await Db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
