namespace EventSourcing.Data;

public partial class StudentEntity
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> EnrolledCourses { get; set; } = [];
}