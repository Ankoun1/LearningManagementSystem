using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class TeachersCourse
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool IsDelited { get; set; }

        public virtual Teacher Course { get; set; }
        public virtual Course Teacher { get; set; }
    }
}
