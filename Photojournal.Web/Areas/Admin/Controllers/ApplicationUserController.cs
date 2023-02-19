using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Photojournal.Data;
using Photojournal.Data.Repository.IRepository;
using Photojournal.Models;

namespace Photojournal.Web.Areas.Admin.Controllers 
{
    public class ApplicationUserController : Controller
    {
        // TODO: Remove these two in favor of the unit of work pattern
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;
        //private readonly IUnitOfWork _unitOfWork;
        public ApplicationUserController(UserManager<ApplicationUser> usermanager, ApplicationDbContext context)
        {
            _userManager = usermanager;
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}
