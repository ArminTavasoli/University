namespace University.Models
{
    public record GetTeacher(
        int page ,
        int pageSize , 
        string? searchItem ,
        string? sortCoumn ,
        string? sortOrder
        );
}
