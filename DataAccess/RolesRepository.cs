using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class RolesRepository : ConnectionClass
    {
        public RolesRepository() : base()//to initialize the model object(Entity)
        {

        }
        //getting a list of roles
        public IQueryable<Role> GetRoles()
        {
            return Entity.Roles;
        }

        //inputing an id of role and displaying its role
        public Role GetRoleById(int id)
        {
            return Entity.Roles.SingleOrDefault(r => r.RoleID == id);
        }

        //getting the roles for a username(person)
        public IQueryable<Role> GetRolesForUser(string username)
        {
            Client myUser = new UsersRepository().GetClient(username);
            return myUser.Roles.AsQueryable();
        }

        public IQueryable<Client> GetUsersForRole(int RoleId)
        {
            //using the method getROleBYID above to get the roles of the id inserted
            Role r = GetRoleById(RoleId);
            return r.Clients.AsQueryable();
        }

        //checking if a user has the role selcted to not locate twice
        public bool IsUserInRole(string username, int roleId)
        {
            Client u = Entity.Clients.SingleOrDefault(x => x.Username == username);
            Role r = GetRoleById(roleId);

            return (u.Roles.Contains(r));

            //this is the same as the return
            /*  if (u.Roles.Contains(r))
              {
                  return true;
              }
              else return false;*/


        }

        public void AllocateRole(Role r, Client u)
        {
            u.Roles.Add(r);
            //r.Users.Add(u); this is the same as the above
            Entity.SaveChanges();
        }

        public void DeAllocateRole(Role r, Client u)
        {
            u.Roles.Remove(r);
            Entity.SaveChanges();
        }

    }
}
