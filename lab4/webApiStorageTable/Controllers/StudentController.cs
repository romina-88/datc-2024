using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace webApiStorageTable.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        
        
        private IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentEntity>> Get()
        {
            return await _studentRepository.GetAllStudents();
        }

        [HttpPost]
        public async Task Post([FromBody]StudentEntity student) {
            try
            {
                await _studentRepository.CreateStudent(student);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }


        
        
   /* [HttpGet(Name = "GetStudents")]
    public IEnumerable<Student> Get()
    {
            return StudentRepo.Students;
    }
    
  //  [HttpGet(Name = "GetStudent")]
   // [Route("{Id}")]
     [HttpGet("{id}")]
    public ActionResult<Student> Get(int Id)
    {

        var studentItem = StudentRepo.Students.Find(x => x.Id == Id);
        return studentItem == null ? NotFound() : Ok(studentItem);
    }

    
    [HttpPost]
    public ActionResult<IEnumerable<Student>> Post(Student newStudent)
    {
        StudentRepo.Students.Add(newStudent);
        return StudentRepo.Students;
    } 


    [HttpPut("{id}")]
    public ActionResult<IEnumerable<Student>> Put(int id, Student updatedStudent)
    {
        Student student = StudentRepo.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        student.Name = updatedStudent.Name;
        student.Faculty = updatedStudent.Faculty;

        return StudentRepo.Students;
    }

    [HttpDelete("{id}")]
    public ActionResult<IEnumerable<Student>> Delete(int id)
    {
        Student student = StudentRepo.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        StudentRepo.Students.Remove(student);

        return StudentRepo.Students;
    }

    */

    
    }

}