using AutoMapper;
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

        public LessonController(IUniversityRepository universityRepository, IMapper mapper)
        {
            this._UniversityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
            this._mapper = mapper ?? throw new ArgumentNullException();
        }

        #region Get All Lessons
        //Get All Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
        {
            var lessons = await _UniversityRepository.GetAllLessonsAsync();
            var AllLesson = _mapper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(new { Message = "All Lesson List :", AllLesson });
        }
        #endregion

        #region Get Lesson With LessonCode
        [HttpGet("{code}")]
        public async Task<ActionResult<LessonDto>> GetLessonWithId(int code)
        {
            var lesson = await _UniversityRepository.GetLessonsByIdAsync(code);
            var lessonMapping = _mapper.Map<LessonDto>(lesson);
            return Ok(new { Message = $"Your Lesson Code Is {code}", lessonMapping });
        }
        #endregion


        #region Add Lesson
        //Add Lesson (Post)
        [HttpPost]
        public async Task<ActionResult<LessonDto>> AddLessonAsync(LessonForCreationDto lessonCreation)
        {
            var lesson = _mapper.Map<Models.Lesson>(lessonCreation);
            await _UniversityRepository.AddLessonAsync(lesson);
            await _UniversityRepository.SaveChanges();
            var lessonMapping = _mapper.Map<Dto.LessonDto>(lesson);
            return Ok(new { Message = $"Add Lesson By Name {lesson.Name}", lesson.Code });

        }
        #endregion


        #region Update Lesson
        //Update Lesson
        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateLessonAsync(int code, LessonForUpdateDto lessonUpdate)
        {
            var lesson = await _UniversityRepository.GetLessonsByIdAsync(code);
            if (lesson == null)
            {
                return StatusCode(404, "This Lesson Not Found...");
            }
            _mapper.Map(lessonUpdate, lesson);
            await _UniversityRepository.SaveChanges();
            return StatusCode(204, $"The Lesson By Id {code} Is Update");
        }
        #endregion

        #region Delete Lesson
        //Delete Lesson
        [HttpDelete("{lessonCode}")]
        public async Task<ActionResult> DeleteLesson(int lessonCode)
        {
            var lesson = await _UniversityRepository.GetLessonsByIdAsync(lessonCode);
            if (lesson == null)
            {
                return StatusCode(404, $"Lesson With Code {lessonCode} not found");
            }
            _UniversityRepository.DeleteLessonAsync(lesson);
            await _UniversityRepository.SaveChanges();
            return StatusCode(200, $"Lesson With Code {lessonCode} Is Deleted");
        }
        #endregion


        #region Relation (Join) 

        //Get Lesson & Teacher
        [HttpGet("lesson&Teacher")]
        public async Task<ActionResult<IEnumerable<JoinLessonAndTeacherDto>>> JoinLessonWithTeacherAsync(int lessonId, int teacherId)
        {
            var tableJoin = await _UniversityRepository.JoinTeacherAndLessonAsync();
            var LessonsTeacher = _mapper.Map<IEnumerable<JoinLessonAndTeacherDto>>(tableJoin);
            return Ok(new { Message = "The Lesson given by the Teacher...", LessonsTeacher });
        }


        //Get lesson and Teacher
        [HttpGet("lesson&Teacher/{teacherId}")]
        public async Task<ActionResult> GetTeachersLessonAsync(int teacherId)
        {
            var getTeacherAndLessonByID = await _UniversityRepository.GetTeacherAndLessonByIdAsync(teacherId);
            return Ok(new { Message = $"You select Teacher {getTeacherAndLessonByID.TeacherName}...", getTeacherAndLessonByID });
        }
        #endregion relation (Join)


    }
}
