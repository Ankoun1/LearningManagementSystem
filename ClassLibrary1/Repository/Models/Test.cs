using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Test
    {
        public Test()
        {
            Assessments = new HashSet<Assessment>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeSpan Continuity { get; set; }
        public bool IsDelited { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
