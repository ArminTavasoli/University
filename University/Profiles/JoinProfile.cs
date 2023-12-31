using AutoMapper;

namespace University.Profiles
{
    public class JoinProfile : Profile
    {
        public JoinProfile()
        {
            //Lesson And Teacher
            CreateMap<Models.LessonJoinTeacher, Dto.JoinLessonAndTeacherDto>();

            //Lesson And Student
            CreateMap<Models.LessonJoinStudent, Dto.JoinLessonAndStudentDto>();

            //Lesson and Teacher and Student
            CreateMap<Models.JunctionTableForJoin, Dto.JunctionTableForJoinDto>();
        }
    }
}
