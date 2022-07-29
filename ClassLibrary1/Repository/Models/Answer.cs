using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public bool IsItTrue { get; set; }
        public bool IsDelited { get; set; }

        public virtual Question Question { get; set; }
    }
}
