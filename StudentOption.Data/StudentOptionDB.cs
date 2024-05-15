using System.ComponentModel;
using System.Globalization;
using Microsoft.Data.SqlClient;
using StudentOption.Classes;

namespace StudentOption.Data;

public class StudentOptionDB(string connectionString)
{
    private readonly string _connectionString = connectionString;

    public List<Course> GetCourses()
    {
        List<Course> courses = [];
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
                courses.Add(new(id, category, examBoard, title));
            }
        }

        return courses;
    }

    public List<ClassSet> GetClassSetsFromCoruse(Course course)
    {
        List<ClassSet> classSets = [];
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT cls.ClassSetID, t.TeacherID, t.Title, t.FirstName, t.LastName, t.Qualification
            FROM dbo.Courses cs, dbo.Teachers t, dbo.ClassSets cls
            WHERE cls.CourseID = cs.CourseID
            AND cls.TeacherID = t.TeacherID
            AND cs.CourseID = {course.ID}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                int tid = dataReader.GetInt32(1);
                string title = dataReader.GetString(2);
                string firstName = dataReader.GetString(3);
                string lastName = dataReader.GetString(4);
                string qualification = dataReader.GetString(5);
                classSets.Add(new(id, course, new(tid, title, firstName, lastName, qualification)));
            }
        }

        return classSets;
    }

    public Course GetCourseByID(int courseID)
    {
        string title = string.Empty, category = string.Empty, examBoard = string.Empty;
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT cs.Title, ctg.CategoryName, eb.ExamBoardName
            FROM dbo.Courses cs, dbo.Categories ctg, dbo.ExamBoards eb
            WHERE cs.CategoryID = ctg.CategoryID
            AND cs.ExamBoardID = eb.ExamBoardID
            AND cs.CourseID = {courseID}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                title = dataReader.GetString(0);
                category = dataReader.GetString(1);
                examBoard = dataReader.GetString(2);
            }
        }

        return new(courseID, category, examBoard, title);
    }

    public bool ExistCourseID(int id)
    {
        int num = 0;
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT COUNT(*)
            FROM dbo.Courses cs
            WHERE cs.CourseID = {id}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                num = dataReader.GetInt32(0);
            }
        }

        return num != 0;
    }

    public bool ExistClassSetIDWithCourse(Course course, int id)
    {
        int num = 0;
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT COUNT(*)
            FROM dbo.ClassSets cls
            WHERE cls.CourseID = {course.ID}
            AND cls.ClassSetID = {id}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                num = dataReader.GetInt32(0);
            }
        }

        return num != 0;
    }

    public ClassSet GetClassSetByIDWithCourse(Course course, int classSetID)
    {
        int teacherID = 0;
        string title = string.Empty, firstName = string.Empty, lastName = string.Empty, qualification = string.Empty;

        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT t.TeacherID, t.Title, t.FirstName, t.LastName, t.Qualification
            FROM dbo.Courses cs, dbo.Teachers t, dbo.ClassSets cls
            WHERE cls.CourseID = cs.CourseID
            AND cls.TeacherID = t.TeacherID
            AND cls.ClassSetID = {classSetID}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                teacherID = dataReader.GetInt32(0);
                title = dataReader.GetString(1);
                firstName = dataReader.GetString(2);
                lastName = dataReader.GetString(3);
                qualification = dataReader.GetString(4);
            }
        }

        return new(classSetID, course, new(teacherID, title, firstName, lastName, qualification));
    }

    public List<Student> GetStudentsFromClassSet(ClassSet classSet)
    {
        List<Student> students = [];
        using (SqlConnection connection = new())
        {
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth
            FROM dbo.Students s, dbo.ClassEnrollments ce, dbo.ClassSets cls
            WHERE cls.ClassSetID = ce.ClassSetID
            AND ce.StudentID = s.StudentID
            AND cls.ClassSetID = {classSet.ID}";
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string firstName = dataReader.GetString(1);
                string lastName = dataReader.GetString(2);
                DateOnly dateOfBirth = DateOnly.FromDateTime(dataReader.GetDateTime(3));

                students.Add(new(id, firstName, lastName, dateOfBirth));
            }
        }

        return students;
    }
}
