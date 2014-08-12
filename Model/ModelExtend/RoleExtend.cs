using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Role
    {
        private List<User> users;
        public virtual List<User> Users 
        {
            get
            {
                if (users != null)
                { return users; }
                using (Entities entities = new Entities())
                {
                    users = (from ur in entities.User_Role
                             join u in entities.Users
                                on ur.UserId equals u.Id
                                where ur.RoleId == Id
                             select u).ToList<User>();
                    return users;
                }
            }
        }

        private List<Guid> userRoleIds;
        public virtual List<Guid> UserRoleIds
        {
            get
            {
                if (userRoleIds != null)
                    return userRoleIds;
                using (Entities entities = new Entities())
                {
                    userRoleIds = (from ur in entities.User_Role
                                   where ur.RoleId == Id
                                   select ur.Id).ToList<Guid>();
                    return userRoleIds;
                }
            }
        }
    }
}
