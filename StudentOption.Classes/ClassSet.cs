namespace StudentOption.Classes;

public class ClassSet(int id, Course course, Teacher teacher)
{
    public int ID
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
}
