using FreeCourse.Services.Order.Domain.Core.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : EntityBase
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public int Quantity { get; private set; }
        public Decimal Price { get; private set; }

        public OrderItem()
        {

        }

        public OrderItem(string productId, string productName, string pictureUrl, int quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Quantity = quantity;
            Price = price;
        }

        public void UpdateOrderItem(string productName, string pictureUrl, int quantity, decimal price)
        {
            this.ProductName = productName;
            this.PictureUrl = pictureUrl;
            this.Quantity = quantity;
            this.Price = price;
        }

    }
}
