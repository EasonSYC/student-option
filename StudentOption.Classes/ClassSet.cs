namespace StudentOption.Classes;

public record ClassSet(int Id, Course Course, Teacher Teacher)
{
    public static readonly ClassSet Default = new(0, Course.Default, Teacher.Default);
}