using AutoMapper;
using FreeCourse.Services.Catalog.Dto;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Repository.Abstract;
using FreeCourse.Services.Catalog.Repository.Concrete;
using FreeCourse.Services.Catalog.Services.Abstract;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Concrete
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CourseService(ICourseRepository courseRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryRepository.GetByIdAsync(course.CategoryId);
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var course = _mapper.Map<Course>(courseCreateDto);
            course.CreatedTime = DateTime.Now;

            var response = await _courseRepository.CreateAsync(course);

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(response), 200);
        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                return ResponseDto<CourseDto>.Fail("Course not found.", 404);
            }
            else
            {
                course.Category = await _categoryRepository.GetByIdAsync(course.CategoryId);
            }

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetByUserIdAsync(string userId)
        {
            var courses = await _courseRepository.GetByUserIdAsync(userId);

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryRepository.GetByIdAsync(course.CategoryId);
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseRepository.UpdateAsync(updateCourse);

            if (result == null)
            {
                return ResponseDto<NoContent>.Fail("Course not found.", 404);
            }

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteByIdAsync(string id)
        {
            var result = await _courseRepository.DeleteByIdAsync(id);

            if (result == true)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else
            {
                return ResponseDto<NoContent>.Fail("Course not found.", 404);
            }
        }
    }
}
