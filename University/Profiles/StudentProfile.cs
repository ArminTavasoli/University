using AutoMapper;

namespace University.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            //Get
            CreateMap<Models.Student , Dto.StudentDto>();
            //Post
            CreateMap<Dto.StudentFotCreationDto , Models.Student>();
            //Put
            CreateMap<Dto.StudentForUpdateDto, Models.Student>();
        }
    }
}
