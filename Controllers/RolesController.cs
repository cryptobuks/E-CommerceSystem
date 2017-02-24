using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult AllocateDeallocate()
        {
            return View(new RolesModel());//rolesmodel is the model to pass it to the view
        }

        [Authorize(Roles = "Administrator")]
        //we need to create a model in order to do two dropdowns comming from two different things(users,roles)
        [HttpPost]
        public ActionResult AllocateDeallocate(string choice, RolesModel data) //rolesmodel is to read from the dropdown lists
        {
            try
            {
                if (choice == "Allocate")
                {
                    new BusinessLogic.RolesBL().AllocateRole(data.RoleId, data.Username);
                    data.Message = "Role Allocated to the selected user";
                }
                else
                {
                    new BusinessLogic.RolesBL().DeAllocateRole(data.RoleId, data.Username);
                    data.Message = "Role Deallocated from the selected user ";
                }

                
            }
            catch (Exception ex)
            {
                data.Message = ex.Message;//this exception message is already set in rolesBL and we are calling it from here
            }

            //if the view expects a model, then inside the brackets you should pass an object of the same datatype as the model
            return View(data); //we have to do return view data in order to display the message
        }
    }
}