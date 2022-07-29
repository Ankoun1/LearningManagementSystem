using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Modul
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Description { get; set; }
        public string Titel { get; set; }
        public bool IsDelited { get; set; }

        public virtual Course Course { get; set; }
    }
}
