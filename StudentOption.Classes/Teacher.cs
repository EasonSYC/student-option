namespace StudentOption.Classes;

public class Teacher(string title, string firstName, string lastName, string qualification)
{
    public string Title
    {
        get;
    } = title;

    public string FirstName
    {
        get;
    } = firstName;

    public string LastName
    {
        get;
    } = lastName;

    public string Qualification
    {
        get;
    } = qualification;
}
