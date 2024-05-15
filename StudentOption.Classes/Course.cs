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

    public static readonly Course Default = new(0, string.Empty, string.Empty, string.Empty);
}
