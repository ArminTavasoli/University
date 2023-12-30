using AutoMapper;

namespace University.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            //Get
            CreateMap<Models.Teacher, Dto.TeacherDto>();

            //Post
            CreateMap<Dto.TeacherForCreationDto , Models.Teacher>();

            //Put
            CreateMap<Dto.TeacherForUpdateDto, Models.Teacher>();
        }
    }
}
