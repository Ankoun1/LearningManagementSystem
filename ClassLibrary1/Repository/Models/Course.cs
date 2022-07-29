using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Course
    {
        public Course()
        {
            Materials = new HashSet<Material>();
            Moduls = new HashSet<Modul>();
            StudentsCourses = new HashSet<StudentsCourse>();
            TeachersCourses = new HashSet<TeachersCourse>();
            Tests = new HashSet<Test>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public byte Difficulty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDelited { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Modul> Moduls { get; set; }
        public virtual ICollection<StudentsCourse> StudentsCourses { get; set; }
        public virtual ICollection<TeachersCourse> TeachersCourses { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
