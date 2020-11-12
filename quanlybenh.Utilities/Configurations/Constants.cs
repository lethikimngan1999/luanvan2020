using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.Utilities.Configurations
{
    public static class Constants
    {
        public static class StatusObject
        {
            public const string AddNew = "New";
            public const string Cancelled = "Cancelled";
            public const string Deleted = "Deleted";
            public const string Locked = "Locked";
            public const string Active = "Active";
            public const string InActive = "InActive";
            public const string Deactivated = "Deactivated";
            public const string True = "True";
        }
        public static class Message
        {
            public static readonly string UserNameNotExists = "UserName not exists ";
            public static readonly string UpdateNotSuccess = "Update not success!";
            public static readonly string DeleteNotSuccess = "Delete not success!";
            public static readonly string DeleteEmployeePositionNotSuccess = "Employee Position exists in Employee. Delete not success!";
            public static readonly string DeleteRoomTypeNotSuccess = "Room Type exists. Delete not success!";
            public static readonly string UserNameExists = "UserName exists ";
            public static readonly string UserNotExists = "User not exists ";
            public static readonly string EmailExists = "Email exists ";
            public static readonly string MasterDataExists = "MasterData Name exists";
            public static readonly string MasterDataNotExists = "MasterData Name not exists";
            public static readonly string GroupNameExists = "Group name exists";
            public static readonly string GroupNameNotExists = "Group name not exists";
            public static readonly string RoleNameExists = "Role Name exists";
            public static readonly string RoleNameNotExists = "Role Name exists";
            public static readonly string RoleNotExists = "Role not exists ";
            public static readonly string RoleNotExistsWithUserID = "Role not exists with UserId";
            public static readonly string UserNotExistsWithRoleID = "User not exists with RoleId";
            public static readonly string LoginFaile = "UserName or password incorrect";
            public static readonly string OldPasswordNotCorrect = GetOldPasswordNotCorrect();
            public static readonly string CreateNotSuccess = "Create not success!";
            public static readonly string CreateEmployeeNotSuccess = "Identity Card Number exists. Create Employee not success!";

            public static readonly string GetDataNotSuccess = "Get data not success!";
            public static readonly string EmailTemplateNotExists = "Email template not exists!";
            public static readonly string EmailSendNotSuccess = "Send email not success!";
            public static readonly string LoginFalse = "Login false!";
            private static string GetOldPasswordNotCorrect()
            {
                return "Old Password is incorrect";
            }
        }

        public static class MenuItem
        {
            public static readonly string User = "Tài khoản người dùng";
            public static readonly string Role = "Vai trò";
            public static readonly string Menu = "Menu";
            public static readonly string KhacHang = "Khách hàng";
            public static readonly string EmployeePosition = "Chức vụ";
            public static readonly string Employee = "Nhân viên";
        }

        public enum Action
        {
            CanRead = 1,
            CanCreate = 2,
            CanUpdate = 3,
            CanDelete = 4,
            CanImport = 5,
            CanExport = 6
        }

        public static class RoleConstant
        {
            public static readonly string RoleId_Empty = "00000000-0000-0000-0000-000000000000";
        }

        public static class TrieuChungConstant
        {
            public static readonly string MaTrieuChung_Empty = "00000000-0000-0000-0000-000000000000";
        }

        public static class LieuTrinhConstant
        {
            public static readonly string MaLieuTrinh_Empty = "00000000-0000-0000-0000-000000000000";
        }

    }

}
