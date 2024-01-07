namespace University.Models
{
    public record GetLesson(
                    int Page ,
                    int pageSize ,
                    string? SearchItem ,
                    string? sortColumn ,
                    string? sortOrder
                            );
}
