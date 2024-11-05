using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace webApiExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet(Name = "GetStudents")]
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



    
    }

}