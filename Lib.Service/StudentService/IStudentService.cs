using Lib.SchoolModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib.Service.StudentService
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudents();

        Task<int> SaveStudent( Student std);
        Task<string> DeleteStudent(int Id);
    }
}
