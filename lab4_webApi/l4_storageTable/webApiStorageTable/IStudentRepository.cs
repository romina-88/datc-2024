using System.Collections.Generic;
using System.Threading.Tasks;
using webApiStorageTable;

public interface IStudentRepository {
    Task<List<StudentEntity>> GetAllStudents();
    Task CreateStudent(StudentEntity student);
}