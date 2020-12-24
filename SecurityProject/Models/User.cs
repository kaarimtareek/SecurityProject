using System;
using System.Collections.Generic;

#nullable disable

namespace SecurityProject.Models
{
    public partial class User
    {
        public User()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public byte[] Iv { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
