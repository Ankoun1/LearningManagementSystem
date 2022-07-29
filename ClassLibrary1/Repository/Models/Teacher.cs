using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeachersCourses = new HashSet<TeachersCourse>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDelited { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<TeachersCourse> TeachersCourses { get; set; }
    }
}
