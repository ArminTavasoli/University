using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Repository;

namespace University.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUniversityRepository _UniversityRepository;
        private readonly IMapper _mapper;

        public StudentController(IUniversityRepository universityRepository, IMapper mapper)
        {
            this._UniversityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent()
        {
            var student = await _UniversityRepository.GetAllStudentAsync();
            var MappingStudent = _mapper.Map<IEnumerable<StudentDto>>(student);
            return Ok(new { Message = "لیست دانشجوها", MappingStudent });
        }


        [HttpGet("Id")]
        public async Task<ActionResult<StudentDto>> GetStudentWithId(int Id)
        {
            var student = await _UniversityRepository.GetStudentByIdAsync(Id);
            return Ok(_mapper.Map<StudentDto>(student));
        }
    }
}
