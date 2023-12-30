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


        [HttpGet("{Id}")]
        public async Task<ActionResult<StudentDto>> GetStudentWithId(int Id)
        {
            var student = await _UniversityRepository.GetStudentByIdAsync(Id);
            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> AddStudentAsync(StudentFotCreationDto studentCreation)
        {
            var student = _mapper.Map<Models.Student>(studentCreation);
            await _UniversityRepository.AddStudent(student);
            await _UniversityRepository.SaveChanges();
            var FinallyMapStudent = _mapper.Map<StudentDto>(student);
            return Ok(new { Message = $"Stuendt With ID {student.ID} And Name {student.Name} Is Add..." });
        } 

        [HttpPut("{StudentID}")]
        public async Task<ActionResult> UpdateStudentAsync(int StudentID , StudentForUpdateDto studentForUpdate)
        {
            var student = await _UniversityRepository.GetStudentByIdAsync(StudentID);
            if(student == null)
            {
                return BadRequest($"Student with ID {StudentID} not found...");
            }
            _mapper.Map(studentForUpdate , student);
            await _UniversityRepository.SaveChanges();
            var FinallyStudent = _mapper.Map<Dto.StudentDto>(student);
            return Ok(new {Message = $"Student with ID {student.ID} and Name {student.Name} is modify..." });
        }

        [HttpDelete("{StudentID}")]
        public async Task<ActionResult> DeleteStudentAsync(int StudentID)
        {
            var student = await _UniversityRepository.GetStudentByIdAsync(StudentID);
            if(student == null)
            {
                return BadRequest($"Student with ID {StudentID} not found...");
            }
            _UniversityRepository.DeleteStudent(student);
            await _UniversityRepository.SaveChanges();
            return Ok(new {Message = $"Student with ID {StudentID} id delete..." });
        }
    }
}
