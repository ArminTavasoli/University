using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Models;
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

        //Get All Student
/*        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent()
        {
            var student = await _UniversityRepository.GetAllStudentAsync();
            var MappingStudent = _mapper.Map<IEnumerable<StudentDto>>();
            return Ok(new { Message = "Student List...", MappingStudent });
        }*/


        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
        {
            var result = new GetStudents(page, pageSize, search, sortColumn, sortOrder);
            var student = await _UniversityRepository.GetAllStudentAsync(result);
            var MappingStudent = _mapper.Map<IEnumerable<StudentDto>>(student);
            return Ok(new { Message = "Student List...", MappingStudent, Total = MappingStudent.Count() }) ;

        }

        //Get Student with ID
        [HttpGet("{Id}")]
        public async Task<ActionResult<StudentDto>> GetStudentWithId(int Id)
        {
            var GetStudent = await _UniversityRepository.GetStudentByIdAsync(Id);
            if(GetStudent == null)
            {
                return BadRequest($"Student with ID {Id} not found ...");
            }
            var Student = _mapper.Map<StudentDto>(GetStudent);
            return Ok(new {Message = $"You select stuent {GetStudent.Name} with ID {GetStudent.ID} ..."});
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



        #region Relation (Join)
        //Get Student & Lesson (Join)
        [HttpGet("lesson")]
        public async Task<ActionResult<IEnumerable<JoinLessonAndStudentDto>>> GetStudentLessonAsync()
        {
            var StudntsLesson = await _UniversityRepository.JoinStudentAndLessonAsync();
            var StudentHaveLesson  = _mapper.Map<IEnumerable<JoinLessonAndStudentDto>>(StudntsLesson);
            return Ok(new { Message = "Student that select Lesson..." , StudentHaveLesson });
        }


        //Get Student & Lesson (Join)
        [HttpGet("lesson/{studentID}")]
        public async Task<ActionResult<JoinLessonAndStudentDto>> GetStudentLessonWithIdAsync(int studentID)
        {
            var GetStudent = await _UniversityRepository.GetStudentByIdAsync(studentID);
            if (GetStudent == null)
            {
                return NotFound($"Not found Student with ID {studentID} ...");
            }
            var studentLesson = await _UniversityRepository.GetStudentsLessonByIdAsync(studentID);
            if(studentLesson == null)
            {
                return BadRequest($"Student with ID {studentID} do not have lesson...");
            }
            var lessonForThisStudent = _mapper.Map<JoinLessonAndStudentDto>(studentLesson);
            return Ok(new { Message = $"Lessons about student with Id{studentLesson.StudentID} and name {studentLesson.StudentName}" , lessonForThisStudent.LessonID , lessonForThisStudent.LessonName });
        }


        //Get Lesson And Teacher For Student
        [HttpGet("lesson&teacher/{studentID}")]
        public async Task<ActionResult> GetListLessonAndTeacherForStudentAsync(int studentID)
        {
            var GetStudent = await _UniversityRepository.GetStudentByIdAsync(studentID);
            if (GetStudent == null)
            {
                return BadRequest($"Student with Id {studentID} not found...");
            }
            var lessonAndTeacherForStudent = await _UniversityRepository.GetLessonAndTeacherForStudentAsync(studentID);
            if(lessonAndTeacherForStudent == null)
            {
                return BadRequest($"student with ID {studentID} do not have lesson...");
            }
            _mapper.Map<JunctionTableForJoinDto>(lessonAndTeacherForStudent);
            return Ok(new
            {
                Message = $"The list of Lesson and Teacher for {GetStudent.Name} with ID {lessonAndTeacherForStudent.StudentId}",
                lessonAndTeacherForStudent.LessonCode,
                lessonAndTeacherForStudent.LessonName,
                lessonAndTeacherForStudent.TeacherId,
                lessonAndTeacherForStudent.TeacherName
            });

        }
            #endregion Relation (Join)
        
    }
}
