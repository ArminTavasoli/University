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


        //Get All Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeacher()
        {
            var teacher = await _UniversityRepository.GetAllTeacherAsync();
            var AllTeacher = _mapper.Map<IEnumerable<TeacherDto>>(teacher);
            return Ok(new { Message = "All Teacher..." , AllTeacher});
        }

        //Get Teacher With ID
        [HttpGet("{Id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int Id)
        {
            var teacher = await _UniversityRepository.GetTeacherByIdAsync(Id);
            return Ok(_mapper.Map<TeacherDto>(teacher));
        }

        //Add Teacher (Post)
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> AddTeacherAsync(TeacherForCreationDto teacher)
        {
            var teachermap = _mapper.Map<Models.Teacher>(teacher);
            await _UniversityRepository.AddTeacherAsync(teachermap);
            await _UniversityRepository.SaveChanges();
            var finallyTeacher = _mapper.Map<Dto.TeacherDto>(teachermap);
            return Ok(new { Message = $"Teacher { teachermap.Name} with NationalCode {teachermap.NationalCode} " +
                                      $"And PhoneNumber {teachermap.PhoneNumber} Is Add to Teacher List" , finallyTeacher.ID});
        }


        [HttpPut("{TeacherID}")]
        public async Task<ActionResult> UpdateTeacherAsync(int TeacherID , TeacherForUpdateDto teacherUpdate)
        {
            var teacherExist = await _UniversityRepository.GetTeacherByIdAsync(TeacherID);
            if(teacherExist == null)
            {
                return StatusCode(404, $"Teacher With ID {TeacherID} not found");
            }
            _mapper.Map(teacherUpdate, teacherExist);
            await _UniversityRepository.SaveChanges();
            return StatusCode(201, $"Teacher with ID {teacherExist.ID} Is Modify");
        }

        [HttpDelete("{TeacherID}")]
        public async Task<ActionResult> DeleteTeacherAsync(int TeacherID)
        {
            var teacher = await _UniversityRepository.GetTeacherByIdAsync(TeacherID);
            if(teacher == null)
            {
                return StatusCode(404, $"Teacher With ID {TeacherID} Not Found...");
            }
            _UniversityRepository.DeleteTeacherAsync(teacher);
            await _UniversityRepository.SaveChanges();
            return StatusCode(201, $"{teacher.Name} By ID {teacher.ID} Is Delete...");
        }
    }
}
