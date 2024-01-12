using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Orders;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productsRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(
            IBasketRepository basketRepo,
            //IGenericRepository<Product> productsRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            //IGenericRepository<Order> ordersRepo
            IUnitOfWork unitOfWork
            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            //_productsRepo = productsRepo;
            //_deliveryMethodsRepo = deliveryMethodsRepo;
            //_ordersRepo = ordersRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliverMethodId, Address shippingAddress)
        {
            // 1. Get Basket From Baskets Repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliverMethodId);

            // 5. Create Order
            var order = new Order(buyerEmail, orderItems, shippingAddress, deliveryMethod, subTotal);
            await _unitOfWork.Repository<Order>().AddAsync(order);

            // 6. Save To Database [TODO]

            var result = await _unitOfWork.Complete();
            if(result <= 0) return null;

            return order;
        }


        public async Task<Order> GetOrderById(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(orderId, buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }


        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

        }
    }
}
