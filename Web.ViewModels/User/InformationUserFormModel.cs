﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels.User
{
    public class InformationUserFormModel
    {        
        public int Id { get; set; }

        public string FullName { get; set; }

        
        public string Password { get; set; }

       
        public string Email { get; set; }

        
        public string Role { get; set; }
    }
}