using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SecurityProject.Constants;
using SecurityProject.Models;

namespace SecurityProject.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly DbContextOptions<SecurityProjectDbContext> dbContextOptions;

        public RepositoryService(DbContextOptions<SecurityProjectDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public Course AddCourse(Course course, User user)
        {
            using (SecurityProjectDbContext context  = new SecurityProjectDbContext(dbContextOptions))
            {
                course.TeacherId = user.Id;
                context.Courses.Add(course);
                context.SaveChanges();
                return course;
            }
        }
        public bool DeleteCourse(Course course, User user)
        {
            if (course.TeacherId == user.Id || user.Type == USERTYPES.ADMIN)
            {
                using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
                {
                    context.Courses.Remove(course);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public Course UpdateCourse(Course course, User user)
        {
            if (course.TeacherId == user.Id || user.Type == USERTYPES.ADMIN)
            {
                using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
                {
                    var originalCourse = context.Courses.SingleOrDefault(x => x.Id == course.Id);
                    if (originalCourse != null)
                    {
                        originalCourse.Name = course.Name;
                        originalCourse.Description = course.Description;
                        originalCourse.Price = course.Price;
                        context.SaveChanges();
                        return originalCourse;
                    }
                    return null;
                }
            }
            return null;
        }
        public Course GetCourse(int id, User user)
        {
            using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
            {
                var course = context.Courses.SingleOrDefault(x => x.Id == id);
                if (course == null)
                    return null;
                if ( user.Type == USERTYPES.ADMIN || course.TeacherId == user.Id )
                    return course;
                return null;
                

            }
        }
        public IEnumerable<Course> GetCourses(User user)
        {
            using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
            {
                if (user.Type == USERTYPES.ADMIN)
                    return context.Courses.Include(c=>c.Teacher).ToList();
                return context.Courses.Include(c=>c.Teacher).Where(c => c.TeacherId == user.Id).ToList();
            }
               

        }

        public User GetUser(int id)
        {
            using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
            {
                return context.Users.SingleOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<User> GetUsers(int id)
        {
            using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
            {
                var user = context.Users.SingleOrDefault(x => x.Id == id);
                if(user!=null && user.Type == USERTYPES.ADMIN)
                {
                    return context.Users.ToList();
                }
                return null;
            }
        }
    }
}
