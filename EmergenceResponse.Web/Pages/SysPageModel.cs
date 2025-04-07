
using EmergenceResponse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using wCyber.Helpers.Identity;
using wCyber.Helpers.Web;
using wCyber.Lib.FileStorage;

namespace EmergenceResponse.Web.Pages
{
    [Authorize]
    public class SysPageModel : PageModel
    {
        public SysPageModel()
        {
            BreadCrumb = new BreadCrumb(GetType());
        }
        IdentityClient _identityClient;
        protected IdentityClient IdentityClient
        {
            get
            {
                if (_identityClient == null)
                    _identityClient = Request.HttpContext.RequestServices.GetService<IdentityClient>();
                return _identityClient;
            }
        }




        static bool IsDbCreated;
        EmergenceContext _db;
        public EmergenceContext Db
        {
            get
            {
                if (_db == null)
                {
                    _db = Request.HttpContext.RequestServices.GetService<EmergenceContext>();
                    if (!IsDbCreated)
                    {
                        try
                        {
                            _db.Database.Migrate();
                        }
                        catch { }
                        IsDbCreated = true;
                    }
                }
                return _db;
            }
        }
        public Guid CurrentUserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        User _currentUser;
        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = Db.Users
                        .FirstOrDefault(c => c.Id == CurrentUserId);
                }
                return _currentUser;
            }
        }

        IFileStore _filestore;
        protected IFileStore FileStore
        {
            get
            {
                if (_filestore == null) _filestore = Request.HttpContext.RequestServices.GetService<IFileStore>();
                return _filestore;
            }
        }

        [ViewData]
        public string Title { get; protected set; }

        [ViewData]
        public string PageTitle { get; protected set; }

        [ViewData]
        public BreadCrumb BreadCrumb { get; protected set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var area = context.RouteData.Values["area"]?.ToString();
            Title = GetType().Namespace[(GetType().Namespace.LastIndexOf(".") + 1)..];
            if (Title == "Pages" && GetType().Namespace.Contains("Area")) Title = area;
            base.OnPageHandlerExecuting(context);
        }
    }
}