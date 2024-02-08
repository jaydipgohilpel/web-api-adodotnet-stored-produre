using web_api_adodotnet_stored_produre.Models;

public interface IStudentRepository
{

    public IEnumerable<StudentGetModel> GetAllStudents(int? id);
    public int AddStudent(StudentModel student);
    public int UpdateStudent(int StudentId, StudentModel student);
    public int DeleteStudent(int? StudentID);

}