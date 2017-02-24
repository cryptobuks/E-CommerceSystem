using System;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic;
using Common;
using System.Web.Security;

namespace Assignment.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Search() //search for a user
        {
            IQueryable<Client> listClients = new UsersBL().GetClients(); //i am storing the list of users
            return View(listClients); //displaying all the clients immediatly

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Search(string keyword)//we have to call the method in the business logic layer
        {
            IQueryable<Client> foundClients = new UsersBL().GetClients(keyword);
            return View(foundClients); //the returned list of users will he be displayed
                                     //in the same view tha was loaded in the first action
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new Client());
        }

        [HttpPost]
        public ActionResult Register(Client c)
        {

            string password = c.Password;

            //to hash password
            try
            {
                c.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

                new UsersBL().RegisterUser(c);

                // ModelState.Clear(); //to clear the boxes

                return RedirectToAction("Index", "Index");
            }
            catch (Exception ex)
            {
                ViewData["msg"] = ex.Message;
                return View(c);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Session.Clear();
            try
            {
                if (new UsersBL().Login(username, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5")))
                {
                    //successful
                    new UsersBL().resetAttempts(username);
                    //this authentication cookie is a textfile which is saved on the clients machine which keeps track of whether the user has already been logged in or not.
                    FormsAuthentication.SetAuthCookie(username, true);

                    //redirecting the user to an action called Search in the books controller
                    return RedirectToAction("Index", "Index");//search is the name of action and books is the controller
                }
                else if (new UsersBL().GetUser(username).Blocked)
                {
                    Session["msg"] = "Your account has been blocked. Please contact administrator.";
                    return View();
                }
                else
                {
                    new UsersBL().LoginFailed(username);
                    Session["msg"] = "Authentication failed. Please try again."; //so that when we refreash the data is still shown 
                    return View();
                }
            }
            catch
            {
                Session["msg"] = "Authentication Failed. Please try again";
                return RedirectToAction("Login", "Users");
                
            }
        }

        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Index");
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult CreateUserType()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        //so that the user will input the type details
        [HttpPost]
        public ActionResult CreateUserType(UserType ut)
        {
            try
            {
                new UsersBL().AddUserType(ut); //to add the type in the database
                ViewData["msg"] = "Success";
                ModelState.Clear(); //to clear the boxes
            }
            catch
            {
                ViewData["msg"] = "User Type already exists";
            }


            return View();
        }



    }
}
