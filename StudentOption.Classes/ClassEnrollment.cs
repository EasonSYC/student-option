using System.Dynamic;

namespace StudentOption.Classes;

public class ClassEnrollment(ClassSet classSet, Student student)
{
    public Student Student
    {
        get;
    } = student;

    public ClassSet ClassSet
    {
        get;
    } = classSet;
}
