using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public bool IsDelited { get; set; }

        public virtual Course Course { get; set; }
    }
}
