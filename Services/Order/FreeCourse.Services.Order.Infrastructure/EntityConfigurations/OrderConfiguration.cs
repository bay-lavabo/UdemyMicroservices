using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Infrastructure.EntityConfigurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Domain.OrderAggregate.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.OrderAggregate.Order> builder)
        {
            builder.ToTable("Orders", "ordering");
            builder.OwnsOne(o => o.Address).WithOwner();
        }
    }
}
