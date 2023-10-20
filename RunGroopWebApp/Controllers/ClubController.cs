using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }
        public IActionResult Index() //C
        {

            var clubs = _context.Clubs.ToList();//M
            return View(clubs);//V
        }
    }
}
