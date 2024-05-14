namespace StudentOption.Classes;

public class Student(string firstName, string lastName, DateOnly dateOfBirth)
{
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
}
