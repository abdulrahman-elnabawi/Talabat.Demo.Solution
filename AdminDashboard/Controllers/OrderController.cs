using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications.Orders;

namespace AdminDashboard.Controllers
{

    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
                this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications();
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return View(orders);
        }
        public async Task<IActionResult> GetUserOrders(string buyerEmail)
        {
            var spec =new OrderWithItemsAndDeliveryMethodSpecifications(buyerEmail);
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return View("Index", orders);
        }
    }
}
