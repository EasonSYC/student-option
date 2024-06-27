using Microsoft.Data.SqlClient;
using StudentOption.Classes;

namespace StudentOption.Data;

public class StudentOptionDb(string connectionString)
{
    private static readonly SelectCommandBuilder _getCourse =
        new SelectCommandBuilder()
            .AddSelectEntry("Courses", "CourseId")
            .AddSelectEntry("Categories", "CategoryName")
            .AddSelectEntry("ExamBoards", "ExamBoardName")
            .AddSelectEntry("Courses", "Title")
            .AddWhereEqual("Courses", "CategoryId", "Categories", "CategoryId")
            .AddWhereEqual("Courses", "ExamBoardId", "ExamBoards", "ExamBoardId");
    private static readonly SelectCommandBuilder _getStudent =
        new SelectCommandBuilder()
            .AddSelectEntry("Students", "StudentId")
            .AddSelectEntry("Students", "FirstName")
            .AddSelectEntry("Students", "LastName")
            .AddSelectEntry("Students", "DateOfBirth");
    private static readonly SelectCommandBuilder _getTeacher =
        new SelectCommandBuilder()
            .AddSelectEntry("Teachers", "TeacherId")
            .AddSelectEntry("Teachers", "Title")
            .AddSelectEntry("Teachers", "FirstName")
            .AddSelectEntry("Teachers", "LastName")
            .AddSelectEntry("Teachers", "Qualification");
    private static readonly SelectCommandBuilder _getClassSet =
        new SelectCommandBuilder()
            .AddSelectEntry("ClassSets", "ClassSetId")
            .AddSelectEntry("Courses", "CourseId")
            .AddSelectEntry("Categories", "CategoryName")
            .AddSelectEntry("ExamBoards", "ExamBoardName")
            .AddSelectEntry("Courses", "Title")
            .AddSelectEntry("Teachers", "TeacherId")
            .AddSelectEntry("Teachers", "Title")
            .AddSelectEntry("Teachers", "FirstName")
            .AddSelectEntry("Teachers", "LastName")
            .AddSelectEntry("Teachers", "Qualification")
            .AddWhereEqual("Courses", "CourseId", "ClassSets", "CourseId")
            .AddWhereEqual("Teachers", "TeacherId", "ClassSets", "TeacherId")
            .AddWhereEqual("Courses", "ExamBoardId", "ExamBoards", "ExamBoardId")
            .AddWhereEqual("Courses", "CategoryId", "Categories", "CategoryId");

    private readonly string _connectionString = connectionString;

