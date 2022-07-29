using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Assessment
    {
        public int Id { get; set; }
        public int TeastId { get; set; }
        public byte Result { get; set; }
        public byte NegativCount { get; set; }
        public int StudentId { get; set; }

        public virtual Test Teast { get; set; }
    }
}
