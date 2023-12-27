using AutoMapper;

namespace University.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Models.Teacher, Dto.TeacherDto>();
        }
    }
}
