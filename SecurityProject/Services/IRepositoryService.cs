using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SecurityProject.Models;

namespace SecurityProject.Services
{
    public interface IRepositoryService
    {
         Course AddCourse(Course course, User user);
         bool DeleteCourse(Course course, User user);
         Course UpdateCourse(Course course, User user);
         Course GetCourse(int id, User user);
         IEnumerable<Course> GetCourses(User user);
         User GetUser(int id);
        IEnumerable<User> GetUsers(int id);
    }
}