    #region SqlReaderToObject
    private static Course GetCourse(SqlDataReader reader)
    {
        return new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
    }
    private static Student GetStudent(SqlDataReader reader)
    {
        return new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), DateOnly.FromDateTime(reader.GetDateTime(3)));
    }
    private static Teacher GetTeacher(SqlDataReader reader)
    {
        return new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
    }
    private static ClassSet GetClassSet(SqlDataReader reader)
    {
        return new(reader.GetInt32(0), new(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)), new(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9)));
    }
    #endregion

    #region GetListAll
    public async Task<List<Course>> GetCoursesAsync()
    {
        List<Course> courses = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getCourse.ChangeOrderBy("Courses", "CourseId", true);
            command.CommandText = builder.ToString();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                courses.Add(GetCourse(dataReader));
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
            SelectCommandBuilder builder = _getStudent.ChangeOrderBy("Students", "StudentId", true);
            command.CommandText = builder.ToString();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                students.Add(GetStudent(dataReader));
            }
        }

        return students;
    }
    public async Task<List<Teacher>> GetTeachersAsync()
    {
        List<Teacher> teachers = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getTeacher.ChangeOrderBy("Teachers", "TeacherId", true);
            command.CommandText = builder.ToString();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                teachers.Add(GetTeacher(dataReader));
            }
        }

        return teachers;
    }
    public async Task<List<ClassSet>> GetClassSetsAsync()
    {
        List<ClassSet> classSets = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getClassSet.ChangeOrderBy("ClassSets", "ClassSetId", true);
            command.CommandText = builder.ToString();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                classSets.Add(GetClassSet(dataReader));
            }
        }

        return classSets;
    }
    #endregion

    #region GetListByRelation
    public async Task<List<ClassSet>> GetClassSetsFromCoruseAsync(Course course)
    {
        List<ClassSet> classSets = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getClassSet
                .AddWhereEqual("Courses", "CourseId", "@CourseId")
                .ChangeOrderBy("ClassSets", "ClassSetId", true);
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@CourseId", course.Id));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                classSets.Add(GetClassSet(dataReader));
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
            SelectCommandBuilder builder = _getStudent
                .AddWhereEqual("ClassSets", "ClassSetId", "ClassEnrollments", "ClassSetId")
                .AddWhereEqual("Students", "StudentId", "ClassEnrollments", "StudentID")
                .AddWhereEqual("ClassSets", "ClassSetId", "@ClassSetId")
                .ChangeOrderBy("Students", "StudentId", true);
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@ClassSetId", classSet.Id));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                students.Add(GetStudent(dataReader));
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
            SelectCommandBuilder builder = _getClassSet
                .AddWhereEqual("ClassSets", "ClassSetId", "ClassEnrollments", "ClassSetId")
                .AddWhereEqual("Students", "StudentId", "ClassEnrollments", "StudentId")
                .AddWhereEqual("Students", "StudentId", "@StudentId")
                .ChangeOrderBy("ClassSets", "ClassSetId", true);
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@StudentId", student.Id));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                classSets.Add(GetClassSet(dataReader));
            }
        }

        return classSets;
    }
    public async Task<List<ClassSet>> GetClassSetsFromTeacherAsync(Teacher teacher)
    {
        List<ClassSet> classSets = [];
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getClassSet
                .AddWhereEqual("Teachers", "TeacherId", "@TeacherId")
                .ChangeOrderBy("ClassSets", "ClassSetId", true);
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@TeacherId", teacher.Id));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                classSets.Add(GetClassSet(dataReader));
            }
        }

        return classSets;
    }
    #endregion

    #region GetSpecificById
    public async Task<Course> GetCourseByIdAsync(int courseId)
    {
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getCourse.AddWhereEqual("Courses", "CourseId", "@CourseId");
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@CourseId", courseId));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                return GetCourse(dataReader);
            }
        }

        throw new ArgumentOutOfRangeException($"CourseId does not exist: {courseId}");
    }
    public async Task<ClassSet> GetClassSetByIdAsync(int classSetId)
    {
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getClassSet.AddWhereEqual("ClassSets", "ClassSetId", "@ClassSetId");
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@ClassSetId", classSetId));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                return GetClassSet(dataReader);
            }
        }

        throw new ArgumentOutOfRangeException($"ClassSetId does not exist: {classSetId}");
    }
    public async Task<Student> GetStudentByIdAsync(int studentId)
    {
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getStudent.AddWhereEqual("Students", "StudentId", "@StudentId");
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@StudentId", studentId));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                return GetStudent(dataReader);
            }
        }

        throw new ArgumentOutOfRangeException($"StudentId does not exist: {studentId}");
    }
    public async Task<Teacher> GetTeacherByIdAsync(int teacherId)
    {
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getTeacher.AddWhereEqual("Teachers", "TeacherId", "@TeacherId");
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@TeacherId", teacherId));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                return GetTeacher(dataReader);
            }
        }

        throw new ArgumentOutOfRangeException($"TeacherId does not exist: {teacherId}");
    }
    public async Task<ClassSet> GetClassSetByIdWithCourseAsync(Course course, int classSetId)
    {
        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SelectCommandBuilder builder = _getClassSet
                .AddWhereEqual("ClassSets", "ClassSetId", "@ClassSetId")
                .AddWhereEqual("Courses", "CourseId", "@CourseId");
            command.CommandText = builder.ToString();
            command.Parameters.Add(new("@ClassSetId", classSetId));
            command.Parameters.Add(new("@CourseId", course.Id));

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            while (dataReader.Read())
            {
                return GetClassSet(dataReader);
            }
        }

        throw new ArgumentOutOfRangeException($"ClassSetId does not exist: {classSetId} within CourseId: {course.Id}");
    }
    #endregion

    #region GetNumber
    public async Task<int> GetStudentNoByClassSetAsync(ClassSet classSet)
    {
        List<Student> students = await GetStudentsFromClassSetAsync(classSet);
        return students.Count;
    }
    #endregion GetNumber

    #region CheckExistance
    public async Task<bool> ExistCourseIdAsync(int courseId)
    {
        try
        {
            await GetCourseByIdAsync(courseId);
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
    public async Task<bool> ExistClassSetIdWithCourseAsync(Course course, int classSetId)
    {
        try
        {
            await GetClassSetByIdWithCourseAsync(course, classSetId);
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
    public async Task<bool> ExistStudentIdAsync(int studentId)
    {
        try
        {
            await GetStudentByIdAsync(studentId);
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
    #endregion
}