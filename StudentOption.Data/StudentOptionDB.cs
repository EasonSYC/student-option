using Microsoft.Data.SqlClient;

namespace StudentOption.Data;

public class StudentOptionDB(string connectionString)
{
    private readonly string _connectionString = connectionString;

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
