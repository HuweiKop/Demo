using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class User
    {
        public string RePassword { get; set; }

        private List<Role> roles;
        public virtual List<Role> Roles 
        {
            get
            {
                if (roles != null)
                { return roles; }
                using (Entities entities = new Entities())
                {
                    roles = (from ur in entities.User_Role
                             join r in entities.Roles
                                on ur.RoleId equals r.Id
                                where ur.UserId == Id
                             select r).ToList<Role>();
                    return roles;
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
                                   where ur.UserId == Id
                                   select ur.Id).ToList<Guid>();
                    return userRoleIds;
                }
            }
        }

        public string Check()
        {
            if (!string.IsNullOrWhiteSpace(this.RePassword) && this.Password != this.RePassword)
                return "两次密码不一样";
            using (Entities entities = new Entities())
            {
                int count = (from u in entities.Users
                                             where u.Id != this.Id
                                             && u.UserName == this.UserName
                                             select u).Count();
                if (count > 0)
                    return "该用户已被注册";
            }
            return "OK";
        }
    }
}
