using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityProject.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price{ get; set; }
        public int  TeacherId{ get; set; }
        public string TeacherName{ get; set; }
    }
}
