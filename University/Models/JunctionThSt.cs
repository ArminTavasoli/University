using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class JunctionThSt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [ForeignKey("TeacherID")]
        public Teacher? Teacher { get; set; }
        public int TeacherID { get; set; }


        [ForeignKey("StudentID")]
        public Student? Student { get; set; }
        public int StudentID { get; set; }

        [ForeignKey("LessonID")]
        public Lesson? Lesson { get; set; }
        public int LessonID { get; set; }
    }
}
