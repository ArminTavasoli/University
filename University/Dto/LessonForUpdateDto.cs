using System.ComponentModel.DataAnnotations;

namespace University.Dto
{
    public class LessonForUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(14)]
        public String Name { get; set; } = String.Empty;
    }
}
