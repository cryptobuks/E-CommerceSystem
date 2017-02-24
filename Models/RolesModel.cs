using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic;
using System.Web.Mvc;

namespace Assignment.Models
{

        public class RolesModel
        {
           
            public SelectList Users { get; set; }
            public SelectList Roles { get; set; }

            public int RoleId { get; set; } //this will hold the selected RoleId
            public string Username { get; set; } //thiswill hold he selected username

            public string Message { get; set; }

        public RolesModel()
            {
                Users = new SelectList(new UsersBL().GetClients(), "Username", "Username"); //first parameter is the primary key, the second parameter is the one you are going to display to the user
                Roles = new SelectList(new RolesBL().GetRoles(), "RoleID", "Type");
            }
        
    }
}