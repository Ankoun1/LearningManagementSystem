using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class User
    {
        public User()
        {
            UsersRoles = new HashSet<UsersRole>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsDelited { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
