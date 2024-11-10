using Restaurant_Management.IServices;
using Restaurant_Management.Models;
using Restaurant_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Restaurant_Management.Controllers
{
    public class UserController : Controller
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        ILogin _Login;
        IUserRole _UserRole;
        IBranch _Branch;
        public UserController(ILogin Login, IUserRole UserRole, IBranch Branch) 
        {
            _Login = Login;
            _UserRole = UserRole;
            _Branch = Branch;
        }
        // GET: User
        [HttpGet]
        public ActionResult Create()
        {
            if (TempData.Peek("OTP") != null)
            {
                int Otp = (int)TempData.Peek("OTP");
            }

            var userRoleList = _UserRole.GetAll().Select(s=> new { s.Id, s.RoleName }).ToList();
            ViewBag.UserRoleId = new SelectList(userRoleList , "Id", "RoleName");

            var branchList = _Branch.BranchList().Select(s => new { s.Id, s.Address }).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Id", "Address");

            return View();
        }

        [HttpPost]
        public ActionResult Create(User obj)
        {
            var userRoleList = _UserRole.GetAll().Select(s => new { s.Id, s.RoleName }).ToList();
            ViewBag.UserRoleId = new SelectList(userRoleList, "Id", "RoleName");

            var branchList = _Branch.BranchList().Select(s => new { s.Id, s.Address }).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Id", "Address");

            if (ModelState.IsValid)
            {
                //Generate 6 digit OTP
                Random random = new Random();
                int OTP = random.Next(100000, 1000000);

                // Store user and OTP in TempData
                TempData["User"] = obj;
                TempData["OTP"] = OTP;
                //send otp on email or text msg

                return RedirectToAction("VerifyOTP");
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOTP(OTPVM obj)
        {
            int TempOTP = (int)TempData.Peek("OTP");     //Keeping the OTP for multiple pages

            if (obj.OTP == TempOTP)
            {
                //Save the user data
                User UserObj = (User)TempData.Peek("User");
                if (UserObj != null)
                {
                    TempData["msg"] = _Login.SaveUser(UserObj);
                    return RedirectToAction("Login","User");
                }
            }
            else
            {
                ViewBag.Message = "Invalid OTP. Please try again";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login obj)
        {
            var User = ctx.tblUsers.Where(w => w.UserName == obj.UserName && w.Password == obj.Password).FirstOrDefault();

            if(ModelState.IsValid)
            {
                if (User != null)
                {
                    TempData["BranchId_LogUser"] = User.BranchId;    
                    //Activate User Context
                    return RedirectToAction("Index", "Master");
                }

                ViewBag.Msg = "Invalid Credentials.Please try again.";
            }
            return View(obj);
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}