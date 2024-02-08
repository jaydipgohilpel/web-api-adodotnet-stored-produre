using System.Data.SqlClient;
using System.Data;
using web_api_adodotnet_stored_produre.Models;

public class StudentRepository : IStudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("dbcs");
    }

    public IEnumerable<StudentGetModel> GetAllStudents(int? id)
    {
        List<StudentGetModel> lstStudent = new List<StudentGetModel>();

        try
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var sp = "getStudentsRecord";
                if (id != null) sp = "getStudentRecordById";

                SqlCommand cmd = new SqlCommand(sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (id != null) cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(id));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StudentGetModel student = new StudentGetModel();
                    student.StudentId = Convert.ToInt32(rdr["StudentId"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.EmailAddress = rdr["EmailAddress"].ToString();
                    student.Gender = rdr["Gender"].ToString();
                    student.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"]);

                    lstStudent.Add(student);
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return lstStudent;
    }


    public int AddStudent(StudentModel student)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("addNewStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;

        }
    }


    public int UpdateStudent(int StudentId, StudentModel student)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("updateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }

    }

    public int DeleteStudent(int? StudentID)
    {

        using (SqlConnection con = new SqlConnection(_connectionString))
        {

            SqlCommand cmd = new SqlCommand("deleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentID);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }
    }

}
