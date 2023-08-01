using AutoMapper;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Services.Discount;

namespace FreeCourse.Services.Discount.Mapping
{
    public class DiscountMapping : Profile
    {
        public DiscountMapping()
        {
            CreateMap<Models.Discount, DiscountDto>().ReverseMap();
        }
    }
}
