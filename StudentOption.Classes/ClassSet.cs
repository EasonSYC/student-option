namespace StudentOption.Classes;

public class ClassSet(Course course, Teacher teacher)
{
    public Course Course
    {
        get;
    } = course;

    public Teacher Teacher
    {
        get;
    } = teacher;
}
