using Microsoft.Data.SqlClient;

namespace StudentOption.Data;

public class StudentOptionDB(string connectionString) // To-do: OOP
{
    private readonly string _connectionString = connectionString;

    public List<(int id, string title, string category, string examBoard)> GetCourses()
    {
        List<(int id, string title, string category, string examBoard)> courses = [];
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"
            SELECT cs.CourseID, cs.Title, ctg.CategoryName, eb.ExamBoardName
            FROM dbo.Courses cs, dbo.Categories ctg, dbo.ExamBoards eb
            WHERE cs.CategoryID = ctg.CategoryID
            AND cs.ExamBoardID = eb.ExamBoardID";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string title = dataReader.GetString(1);
                string category = dataReader.GetString(2);
                string examBoard = dataReader.GetString(3);
                courses.Add((id, title, category, examBoard));
            }
        }

        return courses;
    }

    public List<(int id, string title, string firstName, string lastName, string qualification)> GetClassSetsFromCoruseID(int courseID)
    {
        List<(int id, string title, string firstName, string lastName, string qualification)> classSets = [];
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT cls.ClassSetID, t.Title, t.FirstName, t.LastName, t.Qualification
            FROM dbo.Courses cs, dbo.Teachers t, dbo.ClassSets cls
            WHERE cls.CourseID = cs.CourseID
            AND cls.TeacherID = t.TeacherID
            AND cs.CourseID = {courseID}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string title = dataReader.GetString(1);
                string firstName = dataReader.GetString(2);
                string lastName = dataReader.GetString(3);
                string qualification = dataReader.GetString(4);
                classSets.Add((id, title, firstName, lastName, qualification));
            }
        }

        return classSets;
    }

    public List<string> GetStudentNames()
    {
        List<string> names = [];
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM dbo.Students";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string name = $"{dataReader.GetString(1)} {dataReader.GetString(2)}";
                names.Add(name);
            }
        }
        return names;
    }
}
