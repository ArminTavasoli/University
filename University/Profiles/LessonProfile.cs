using AutoMapper;

namespace University.Profiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Models.Lesson , Dto.LessonDto>();
        }
    }
}
