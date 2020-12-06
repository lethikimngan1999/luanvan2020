using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using quanlybenh.DataModels.Entities;

namespace quanlybenh.Filters
{

    public class FeatureAuthentication : AuthorizationFilterAttribute
    {
        public int _action { get; set; }
        public FeatureAuthentication(int action)
        {
            _action = action;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            var _context = new AppDbContext();
            var currentUserId = GetCurrentUserId;
            var menuName = GetMenu(actionContext.Request);
            if (currentUserId == "")
            {
                return;
            }
            if (CheckRoleAdmin())
            {
                return;
            }
            var left = GetRolePermissionLeft(currentUserId, menuName);
            var sql = GetRolePermissionLeft(currentUserId, menuName);
            var entities = sql.ToList();
            if (!entities.Any())
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                return;

            }
            var havePermission = false;
            foreach (RolePermission rolePsermission in entities)
            {
                var namePermission = Enum.GetName(typeof(Utilities.Configurations.Constants.Action), _action);
            }
            if (!havePermission)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }

        private List<RolePermission> GetRolePermissionLeft(string currentUserId, string menuName)
        {
            var _context = new AppDbContext();
            var listUser = _context.Users.Where(p => p.Status != "Deleted");
            var listUserRoles = _context.UserRoles;
            var listRoles = _context.Roles.Where(p => p.Status != "Deleted");
            var listRolePermission = _context.RolePermissions.Where(p => p.Status != "Deleted");
            var listMenu = _context.Menus.Where(p => p.Status != "Deleted");

            var result = from users in listUser
                         join userRoles in listUserRoles on users.Id equals userRoles.UserId
                         join roles in listRoles on userRoles.RoleId equals roles.Id
                         join rolePermission in listRolePermission on roles.Id equals rolePermission.RoleId
                         join menu in listMenu on rolePermission.MenuId equals menu.MenuId
                         where users.Id.ToString().ToLower() == currentUserId.ToLower()
                         && menu.MenuId.ToString().ToLower() == menuName.ToLower()
                         select rolePermission;
            return result.ToList();
        }

        private bool CheckRoleAdmin()
        {
            var _context = new AppDbContext();
            var currentUserId = GetCurrentUserId;
            var listUserRoles = _context.UserRoles.Where(p => p.UserId.ToString() == currentUserId).ToList();
            var roleAdmin = _context.Roles.Where(p => p.Name == "Admin").ToList()[0];
            foreach (var userRole in listUserRoles)
            {
                if (userRole.RoleId == roleAdmin.Id)
                {
                    return true;
                }

            }
            return false;
        }
        private string GetMenu(HttpRequestMessage httpRequest)
        {
            var path = httpRequest.RequestUri.ToString();
            var arrayPath = path.Split('/');
            var menuName = arrayPath[4];
            return menuName;
        }

        public string GetCurrentUserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }
    }
}