using System.Reflection;

namespace StudentOption.Classes;

public class Course(int id, string category, string examBoard, string title)
{
    public int ID
    {
        get;
    } = id;

    public string Category
    {
        get;
    } = category;

    public string ExamBoard
    {
        get;
    } = examBoard;

    public string Title
    {
        get;
    } = title;
}
