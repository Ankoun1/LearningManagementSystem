using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class UsersRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDelited { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
