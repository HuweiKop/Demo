using BLL;
using Model;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RET.Controllers
{
    public class AccountController : Web.ControllerBase
    {
        //
        // GET: /Account/

        #region 页面加载

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name=""></param>
        public ActionResult BackLogin(string isAjax)
        {
            if (isAjax == "Yes")
            {
                return JavaScript("top.location.href = '/Account/Login'");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public string DoRegister(User user)
        {
            string result = user.Check();
            if (!"OK".Equals(result))
                return result;
            return AccountService.InsertUser(user) ? "OK" : "Error";
        }

        public ActionResult DoLogin(string username, string password)
        {
            try
            {
                User user = AccountService.Login(username, password);
                if (user != null)
                {
                    Session["UserId"] = user.Id;
                    return RedirectToAction("MainPage", "Main");
                }

                ViewData["ErrorMassage"] = "用户名或密码不正确";

                return RedirectToAction("Login", "Account");
            }
            catch 
            {
                
                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("UserManagement", Order = 2)]
        public ActionResult UserList(User user, int page, string orderBy, string orderAs)
        {
            try
            {
                ViewData["UserName"] = user.UserName;
                PagedList<User> listUser = AccountService.GetAllUsers(user, orderBy, orderAs, page);
                return View(listUser);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("RoleManagement", Order=2)]
        public ActionResult RoleList()
        {
            try
            {
                List<Role> listRole = AccountService.GetAllRoles();
                return View(listRole);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("UserManagement", Order = 2)]
        public ActionResult UserEdit(string userId)
        {
            try
            {
                ViewData["operate"] = "Add";
                User user = new User();
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    ViewData["operate"] = "Update";
                    user = AccountService.GetUserById(new Guid(userId));
                }

                return View(user);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("RoleManagement", Order = 2)]
        public ActionResult RoleEdit(string roleId)
        {
            try
            {
                ViewData["operate"] = "Add";
                Role role = new Role();
                if (!string.IsNullOrWhiteSpace(roleId))
                {
                    ViewData["operate"] = "Update";
                    role = AccountService.GetRoleById(new Guid(roleId));
                }

                return View(role);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("UserManagement", Order = 2)]
        public ActionResult RoleListDialog(string userId)
        {
            try
            {
                List<Role> listRole = AccountService.GetAllRoles();
                List<string> alreadyRole = AccountService.GetRoleIdsByUserId(userId);
                ViewData["alreadyRole"] = JsonTools.JsonSerializer(alreadyRole);
                return View(listRole);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("RoleManagement", Order = 2)]
        public ActionResult UserListDialog(string roleId)
        {
            try
            {
                List<User> listUser = AccountService.GetAllUsers(null, "UserName", "asc", 1);
                List<string> alreadyUser = AccountService.GetUserIdsByRoleId(roleId);
                ViewData["alreadyUser"] = JsonTools.JsonSerializer(alreadyUser);
                return View(listUser);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("UserManagement", Order = 2)]
        public ActionResult RoleListByUserId(string userId)
        {
            try
            {
                ViewData["userId"] = userId;
                var listRole = AccountService.GetUserById(new Guid(userId));

                return View(listRole);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("RoleManagement", Order = 2)]
        public ActionResult UserListByRoleId(string roleId)
        {
            try
            {
                ViewData["roleId"] = roleId;
                var listUser = AccountService.GetRoleById(new Guid(roleId));

                return View(listUser);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("RoleManagement", Order = 2)]
        public ActionResult ResourceList(string roleId)
        {
            try
            {
                ViewData["roleId"] = roleId;
                List<Resource> listResource = AccountService.GetAllResource();
                List<string> alreadyResource = AccountService.GetResourceIdByRoleId(new Guid(roleId));
                ViewData["resource"] = JsonTools.JsonSerializer(listResource);
                ViewData["alreadyResource"] = JsonTools.JsonSerializer(alreadyResource);

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 页面事件

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string EditUser(User user, string operate)
        {
            try
            {
                if ("Update".Equals(operate))
                {
                    return AccountService.UpdateUser(user) ? "OK" : "Error";
                }
                string result = user.Check();
                if (!"OK".Equals(result))
                    return result;
                return AccountService.InsertUser(user) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string EditRole(Role role, string operate)
        {
            try
            {
                if ("Update".Equals(operate))
                {
                    return AccountService.UpdateRole(role) ? "OK" : "Error";
                }
                return AccountService.InsertRole(role) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string DeleteUser(string userId)
        {
            try
            {
                return AccountService.DeleteUser(new Guid(userId)) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string DeleteRole(string roleId)
        {
            try
            {
                if (AccountService.CheckRoleIsUsed(new Guid(roleId)))
                    return "该角色正在被使用，删除前请确保没有用户拥有该角色";
                return AccountService.DeleteRole(new Guid(roleId)) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string AddUserRole(string userIds, string roleIds)
        {
            try
            {
                return AccountService.AddUserRole(userIds, roleIds) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string DeleteUserRole(string userRoleId)
        {
            try
            {
                return AccountService.DeleteUserRole(new Guid(userRoleId)) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string UpdatePermission(string roleId, string resourceIds)
        {
            try
            {
                return AccountService.UpdatePermission(new Guid(roleId), resourceIds) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string ChangeUserStatus(string userId)
        {
            try
            {
                return AccountService.ChangeUserStatus(userId) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
