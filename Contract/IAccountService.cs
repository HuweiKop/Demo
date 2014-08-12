using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IAccountService
    {
        User Login(string username, string password);

        List<string> GetResourcesByUserId(Guid userId);

        PagedList<User> GetAllUsers(User user, string orderBy, string orderAs, int page);

        List<Role> GetAllRoles();

        User GetUserById(Guid userId);

        Role GetRoleById(Guid roleId);

        bool InsertUser(User user);

        bool InsertRole(Role role);

        bool UpdateUser(User user);

        bool UpdateRole(Role role);

        bool DeleteUser(Guid userId);

        bool DeleteRole(Guid roleId);

        bool CheckRoleIsUsed(Guid roleId);

        List<User_Role> GetRoleByUserId(Guid userId);

        List<User_Role> GetUserByRoleId(Guid roleId);

        List<string> GetRoleIdsByUserId(string userId);

        List<string> GetUserIdsByRoleId(string roleId);

        bool AddUserRole(string user, string role);

        bool DeleteUserRole(Guid userRoleId);

        List<Resource> GetAllResource();

        List<string> GetResourceIdByRoleId(Guid roleId);

        bool UpdatePermission(Guid roleId, string resourceIds);

        bool ChangeUserStatus(string userId);
    }
}
