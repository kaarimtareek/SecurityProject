using System;
using System.Collections.Generic;

#nullable disable

namespace SecurityProject.Models
{
    public partial class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TeacherId { get; set; }

        public virtual User Teacher { get; set; }
    }
}
