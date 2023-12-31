namespace University.Models
{
    public class JunctionTableForJoin
    {
        public int LessonCode { get; set; }
        public string LessonName { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}
