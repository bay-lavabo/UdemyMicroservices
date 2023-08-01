using AutoMapper;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Services.Discount.Repository.Abstract;
using FreeCourse.Services.Discount.Services.Abstract;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Services.Discount.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<NoContent>> DeleteById(int id)
        {
            var result = await _discountRepository.DeleteById(id);

            return result ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Discount not found.", 404);

        }

        public async Task<ResponseDto<List<DiscountDto>>> GetAll()
        {
            var results = await _discountRepository.GetAll();

            return ResponseDto<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(results), 200);
        }

        public async Task<ResponseDto<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            var result = await _discountRepository.GetByCodeAndUserId(code, userId);

            return result != null ? ResponseDto<DiscountDto>.Success(_mapper.Map<DiscountDto>(result), 200) : ResponseDto<DiscountDto>.Fail("Discount not found.", 404);
        }

        public async Task<ResponseDto<DiscountDto>> GetById(int id)
        {
            var result = await _discountRepository.GetById(id);

            return result != null ? ResponseDto<DiscountDto>.Success(_mapper.Map<DiscountDto>(result), 200) : ResponseDto<DiscountDto>.Fail("Discount not found.", 404);
        }

        public async Task<ResponseDto<NoContent>> Save(DiscountDto discountDto)
        {
            var result = await _discountRepository.Save(discountDto);

            return result ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("An error accured while insert.", 500);
        }

        public async Task<ResponseDto<NoContent>> Update(DiscountDto discountDto)
        {
            var result = await _discountRepository.Update(discountDto);

            return result ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Discount not found.", 404);
        }
    }
}
