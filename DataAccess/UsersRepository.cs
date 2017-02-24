using System.Linq;
using Common;

namespace DataAccess
{
    public class UsersRepository : ConnectionClass
    {
        public UsersRepository() : base() //to access base constructor public ConnectionClass()
        {

        }

        public IQueryable<Client> GetClients() //to get the list of users . IQueryable is like a list
        {
            return Entity.Clients;
        }

        public bool DoesUsernameExist(string username)//to check if a username exists 
        {
            return (Entity.Clients.Count(x => x.Username == username) > 0);
        }

        public bool DoesEmailExist(string email)//to check if a username exists 
        {
            return (Entity.Clients.Count(x => x.Email == email) > 0);
        }


        public IQueryable<Client> GetClients(string keyword)//searching for a user with surname or name
        {


            return Entity.Clients.Where(u => u.FirstName.Contains(keyword) || u.LastName.Contains(keyword));//=> means for each user
                                                                                                         //where always returns a list so we cannot use it to return oe person like that username                                                      //getting the users where that keyword matches with the username
        }

        public Client GetClient(string username) //to get only one user with a username. the singleordefault is used instead of the where because we have to return only one record and not a list
        {
            return Entity.Clients.SingleOrDefault(c => c.Username == username); //has to be exactly like.
        }

        public void AddClient(Client c)//to add a new user 
        {
            Entity.Clients.Add(c);
            Entity.SaveChanges();
        }

        public IQueryable<Town> GetTowns()
        {
            return Entity.Towns;
        }

        public bool Login(string username, string password)
        {
            
            return (GetClient(username).Password == password);
        }

        public void LoginFailed(string username)
        {
            Client c = GetClient(username);
            c.LoginAttempts += 1;
            if (c.LoginAttempts == 3)
                c.Blocked = true;

            Entity.Entry(c).State = System.Data.Entity.EntityState.Modified;
            Entity.SaveChanges();
        }

        public void resetAttempts(string username)
        {
            Client c = GetClient(username);
            c.LoginAttempts = 0;
     
            Entity.Entry(c).State = System.Data.Entity.EntityState.Modified;
            Entity.SaveChanges();
        }


        //to add user
        public void AddUserType(UserType ut)
        {
            Entity.UserTypes.Add(ut);
            Entity.SaveChanges();
        }

        public UserType GetUserType(string type) //to get only one user with a username. the singleordefault is used instead of the where because we have to return only one record and not a list
        {
            //returns a single object displaying null if not found
            return Entity.UserTypes.SingleOrDefault(ut => ut.Type == type); //has to be exactly like.
        }




    }
        

    }

