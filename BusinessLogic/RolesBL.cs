using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class RolesBL
    {

        public IQueryable<Role> GetRoles()
        {
            return new RolesRepository().GetRoles();
        }


        public Role GetRoleById(int id)
        {
            return new RolesRepository().GetRoleById(id);
        }


        public IQueryable<Role> GetRolesForUser(string username)
        {
            return new RolesRepository().GetRolesForUser(username);
        }

        public IQueryable<Client> GetUsersForRole(int RoleId)
        {
            return new RolesRepository().GetUsersForRole(RoleId);
        }


        public bool IsUserInRole(string username, int roleId)
        {
            return new RolesRepository().IsUserInRole(username, roleId);
        }

        //it makes it eassier for the website to just pass theroleId and the username instead of passing a whole role dtetails and a whole user details.
        public void AllocateRole(int roleId, String username)
        {

            RolesRepository rr = new RolesRepository();
            Role r = rr.GetRoleById(roleId);//getting the role

            UsersRepository ur = new UsersRepository();
            ur.Entity = rr.Entity; //we put the repositries  in the same locations so that we can call multiple repositories
                                   //it is important to do this line before you use the second method (getUSer)

            Client u = ur.GetClient(username);//getting the user

            if (rr.IsUserInRole(username, roleId) == false)
                rr.AllocateRole(r, u);
            else
                throw new Exception("Role has been already allocated to the selected user");


        }


        public void DeAllocateRole(int roleId, String username)
        {
            RolesRepository rr = new RolesRepository();
            Role r = rr.GetRoleById(roleId);//getting the role

            UsersRepository ur = new UsersRepository();
            ur.Entity = rr.Entity; //we put the repositries  in the same locations so that we can call multiple repositories
                                   //it is important to do this line before you use the second method (getUSer)

            Client u = ur.GetClient(username);//getting the user

            if (rr.IsUserInRole(username, roleId) == true)
                rr.DeAllocateRole(r, u);
            else
                throw new Exception("Role is not allocated to that user");
        }
    }
}
