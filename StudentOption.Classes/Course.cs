using System.Reflection;

namespace StudentOption.Classes;

public class Course(Category category, ExamBoard examBoard, string title)
{
    public Category Category
    {
        get;
    } = category;

    public ExamBoard ExamBoard
    {
        get;
    } = examBoard;

    public string Title
    {
        get;
    } = title;
}
