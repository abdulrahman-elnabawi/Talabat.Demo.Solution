using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace AdminDashboard.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var Brands = await unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return View(Brands);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductBrand brand)
        {
            try
            {
                await unitOfWork.Repository<ProductBrand>().AddAsync(brand);
                await unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please Enter A New Brand");
                return View("index", await unitOfWork.Repository<ProductBrand>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            unitOfWork.Repository<ProductBrand>().Delete(brand);
            await unitOfWork.Complete();
            return RedirectToAction("index");
        }
    }
}
