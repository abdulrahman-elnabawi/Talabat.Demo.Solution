using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.wwwroot
{
    public class BrandController : Controller
    {
        public BrandController(IUnitOfWork unitOfWork)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
