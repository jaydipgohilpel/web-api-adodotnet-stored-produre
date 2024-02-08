using Microsoft.AspNetCore.Mvc;
using web_api_adodotnet_stored_produre.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_api_adodotnet_stored_produre.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly StudentRepository _studentRepository;

        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/<studentController>
        [HttpGet]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StudentGetModel>> Get(int? id)
        {
            try
            {
                List<StudentGetModel> students = new List<StudentGetModel>();
                students = _studentRepository.GetAllStudents(id).ToList();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/<studentController>
        [HttpPost]
        public ActionResult<IEnumerable<StudentModel>> Post(StudentModel student)
        {
            if (student == null) return BadRequest();

            try
            {
                var rowsAffected = _studentRepository.AddStudent(student);
                if (rowsAffected == 1)
                    return Ok("Student added successfully.");
                else return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/<studentController>/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<StudentModel>> Put(int id, StudentModel student)
        {
            if (student == null || id == null) return BadRequest();

            try
            {
                var rowsAffected = _studentRepository.UpdateStudent(id, student);
                if (rowsAffected == 1)
                    return Ok("Student Updated successfully.");
                else return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<studentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            try
            {
                var rowsAffected = _studentRepository.DeleteStudent(id);
                if (rowsAffected == 1)
                    return Ok("Student Deleted successfully.");
                else return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
