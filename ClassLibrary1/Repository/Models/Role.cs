using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Repository.Models
{
    public partial class Role
    {
        public Role()
        {
            UsersRoles = new HashSet<UsersRole>();
        }

        public int Id { get; set; }
        public string NameRole { get; set; }

        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
