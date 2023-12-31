namespace University.Dto
{
    public class JoinLessonAndTeacherDto
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
