namespace StudentOption.Classes;

public class Teacher(int id, string title, string firstName, string lastName, string qualification)
{
    public int Id
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

    public static readonly Teacher Default = new(0, string.Empty, string.Empty, string.Empty, string.Empty);
}
