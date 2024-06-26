namespace StudentOption.Classes;

public record Student(int Id, string FirstName, string LastName, DateOnly DateOfBirth)
{
    public static readonly Student Default = new(0, string.Empty, string.Empty, new());
}
