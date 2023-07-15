using AutoMapper;
using FreeCourse.Services.Catalog.Dto;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Repository.Abstract;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);

            var response = await _categoryRepository.CreateAsync(category);

            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(response), 200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Category not found.", 404);
            }

            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
