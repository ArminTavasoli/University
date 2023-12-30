using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Repository;

namespace University.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IUniversityRepository _UniversityRepository;
        private readonly IMapper _mapper;
        public TeacherController(IUniversityRepository universityRepository, IMapper mapper)
        {
            this._UniversityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeacher()
        {
            var teacher = await _UniversityRepository.GetAllTeacherAsync();
            var AllTeacher = _mapper.Map<IEnumerable<TeacherDto>>(teacher);
            return Ok(new { Message = "لیست تمام استادها" });
        }

        [HttpGet("Id")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int Id)
        {
            var teacher = await _UniversityRepository.GetTeacherByIdAsync(Id);
            return Ok(_mapper.Map<TeacherDto>(teacher));
        }
    }
}
