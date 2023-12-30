using University.Models;

namespace University.Repository
{
    public interface IUniversityRepository
    {
        Task<IEnumerable<Lesson>> GetAllLessonsAsync();

        Task<Lesson> GetLessonsByIdAsync(int code);

        Task<Lesson> GetlessonByTeacher(int code, bool includeTeacher);

        Task<IEnumerable< Teacher>> GetAllTeacherAsync();

        Task<Teacher> GetTeacherByIdAsync(int Id);

        Task <IEnumerable<Student>> GetAllStudentAsync();

        Task<Student> GetStudentByIdAsync(int Id);

        Task<int> AddLessonAsync(Lesson lesson);
        Task<bool> SaveChanges();

        Task void DeleteLessonAsync();

    }
}
