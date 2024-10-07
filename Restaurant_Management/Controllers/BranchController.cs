using Restaurant_Management.IServices;
using Restaurant_Management.Models;
using Restaurant_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Management.Controllers
{
    public class BranchController : Controller
    {
        IBranch _Branch;

        // Constructor for the AccountController that takes an IBranchService parameter (injected via Dependency Injection).
        public BranchController(IBranch Branch)  
        {
            // Assigns the injected IBranchService instance to the private _Branch field so it can be used within the controller.
            _Branch = Branch;        
        }
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        // GET: Branch
        public ActionResult CreateBranch()
        {
            ViewBag.StateId = _Branch.State_Dropdown();
            ViewBag.CityId = _Branch.City_Dropdown();
            ViewBag.CountryId = _Branch.Country_Dropdown();
            ViewBag.BranchList = _Branch.BranchList();
            return View();
        }
    }
}