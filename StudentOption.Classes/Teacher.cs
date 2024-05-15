namespace StudentOption.Classes;

public class Teacher(int id, string title, string firstName, string lastName, string qualification)
{
    public int ID
    {
        get;
    } = id;
    
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
