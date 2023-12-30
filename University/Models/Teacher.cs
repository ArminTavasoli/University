using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(14)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string NationalCode { get; set; }

        [MaxLength(11)]
        public string? PhoneNumber { get; set; }

    }
}
