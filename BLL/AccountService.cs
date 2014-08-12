using DAL;
using Model;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Contract;

namespace BLL
{
    public class AccountService : IAccountService
    {
        //IDbContextBase context = DbContextFactory.GetDbContext();

        public User Login(string username, string password)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    User user = context.FindFirstOrDefault<User>(obj => obj.UserName.Equals(username) && obj.Password.Equals(password));

                    return user;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<string> GetResourcesByUserId(Guid userId)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<string> listResource = (from ur in entities.User_Role
                                                 join p in entities.Permissions
                                                 on ur.RoleId equals p.RoleId
                                                 join r in entities.Resources
                                                 on p.ResourceId equals r.Id
                                                 where ur.UserId == userId
                                                 select r.Name).Distinct()
                                                   .ToList<string>();

                    return listResource;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public PagedList<User> GetAllUsers(User user, string orderBy, string orderAs, int page)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    string userName = user == null ? "" : user.UserName + "";
                    PagedList<User> listUser = context.FindAllByPage<User>(obj => obj.UserName.Contains(userName), orderBy, orderAs, page);
                    return listUser;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public List<Role> GetAllRoles()
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    List<Role> listRole = context.FindAll<Role>();
                    return listRole;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public User GetUserById(Guid userId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Find<User>(userId);
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public Role GetRoleById(Guid roleId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Find<Role>(roleId);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool InsertUser(User user)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    user.Id = Guid.NewGuid();
                    return context.Insert<User>(user);
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool InsertRole(Role role)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    role.Id = Guid.NewGuid();
                    return context.Insert<Role>(role);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Update<User>(user);
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool UpdateRole(Role role)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Update<Role>(role);
                }
            }
            catch
            {

                throw;
            }
        }

        public bool DeleteUser(Guid userId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    context.Delete<User>(userId, false);
                    context.Delete<User_Role>(obj => obj.UserId == userId, false);

                    return context.SaveChanges();
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool DeleteRole(Guid roleId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    context.Delete<Role>(roleId, false);
                    context.Delete<User_Role>(obj => obj.RoleId == roleId, false);
                    context.Delete<Permission>(obj => obj.RoleId == roleId, false);

                    return context.SaveChanges();
                }
            }
            catch
            {

                throw;
            }
        }

        public bool CheckRoleIsUsed(Guid roleId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    int count = context.Find<Role>(roleId).User_Role.Count();
                    return count > 0;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public List<User_Role> GetRoleByUserId(Guid userId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    List<User_Role> listRole = context.FindAll<User_Role>(obj => obj.UserId == userId);

                    return listRole;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public List<User_Role> GetUserByRoleId(Guid roleId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    List<User_Role> listRole = context.FindAll<User_Role>(obj => obj.RoleId == roleId);

                    return listRole;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public List<string> GetRoleIdsByUserId(string userId)
        {
            try
            {
                SqlService sqlService = SqlServiceFactory.GetSQLService();
                string sql = "select RoleId from User_Role where UserId=@UserId";
                List<SqlParameter> listPar = new List<SqlParameter>();
                listPar.Add(new SqlParameter("UserId", userId));

                return sqlService.QueryOneColumn(sql, listPar);
            }
            catch 
            {
                
                throw;
            }
        }

        public List<string> GetUserIdsByRoleId(string roleId)
        {
            try
            {
                SqlService sqlService = SqlServiceFactory.GetSQLService();
                string sql = "select UserId from User_Role where RoleId=@RoleId";
                List<SqlParameter> listPar = new List<SqlParameter>();
                listPar.Add(new SqlParameter("RoleId", roleId));

                return sqlService.QueryOneColumn(sql, listPar);
            }
            catch
            {

                throw;
            }
        }

        public bool AddUserRole(string userIds, string roleIds)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    string[] arrUserId = userIds.Split(',');
                    string[] arrRoleId = roleIds.Split(',');

                    User_Role userRole = null;
                    for (int i = 0; i < arrUserId.Length; i++)
                    {
                        for (int j = 0; j < arrRoleId.Length; j++)
                        {
                            userRole = new User_Role();
                            userRole.Id = Guid.NewGuid();
                            userRole.RoleId = new Guid(arrRoleId[j]);
                            userRole.UserId = new Guid(arrUserId[i]);

                            context.Insert<User_Role>(userRole, false);
                        }
                    }

                    return context.SaveChanges();
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool DeleteUserRole(Guid userRoleId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    return context.Delete<User_Role>(userRoleId);
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public List<Resource> GetAllResource()
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    SqlService sqlService = SqlServiceFactory.GetSQLService();
                    string sql = "select * from Resource where Id!='0'";
                    List<Resource> listResource = sqlService.GetEntities<Resource>(sql);

                    return listResource;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public List<string> GetResourceIdByRoleId(Guid roleId)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    SqlService sqlService = SqlServiceFactory.GetSQLService();
                    string sql = "select ResourceId from Permission where RoleId=@RoleId";
                    List<SqlParameter> listPar = new List<SqlParameter>();
                    listPar.Add(new SqlParameter("RoleId", roleId));

                    return sqlService.QueryOneColumn(sql, listPar);
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool UpdatePermission(Guid roleId, string resourceIds)
        {
            try
            {
                using (IDbContextBase context = DbContextFactory.GetDbContext())
                {
                    string[] arrResourceId = resourceIds.Split(',');
                    Permission permission = null;

                    context.Delete<Permission>(obj => obj.RoleId == roleId, false);

                    for (int i = 0; i < arrResourceId.Length; i++)
                    {
                        permission = new Permission();
                        permission.Id = Guid.NewGuid();
                        permission.RoleId = roleId;
                        permission.ResourceId = arrResourceId[i];

                        context.Insert<Permission>(permission, false);
                    }

                    return context.SaveChanges();
                }
            }
            catch 
            {
                
                throw;
            }
        }

        public bool ChangeUserStatus(string userId)
        {
            SqlService service = SqlServiceFactory.GetSQLService();
            string sql = "update [User] set IsBan=(IsBan+1)%2 where Id=@userId";
            List<SqlParameter> listPar = new List<SqlParameter>();
            listPar.Add(new SqlParameter("@userId", userId));
            return service.ExecuteNonQueryReturnBool(sql, listPar);
        }
    }
}
