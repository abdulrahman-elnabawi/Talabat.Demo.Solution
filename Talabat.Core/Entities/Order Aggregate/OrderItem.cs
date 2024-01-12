using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrdered productItemOrdered, decimal price, int quantity)
        {
            ProductItemOrdered = productItemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ProductItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
