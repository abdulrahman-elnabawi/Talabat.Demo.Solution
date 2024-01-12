using System.Collections.Generic;
using System;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public List<OrderItemDto> Items { get; set; } // Navigational Property [Many]
        public DateTimeOffset OrderDate { get; set; } 
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryCost { get; set; }
        public string Status { get; set; } 
        public int PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
