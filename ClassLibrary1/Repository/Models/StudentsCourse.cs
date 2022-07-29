using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class StudentsCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public byte? Score { get; set; }
        public bool IsDelited { get; set; }

        public virtual Student Course { get; set; }
        public virtual Course Student { get; set; }
    }
}
