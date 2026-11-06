using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult GetApprovedStudents()
        {
            var students = Repository2.Students
                .Where(s => s.Average > 70)
                .ToList();

            return Ok(students);
        }


        [HttpGet("{id}")]
        public IActionResult GetCertainStudent(int id)
        {
            var student = Repository2.Students.First(s => s.Id == id);

            if (student == null)
            {
                return NotFound($"No se encontró un estudiante con el ID: {id}.");
            }

            return Ok(student);
        }


      /*   [HttpGet("approved")]
        public IActionResult GetApprovedStudents()
        {
            var students = Repository2.Students
                .Where(s => s.Average > 70)
                .ToList();

            return Ok(students);
        } */


        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest("El nombre no puede estar vacío.");
            }

            if (student.Average < 0 || student.Average > 100)
            {
                return BadRequest("El promedio debe estar entre 0 y 100.");
            }

            Repository2.Students.Add(student);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = Repository2.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound($"No se encontró un estudiante con el ID: {id}.");
            }

            Repository2.Students.Remove(student);

            return NoContent();
        }
    }


    public class Repository2
    {
        public static List<Student> Students = new List<Student>
        {
            new Student() { Id = 1, Name = "Juan", Average = 85 },
            new Student() { Id = 2, Name = "María", Average = 65 },
            new Student() { Id = 3, Name = "Pedro", Average = 92 },
            new Student() { Id = 4, Name = "Ana", Average = 70 }
        };
    }
}