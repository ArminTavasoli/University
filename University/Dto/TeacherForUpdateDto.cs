using System.ComponentModel.DataAnnotations;

namespace University.Dto
{
    public class TeacherForUpdateDto
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(14)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string NationalCode { get; set; }

        [MaxLength(11)]
        public string? PhoneNumber { get; set; }
    }
}
