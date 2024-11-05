using System.Collections.Generic;

namespace webApiExample
{
    public static class StudentRepo
    {
        public static List<Student> Students = new List<Student>() {
            new Student() { Id = 1, Name = "Mircea", Faculty = "AC", StudyYear = 1},
            new Student() { Id = 2, Name = "Ion", Faculty = "ETC", StudyYear = 2}
        };
    }
}