namespace StudentOption.Classes;

public class Student(int id, string firstName, string lastName, DateOnly dateOfBirth)
{
    public int Id
    {
        get;
    } = id;

    public string FirstName
    {
        get;
    } = firstName;

    public string LastName
    {
        get;
    } = lastName;

    public DateOnly DateOfBirth
    {
        get;
    } = dateOfBirth;

    public static readonly Student Default = new(0, string.Empty, string.Empty, new());
}
