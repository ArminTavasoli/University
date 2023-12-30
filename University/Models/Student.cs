using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(14)]
        public string Name { get; set; }

        [MaxLength(10)]
        public int NationalCode { get; set; }

        [MaxLength(11)]
        public int? PhoneNumber { get; set; }

    }
}
