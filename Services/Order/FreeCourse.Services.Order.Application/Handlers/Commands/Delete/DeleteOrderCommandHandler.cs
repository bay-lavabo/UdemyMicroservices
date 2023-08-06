using FreeCourse.Services.Order.Application.Commands.Delete;
using FreeCourse.Services.Order.Application.Commands.Insert;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Infrastructure.DbContexts;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers.Commands.Delete
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ResponseDto<IsSuccessDto>>
    {
        private readonly OrderDbContext _dbContext;

        public DeleteOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<IsSuccessDto>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.Id == request.Id).ToListAsync();

            if (!order.Any())
            {
                return ResponseDto<IsSuccessDto>.Fail("Order not found.", 404);
            }

            order.ForEach(async x =>
            {
                foreach (var item in x.OrderItems)
                {
                    _dbContext.OrderItems.Remove(item);
                }

                _dbContext.Orders.Remove(x);
            });

            await _dbContext.SaveChangesAsync();

            return ResponseDto<IsSuccessDto>.Success(new IsSuccessDto { IsSuccess = true }, 200);
        }
    }
}
