using Microsoft.AspNetCore.Mvc;


namespace Lab3_mvc.Models
{
    public class StudentMoc: IStudent
    {
        List<Student> students = new List<Student>()
        {
            new Student () {Id=1,Name="Mahmoud",Age="25",stdImg="a1.jpg"},
            new Student () {Id=2,Name="Hassan",Age="27",stdImg="a2.jpg"},
            new Student () {Id=3,Name="Waleed",Age="23",stdImg="a3.jpg"},
        };

        public List<Student> GetAllStudent()
        {
            return students;
        }

        public Student GetStudentById(int id)
        {
            return students.FirstOrDefault(a => a.Id == id);
        }

        public void AddStudents(Student std)
        {
            students.Add(std);
        }

        public int GetNextId()
        {
            return students.Max(x => x.Id) + 1;
        }

        public void DeleteById(int id)
        {
            students.Remove(GetStudentById(id));
        }
        public void EditStudents(Student std)
        {
            Student oldstd = students.FirstOrDefault(x => x.Id == std.Id);
            oldstd.Name = std.Name;
            oldstd.Age = std.Age;
        }
        public void Editimg(int? id,string imgName)
        {
            Student oldstd = students.FirstOrDefault(a => a.Id == id);
            oldstd.stdImg = imgName;
        }
    }
}
