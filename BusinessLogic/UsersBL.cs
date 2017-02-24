using System;
using System.Linq;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class UsersBL
    {
        public IQueryable<Client> GetClients()//fowarding the the call to the get users method from usersRepository
        {
            return new UsersRepository().GetClients(); //getusers is found  in the users repository
        }

        public IQueryable<Client> GetClients(string keyword)//calling the method in usersrepository
        {
            if (keyword == string.Empty)
            {
                return GetClients();//return a list with all users(above method)
            }
            else
            {
                return new UsersRepository().GetClients(keyword);
            }
        }

        public IQueryable<Town> GetTowns()
        {
            return new UsersRepository().GetTowns();
        }

        public void RegisterUser(Client c)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();

            ur.Entity = rr.Entity;

            if (ur.DoesUsernameExist(c.Username))//do or email aswell (do a method like the does username exists in the repository.. put the email verification in an else if statement
            {
                throw new Exception("Username already exists. Please input a different one");
            }
            else if(ur.DoesEmailExist(c.Email)){
                throw new Exception("Email already exists. Please input a different one");
            }
            else
            {
                c.UserType = 2;
                ur.AddClient(c);
                Role defaultRole = rr.GetRoleById(2); //getting the user role from the database
                rr.AllocateRole(defaultRole, c); //allocating the role to the user
               
            }
        }



        public bool Login(string username, string password)
        {
            //you allow the user up tp 3 failed attempts(for home assignment add blocked in the database.
            //check if user is blocked
            if(new UsersRepository().GetClient(username).Blocked == false)
            {
                return new UsersRepository().Login(username, password);
            }
            else
            {
                return false;
            }
            
        }

        public void LoginFailed(string username)
        {
            new UsersRepository().LoginFailed(username);
        }

        public void resetAttempts(string username)
        {
            new UsersRepository().resetAttempts(username);
        }


        public Client GetUser(string username)
        {
            return new UsersRepository().GetClient(username);
        }

        public void AddUserType(UserType ut)
        {
            UsersRepository ur = new UsersRepository();
            //checking if the isbn already exists
            if (ur.GetUserType(ut.Type) == null)
            {
                ur.AddUserType(ut);
            }
            else
            {
                throw new Exception("Usertype already exists");
            }
        }


    }
}
