using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentsCourses = new HashSet<StudentsCourse>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDelited { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<StudentsCourse> StudentsCourses { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
