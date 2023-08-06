using AutoMapper.Internal.Mappers;
using FreeCourse.Services.Order.Application.Commands.Update;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure.DbContexts;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers.Commands.Update
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ResponseDto<OrderDto>>
    {
        private readonly OrderDbContext _dbContext;

        public UpdateOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            //Önce db deki mevcut silinsin.
            var oldOrder = await _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.Id == request.Id).ToListAsync();

            if (oldOrder == null)
            {
                return ResponseDto<OrderDto>.Fail("Order not found.", 404);
            }

            oldOrder.ForEach(async x =>
            {
                foreach (var item in x.OrderItems)
                {
                    _dbContext.OrderItems.Remove(item);
                }

                _dbContext.Orders.Remove(x);
            });

            //Sonra db ye yeni order eklensin.
            var newAddress = new Address(request.AddressDto.Province, request.AddressDto.District, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(newAddress, request.BuyerId);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.PictureUrl, x.Quantity, x.Price);
            });

            await _dbContext.Orders.AddAsync(newOrder);

            await _dbContext.SaveChangesAsync();

            //Eklenen sipariş çağrılır.
            var order = await _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.Id == newOrder.Id).FirstOrDefaultAsync();

            var orderDto = ObjectMapper.Mapper.Map<OrderDto>(order);

            return ResponseDto<OrderDto>.Success(orderDto, 200);


        }
    }
}
