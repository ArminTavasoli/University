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
        #endregion Lesson



        #region TeacherRpository

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
        #endregion Teacher



        #region StudentRepository
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
        #endregion student



        #region Relation (Join) Repository

        //Get Lesson and Teacher for Student
        public async Task<JunctionTableForJoin> GetLessonAndTeacherForStudentAsync(int studentID)
        {
            var StudentsLessonAndTeacher = await _context.Students.Join(_context.JunctionThSts,
                                                                        student => student.ID,
                                                                        junction => junction.StudentID,
                                                                        (student, junction) => new
                                                                        {
                                                                            student,
                                                                            junction
                                                                        }).Join(_context.Lessons,
                                                                            ResultJoin => ResultJoin.junction.LessonID,
                                                                            lesson => lesson.Code,
                                                                            (ResultJoin, lesson) => new
                                                                            {
                                                                                ResultJoin,
                                                                                lesson
                                                                            }).Join(_context.Teachers,
                                                                                ResultJoin2 => ResultJoin2.ResultJoin.junction.TeacherID,
                                                                                teacher => teacher.ID,
                                                                                (ResultJoin2, teacher) => new JunctionTableForJoin
                                                                                {
                                                                                    StudentId = studentID,
                                                                                    StudentName = ResultJoin2.ResultJoin.student.Name,
                                                                                    LessonCode = ResultJoin2.lesson.Code,
                                                                                    LessonName = ResultJoin2.lesson.Name,
                                                                                    TeacherId = teacher.ID,
                                                                                    TeacherName = teacher.Name
                                                                                }).FirstOrDefaultAsync();
            return StudentsLessonAndTeacher;
        }


        //Get Lesson & Stuedent (Join)
        public async Task<IEnumerable<LessonJoinTeacher>> JoinTeacherAndLessonAsync()
        {
            var JoinTable = await _context.Lessons.Join(_context.JunctionThSts,
                                                           lesson => lesson.Code,
                                                           junctionTable => junctionTable.LessonID,
                                                           (lesson, junctionTable) => new
                                                           {
                                                               lesson,
                                                               junctionTable
                                                           }).Join(_context.Teachers,
                                                            joinResult => joinResult.junctionTable.TeacherID,
                                                            teacher => teacher.ID,
                                                            (joinResult, teacher) => new LessonJoinTeacher
                                                            {
                                                                LessonId = joinResult.lesson.Code,
                                                                LessonName = joinResult.lesson.Name,
                                                                TeacherId = teacher.ID,
                                                                TeacherName = teacher.Name
                                                            }).ToListAsync();
            return JoinTable;
        }


        //Get Lesson And Teacher (Join)
        public async Task<LessonJoinTeacher> GetTeacherAndLessonByIdAsync(int TeacherID)
        {
            var JoinTable = await _context.Teachers.Where(t => t.ID == TeacherID)
                                                                    .Join(_context.JunctionThSts,
                                                                             teacher => teacher.ID,
                                                                             junctionTable => junctionTable.TeacherID,
                                                                             (teacher, junctionTable) => new
                                                                             {
                                                                                 teacher,
                                                                                 junctionTable
                                                                             }).Join(_context.Lessons,
                                                                                 join => join.junctionTable.LessonID,
                                                                                 lesson => lesson.Code,
                                                                                 (join, lesson) => new LessonJoinTeacher
                                                                                 {
                                                                                     LessonId = lesson.Code,
                                                                                     LessonName = lesson.Name,
                                                                                     TeacherId = TeacherID,
                                                                                     TeacherName = join.teacher.Name
                                                                                 }).FirstOrDefaultAsync();
            return JoinTable;
        }


        //Get Lesson & Student (Join)
        public async Task<IEnumerable<LessonJoinStudent>> JoinStudentAndLessonAsync()
        {
            var JoinStudentAndLesson = await _context.Lessons.Join(_context.JunctionThSts,
                                                                      lesson => lesson.Code,
                                                                      junction => junction.LessonID,
                                                                      (lesson, junction) => new
                                                                      {
                                                                          lesson,
                                                                          junction
                                                                      }).Join(_context.Students,
                                                                        JoinTable => JoinTable.junction.StudentID,
                                                                        student => student.ID,
                                                                        (JoinTable, student) => new LessonJoinStudent
                                                                        {
                                                                            LessonID = JoinTable.lesson.Code,
                                                                            LessonName = JoinTable.lesson.Name,
                                                                            StudentID = student.ID,
                                                                            StudentName = student.Name
                                                                        }).ToListAsync();
            return JoinStudentAndLesson;
        }


        //Get Lesson & Student (Join)
        public async Task<LessonJoinStudent> GetStudentsLessonByIdAsync(int studentID)
        {
            var joinStudentAndLesson = await _context.Students.Join(_context.JunctionThSts,
                                                                     student => student.ID,
                                                                     junction => junction.StudentID,
                                                                     (student, junction) => new
                                                                     {
                                                                         student,
                                                                         junction
                                                                     }).Join(_context.Lessons,
                                                                        joinResult => joinResult.junction.LessonID,
                                                                        lesson => lesson.Code,
                                                                        (joinResult, lesson) => new LessonJoinStudent
                                                                        {
                                                                            StudentID = studentID,
                                                                            StudentName = joinResult.student.Name,
                                                                            LessonID = lesson.Code,
                                                                            LessonName = lesson.Name
                                                                        }).FirstOrDefaultAsync();
            return joinStudentAndLesson;
        }
        #endregion Join


        //Save
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }


    }
}
