﻿using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Commands.Delete
{
    public class DeleteOrderCommand : IRequest<ResponseDto<IsSuccessDto>>
    {
        public int Id { get; set; }
    }
}
