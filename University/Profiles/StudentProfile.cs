using AutoMapper;

namespace University.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Models.Student , Dto.StudentDto>();
        }
    }
}
