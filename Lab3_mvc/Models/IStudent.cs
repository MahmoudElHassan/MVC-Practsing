namespace Lab3_mvc.Models
{
    public interface IStudent
    {
        public List<Student> GetAllStudent();
        public Student GetStudentById(int id);
        public void AddStudents(Student std);
        public int GetNextId();
        public void DeleteById(int id);
        public void EditStudents(Student std);
        public void Editimg(int? id, string imgName);
    }
}



