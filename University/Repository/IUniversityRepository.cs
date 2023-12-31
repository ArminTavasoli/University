using University.Models;

namespace University.Repository
{
    public interface IUniversityRepository
    {
        //Lesson
        Task<IEnumerable<Lesson>> GetAllLessonsAsync();

        Task<Lesson> GetLessonsByIdAsync(int code);

        Task<Lesson> GetlessonByTeacher(int code, bool includeTeacher);

        Task<int> AddLessonAsync(Lesson lesson);
     
         void DeleteLessonAsync(Lesson lesson);


        //Teacher
        Task<IEnumerable<Teacher>> GetAllTeacherAsync();

        Task<Teacher> GetTeacherByIdAsync(int Id);

        Task<int> AddTeacherAsync(Teacher teacher);

        void DeleteTeacherAsync(Teacher teacher);



        //Students
        Task<IEnumerable<Student>> GetAllStudentAsync();

        Task<Student> GetStudentByIdAsync(int Id);

        Task<int> AddStudent(Student student);

        void DeleteStudent(Student student);

        
        //Get Lesson and Teacher for Student
        Task<JunctionTableForJoin> GetLessonAndTeacherForStudentAsync(int studentID);

        //Join Lesson & Teacher
        Task<IEnumerable<LessonJoinTeacher>> JoinTeacherAndLessonAsync();

        //Get Teacher and Lesson with ID
        Task<LessonJoinTeacher> GetTeacherAndLessonByIdAsync(int TeacherID);

        //Join Lesson & Student
        Task<IEnumerable<LessonJoinStudent>> JoinStudentAndLessonAsync();

        //Get Lesson and Student with ID
        Task<LessonJoinStudent> GetStudentsLessonByIdAsync(int studentID);

        //Save
        Task<bool> SaveChanges();

    }
}
