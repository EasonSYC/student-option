namespace StudentOption.Classes;

public record Teacher(int Id, string Title, string FirstName, string LastName, string Qualification)
{
    public static readonly Teacher Default = new(0, string.Empty, string.Empty, string.Empty, string.Empty);
}
