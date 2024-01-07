namespace University.Models
{
    public record GetStudents(
        int page ,
        int pageSize , 
        string? SearchItem ,
        string? sortColumn ,
        string? sortOrder
        );

}
