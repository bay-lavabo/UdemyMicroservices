using FreeCourse.Services.Order.Domain.Core.Base.Abstract;
using FreeCourse.Services.Order.Domain.Core.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : EntityBase, IAggregateRoot
    {
        //EF Core features
        // - OwnedTypes
        // - Shadow Property
        // - Backed field
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }


        //Backed field;
        private readonly List<OrderItem> _orderItems;
        //Encapsulation;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Address address, string buyerId)
        {
            _orderItems = new List<OrderItem>();
            
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }

        public void AddOrderItem(string productId, string productName, string pictureUrl, int quantity, decimal price)
        {
            var existsProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existsProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, quantity, price);

                _orderItems.Add(newOrderItem);
            }
        }

        //Toplam miktarı getirir lambda kullanılırsa sadece get i  olur.
        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
