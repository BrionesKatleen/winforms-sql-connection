//Imports
using MySql.Data.MySqlClient;
using TestWins.Model;

namespace TestWins.Repoository;


public class StudentRepository
{

    private readonly ConnectionSql _db = new ConnectionSql();

    //Create CRUD functionalities
    public void create(Student student)
    {
        using var conn = _db.connectSql(); //Connection
        conn.Open(); //Open Connection

        string query = "INSERT INTO (name, age, course) students VALUES(@name, @age, @course)";

        using var cmd = new MySqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@name", student.Name);
        cmd.Parameters.AddWithValue("@age", student.age);
        cmd.Parameters.AddWithValue("@course", student.course);

        cmd.ExecuteNonQuery();
    }

    public List<Student> getAll()
    {
        var list = new List<Student>();

        using var conn = _db.connectSql();
        conn.Open();

        string query = "SELECT * FROM students";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Student
            {
                studentId = reader.GetString(0),
                Name = reader.GetString(1),
                age = reader.GetInt32(2),
                course = reader.GetString(3),
            });
        }

        return list;
    }
    public void update(Student student)
    {
        using var conn = _db.connectSql();
        conn.Open();

        string query = "UPDATE students SET name = @name, age=@age, course=@course where studentId = @id";

        using var cmd = new MySqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@id", student.studentId);
        cmd.Parameters.AddWithValue("@name", student.Name);
        cmd.Parameters.AddWithValue("@age", student.age);
        cmd.Parameters.AddWithValue("@course", student.course);

        cmd.ExecuteNonQuery();

    }

    public void delete(string id)
    {
        using var conn = _db.connectSql();

        conn.Open();

        string query = "DELETE FROM students WHERE studentId = @id";

        using var cmd = new MySqlCommand(query, conn);

        cmd.ExecuteNonQuery();
    }

}