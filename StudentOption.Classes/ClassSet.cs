namespace StudentOption.Classes;

public class ClassSet(int id, Course course, Teacher teacher)
{
    public int Id
    {
        get;
    } = id;

    public Course Course
    {
        get;
    } = course;

    public Teacher Teacher
    {
        get;
    } = teacher;

    public static readonly ClassSet Default = new(0, Course.Default, Teacher.Default);
}
