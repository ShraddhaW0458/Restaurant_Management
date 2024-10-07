using Restaurant_Management.IServices;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Management.Services
{
    public class LoginService : ILogin    //Impliment interface 
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();

        public string SaveUser(User UserObj)
        {
            try
            {
                tblUser u = new tblUser();
                u.FullName = UserObj.FullName;
                u.Email = UserObj.Email;
                u.MobileNo = string.IsNullOrEmpty(UserObj.MobileNo) ? (int?)null : int.Parse(UserObj.MobileNo);
                u.UserRoleId = UserObj.UserRoleId;
                u.Password = UserObj.Password;
                u.UserName = UserObj.UserName;
                u.BranchId = UserObj.BranchId;

                ctx.tblUsers.Add(u);

                ctx.SaveChanges();
                string msg = $"{u.FullName} your account has been created successfully. Kindly login with your registered username({u.UserName}) and password.";
                return msg;
            }
            catch (Exception er)
            {

                return er.Message;
            }
        }
    }
}