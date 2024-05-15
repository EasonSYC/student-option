namespace StudentOption.Classes;

public class Student(int id, string firstName, string lastName, DateOnly dateOfBirth)
{
    public int ID
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
}
