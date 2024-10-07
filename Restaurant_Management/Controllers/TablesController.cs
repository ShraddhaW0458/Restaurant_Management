using Restaurant_Management.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Restaurant_Management.Models;

namespace Restaurant_Management.Controllers
{
    public class TablesController : Controller
    {
        ITables _Tables;
        IBranch  _Branch;
        Mapper _Map;

        public TablesController(ITables tables, IBranch branch)
        {
            _Tables = tables;
            _Branch = branch;

            MapperConfiguration mapConfig = new MapperConfiguration(m => m.CreateMap<tblTable, Tables>().ReverseMap());
            _Map = new Mapper(mapConfig);
        }

        [HttpGet]
        // GET: Tables
        public ActionResult CreateTable(int Id=0)
        {
            List<tblTable> tableList = _Tables.GetAll();
            List<Tables> tList = _Map.Map<List<Tables>>(tableList);
            ViewBag.Tables = tList;

            var branchList = _Branch.BranchList().Select(s => new { s.Id, s.Address }).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Id", "Address");

            if (Id > 0 )
            {
                tblTable tb = _Tables.Get(Id);
                if(tb!=null)
                {
                    Tables table = _Map.Map<Tables>(tb);
                    return View(table);
                }
            }

            return View();
        }

        [HttpPost]
        // GET: Tables
        public ActionResult CreateTable(Tables tab)
        {

            tblTable t = _Map.Map<tblTable>(tab);
            bool res = _Tables.Save(t);
            if(res == true)
            {
                ViewBag.msg = "Saved Successfully";
            }
            else
            {
                ViewBag.msg = "Failed to Save";
            }

            ModelState.Clear();

            List<tblTable> tableList = _Tables.GetAll();
            List<Tables> tList = _Map.Map<List<Tables>>(tableList);
            ViewBag.Tables = tList;

            var branchList = _Branch.BranchList().Select(s => new { s.Id, s.Address }).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Id", "Address");

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if(Id>0)
            {
                bool res = _Tables.Delete(Id);
                if(res == true)
                {
                    TempData["msg"] = "Deleted Successfully";
                }
                else
                {
                    TempData["msg"] = "Failed to save";
                }
            }
            return RedirectToAction("CreateTable");
        }
    }
}