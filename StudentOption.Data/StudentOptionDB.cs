using Microsoft.Data.SqlClient;
using StudentOption.Classes;

namespace StudentOption.Data;

public class StudentOptionDB(string connectionString)
{
    private readonly string _connectionString = connectionString;

    #region GetList

    public async Task<List<Course>> GetCoursesAsync()
    {
        List<Course> courses = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"
            SELECT cs.CourseId, cs.Title, ctg.CategoryName, eb.ExamBoardName
            FROM dbo.Courses cs, dbo.Categories ctg, dbo.ExamBoards eb
            WHERE cs.CategoryId = ctg.CategoryId
            AND cs.ExamBoardId = eb.ExamBoardId
            ORDER BY cs.CourseId ASC";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
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

    public async Task<List<Student>> GetStudentsAsync()
    {
        List<Student> students = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"
            SELECT s.StudentId, s.FirstName, s.LastName, s.DateOfBirth
            FROM dbo.Students s
            ORDER BY s.StudentId ASC";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
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

    public async Task<List<ClassSet>> GetClassSetsFromCoruseAsync(Course course)
    {
        List<ClassSet> classSets = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT cls.ClassSetId, t.TeacherId, t.Title, t.FirstName, t.LastName, t.Qualification
            FROM dbo.Courses cs, dbo.Teachers t, dbo.ClassSets cls
            WHERE cls.CourseId = cs.CourseId
            AND cls.TeacherId = t.TeacherId
            AND cs.CourseId = {course.Id}
            ORDER BY cls.ClassSetId ASC";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
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

    public async Task<List<Student>> GetStudentsFromClassSetAsync(ClassSet classSet)
    {
        List<Student> students = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT s.StudentId, s.FirstName, s.LastName, s.DateOfBirth
            FROM dbo.Students s, dbo.ClassEnrollments ce, dbo.ClassSets cls
            WHERE cls.ClassSetId = ce.ClassSetId
            AND ce.StudentId = s.StudentId
            AND cls.ClassSetId = {classSet.Id}
            ORDER BY s.StudentId ASC";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
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

    public async Task<List<ClassSet>> GetClassSetsFromStudentAsync(Student student)
    {
        List<ClassSet> classSets = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT cls.ClassSetId, cs.CourseId, cs.Title, cg.CategoryName, eb.ExamBoardName, t.TeacherId, t.Title, t.FirstName, t.LastName, t.qualification
            FROM dbo.Courses cs, dbo.Teachers t, dbo.ClassSets cls, dbo.Students s, dbo.ClassEnrollments ce, dbo.ExamBoards eb, dbo.Categories cg
            WHERE cls.ClassSetId = ce.ClassSetId
            AND s.StudentId = ce.StudentId
            AND s.StudentId = {student.Id}
            AND t.TeacherId = cls.TeacherId
            AND cs.CourseId = cls.CourseId
            AND cg.CategoryId = cs.CategoryId
            AND eb.ExamBoardId = cs.ExamBoardId
            ORDER BY cls.ClassSetId ASC";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                int classSetId = dataReader.GetInt32(0);
                int courseId = dataReader.GetInt32(1);
                string courseTitle = dataReader.GetString(2);
                string category = dataReader.GetString(3);
                string examBoard = dataReader.GetString(4);
                int teacherId = dataReader.GetInt32(5);
                string teacherTitle = dataReader.GetString(6);
                string firstName = dataReader.GetString(7);
                string lastName = dataReader.GetString(8);
                string qualification = dataReader.GetString(9);
                classSets.Add(new(classSetId, new(courseId, category, examBoard, courseTitle), new(teacherId, teacherTitle, firstName, lastName, qualification)));
            }
        }

        return classSets;
    }
    #endregion

    #region GetSpecific

    public async Task<Course> GetCourseByIdAsync(int courseId)
    {
        string title = string.Empty, category = string.Empty, examBoard = string.Empty;
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT cs.Title, ctg.CategoryName, eb.ExamBoardName
            FROM dbo.Courses cs, dbo.Categories ctg, dbo.ExamBoards eb
            WHERE cs.CategoryId = ctg.CategoryId
            AND cs.ExamBoardId = eb.ExamBoardId
            AND cs.CourseId = {courseId}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
            {
                title = dataReader.GetString(0);
                category = dataReader.GetString(1);
                examBoard = dataReader.GetString(2);
            }
        }

        return new(courseId, category, examBoard, title);
    }

    public async Task<ClassSet> GetClassSetByIdWithCourseAsync(Course course, int classSetId)
    {
        int teacherId = 0;
        string title = string.Empty, firstName = string.Empty, lastName = string.Empty, qualification = string.Empty;

        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT t.TeacherId, t.Title, t.FirstName, t.LastName, t.Qualification
            FROM dbo.Courses cs, dbo.Teachers t, dbo.ClassSets cls
            WHERE cls.CourseId = cs.CourseId
            AND cls.TeacherId = t.TeacherId
            AND cls.ClassSetId = {classSetId}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
            {
                teacherId = dataReader.GetInt32(0);
                title = dataReader.GetString(1);
                firstName = dataReader.GetString(2);
                lastName = dataReader.GetString(3);
                qualification = dataReader.GetString(4);
            }
        }

        return new(classSetId, course, new(teacherId, title, firstName, lastName, qualification));
    }

    public async Task<Student> GetStudentByIdAsync(int studentId)
    {
        string firstName = string.Empty, lastName = string.Empty;
        DateOnly dateOfBirth = new();

        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT s.FirstName, s.LastName, s.DateOfBirth
            FROM dbo.Students s
            WHERE s.StudentId = {studentId}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read()) // TO-DO
            {
                firstName = dataReader.GetString(0);
                lastName = dataReader.GetString(1);
                dateOfBirth = DateOnly.FromDateTime(dataReader.GetDateTime(2));
            }
        }

        return new(studentId, firstName, lastName, dateOfBirth);
    }

    #endregion

    #region GetNumber

    public async Task<int> GetStudentNoByClassSetAsync(ClassSet classSet)
    {
        int num = 0;
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT COUNT(*)
            FROM dbo.Students s, dbo.ClassEnrollments ce, dbo.ClassSets cls
            WHERE cls.ClassSetId = ce.ClassSetId
            AND ce.StudentId = s.StudentId
            AND cls.ClassSetId = {classSet.Id}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                num = dataReader.GetInt32(0);
            }
        }

        return num;
    }

    #endregion GetNumber

    #region CheckExistance

    public async Task<bool> ExistCourseIdAsync(int id)
    {
        int num = 0;
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();
            
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT COUNT(*)
            FROM dbo.Courses cs
            WHERE cs.CourseId = {id}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                num = dataReader.GetInt32(0);
            }
        }

        return num != 0;
    }

    public async Task<bool> ExistClassSetIdWithCourseAsync(Course course, int id)
    {
        int num = 0;
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT COUNT(*)
            FROM dbo.ClassSets cls
            WHERE cls.CourseId = {course.Id}
            AND cls.ClassSetId = {id}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                num = dataReader.GetInt32(0);
            }
        }

        return num != 0;
    }

    public async Task<bool> ExistStudentIdAsync(int id)
    {
        int num = 0;
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = @$"
            SELECT COUNT(*)
            FROM dbo.Students s
            WHERE s.StudentId = {id}";

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                num = dataReader.GetInt32(0);
            }
        }

        return num != 0;
    }

    #endregion
}