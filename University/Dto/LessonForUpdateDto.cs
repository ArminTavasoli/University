using System.ComponentModel.DataAnnotations;

namespace University.Dto
{
    public class LessonForUpdateDto
    {
        [Required]
        [MaxLength(14)]
        public String Name { get; set; }
    }
}
