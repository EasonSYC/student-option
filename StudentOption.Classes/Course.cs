namespace StudentOption.Classes;

public record Course(int Id, string Category, string ExamBoard, string Title)
{
    public static readonly Course Default = new(0, string.Empty, string.Empty, string.Empty);
}
