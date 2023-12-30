using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Models;

namespace University.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly UniversityDbContext _context;

        public UniversityRepository(UniversityDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #region LessonRepository
        //All Lesson
        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            return await _context.Lessons.OrderBy(o => o.Code).ToListAsync();
        }

        //Get Lesson With ID
        public async Task<Lesson> GetLessonsByIdAsync(int code)
        {
            return await _context.Lessons.Where(l => l.Code == code).FirstOrDefaultAsync();
        }

        //Get Lesson With ID & Include Teacher
        public async Task<Lesson> GetlessonByTeacher(int code, bool includeTeacher)
        {
            throw new NotImplementedException();
        }

        //Add Lesson (Post)
        public async Task<int> AddLessonAsync(Lesson lesson)
        {
            await _context.Lessons.AddAsync(lesson);
            return lesson.Code;
        }

        //Delete Lesson
        public void DeleteLessonAsync(Lesson lesson)
        {
            _context.Lessons.Remove(lesson);
        }
        #endregion



        #region Teacher

        //Get All Teacher
        public async Task<IEnumerable<Teacher>> GetAllTeacherAsync()
        {
            return await _context.Teachers.OrderBy(t => t.ID).ToArrayAsync();
        }

        //Get Teacher With ID
        public async Task<Teacher> GetTeacherByIdAsync(int Id)
        {
            return await _context.Teachers.Where(t => t.ID == Id).FirstOrDefaultAsync();
        }

        //Add Teacher
        public async Task<int> AddTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            return teacher.ID;
        }

        //Delete Teacher
        public void DeleteTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
        }
        #endregion



        #region Student
        //Get All Student
        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _context.Students.OrderBy(s => s.ID).ToListAsync();
        }

        //Get Student With ID
        public async Task<Student> GetStudentByIdAsync(int Id)
        {
            return await _context.Students.Where(s => s.ID == Id).FirstOrDefaultAsync();
        }

        //Add Student
        public async Task<int> AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            return student.ID;
        }

        //Delete Student
        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
        }
        #endregion


        //Save
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }


    }
}
