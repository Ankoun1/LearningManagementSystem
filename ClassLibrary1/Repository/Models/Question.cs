using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public string Description { get; set; }
        public bool IsDelited { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
