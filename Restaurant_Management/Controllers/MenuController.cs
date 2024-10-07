using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Restaurant_Management.Models;

namespace Restaurant_Management.Controllers
{
    public class MenuController : Controller
    {
        IMenu _Menu;
        Mapper _Map;
        public MenuController(IMenu menu)
        {
            _Menu = menu;

            MapperConfiguration mapConfig = new MapperConfiguration(m => m.CreateMap<tblMenu, Menu_Items>().ReverseMap());
            _Map = new Mapper(mapConfig);
        }

        [HttpGet]
        // GET: Menu
        public ActionResult CreateMenu(int Id=0)
        {
            List<tblMenu> mList = _Menu.GetAll();
            List<Menu_Items> menuList = _Map.Map<List<Menu_Items>>(mList);
            ViewBag.Menu = menuList;

            if(Id>0)
            {
                tblMenu me = _Menu.Get(Id);
                if(me!=null)
                {
                    Menu_Items men = _Map.Map<Menu_Items>(me);
                    return View(men);
                }

            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateMenu(Menu_Items menu)
        {
            tblMenu m = _Map.Map<tblMenu>(menu);
            bool res = _Menu.Save(m);
            if(res == true)
            {
                ViewBag.msg = "Saved successfully.";
            }
            else
            {
                ViewBag.msg = "Failed to Save.";
            }

            ModelState.Clear();

            List<tblMenu> mList = _Menu.GetAll();
            List<Menu_Items> menuList = _Map.Map<List<Menu_Items>>(mList);
            ViewBag.Menu = menuList;

            ViewBag.Categories = new List<SelectListItem>
            {
                new SelectListItem { Text = "Starters", Value = "S" },
                new SelectListItem { Text = "Main course", Value = "M" },
                new SelectListItem { Text = "Dessert", Value = "D" },
                new SelectListItem { Text = "Refreshment", Value = "R" }
            };

            ViewBag.Availabilities = new List<SelectListItem>
            {
                new SelectListItem { Text = "Available", Value = "A" },
                new SelectListItem { Text = "Unavailable", Value = "U" }
            };

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                bool res = _Menu.Delete(Id);
                if(res == true)
                {
                    TempData["msg"] = "Deleted Successfully.";
                }
                else
                {
                    TempData["msg"] = "Failed to delete.";
                }
            }
            return RedirectToAction("CreateMenu");
        }
    }
}