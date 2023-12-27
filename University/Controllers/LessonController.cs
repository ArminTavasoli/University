using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Repository;

namespace University.Controllers
{
    [Route("api/Lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IUniversityRepository _UniversityRepository;
        private readonly IMapper _mapper;

        public LessonController(IUniversityRepository universityRepository , IMapper mapper)
        {
            this._UniversityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
        {
            var lessons = await _UniversityRepository.GetAllLessonsAsync();
            var AllLesson = _mapper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(new {Message = "کل درس ها" , AllLesson });
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<LessonDto>> GetLessonWithId(int code)
        {
            var lesson = await _UniversityRepository.GetLessonsByIdAsync(code);
            var lessonMapping = _mapper.Map<LessonDto>(lesson);
            return Ok(new { Message = $"کتاب با کد {code}" , lessonMapping});
        }
    }
}
