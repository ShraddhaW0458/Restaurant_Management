using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Management.Controllers
{
    public class CommonController : Controller
    {
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        //public PartialViewResult getNav()
        //{
        //    return PartialView("_NavHeader", ctx.tblNavHeaders.Where(w => w.IsHeader == true).ToList());
        //}

        //public PartialViewResult getFooter()
        //{
        //    return PartialView("_Footer", ctx.tblNavHeaders.Where(w => w.IsHeader == false).ToList());
        //}

    }
}