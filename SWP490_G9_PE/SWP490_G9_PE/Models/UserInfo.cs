﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SWP490_G9_PE.Models
{
    public partial class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserInfo() { }
        public UserInfo(int id, string fName, string lName, string email, string pass, DateTime crD)
        {
            this.UserId = id;
            this.FirstName = fName;
            this.LastName = lName;
            this.Email = email;
            this.Password = pass;
            this.CreatedDate = crD;
        }
        public UserInfo(string fName, string lName, string email, string pass, DateTime crD)
        {
            this.FirstName = fName;
            this.LastName = lName;
            this.Email = email;
            this.Password = pass;
            this.CreatedDate = crD;
        }
    }
}
