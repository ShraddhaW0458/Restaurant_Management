using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Restaurant_Management.IServices;
using Restaurant_Management.Models;

namespace Restaurant_Management.Controllers
{
    public class CustomerController : Controller
    {
        ICustomer _Customer;
        ICountry _Country;
        IState _State;
        ICity _City;
        Mapper _Map;

        public CustomerController( ICountry country, IState state, ICity city, ICustomer customer)
        {
            _Customer = customer;
            _Country = country;
            _State = state;
            _City = city;

            MapperConfiguration mapConfig = new MapperConfiguration(m => m.CreateMap<Customer, tblCustomer>().ReverseMap());
            _Map = new Mapper(mapConfig);
        }
        // GET: Customer
        [HttpGet]
        public ActionResult CreateCustomer(int Id = 0)
        {
            List<tblCustomer> custList = _Customer.GetAll();
            List<Customer> cList = _Map.Map<List<Customer>>(custList);
            ViewBag.Customers = cList;

            var countryList = _Country.GetAll().Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.CountryId = new SelectList(countryList, "Id", "Name");

            var stateList = _State.GetAll().Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.StateId = new SelectList(stateList, "Id", "Name");

            var cityList = _City.GetAll().Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.CityId = new SelectList(cityList, "Id", "Name");

            if (Id > 0)
            {
                tblCustomer ctm = _Customer.Get(Id);
                if (ctm != null)
                {
                    Customer C = _Map.Map<Customer>(ctm);

                    return View(C);
                }
            }

            return View(new Customer());
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer cust)
        {

            tblCustomer c = _Map.Map<tblCustomer>(cust);
            bool res = _Customer.Save(c);
            
            if(res == true)
            {
                ViewBag.msg = "Saved Successfully.";
            }
            else
            {
                ViewBag.msg = "Failed to Save.";
            }

            ModelState.Clear();

            var countryList = _Country.GetAll().Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.CountryId = new SelectList(countryList, "Id", "Name");

            var stateList = _State.GetAll().Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.StateId = new SelectList(stateList, "Id", "Name");

            var cityList = _City.GetAll().Select(s => new { s.Id, s.Name }).ToList();
            ViewBag.CityId = new SelectList(cityList, "Id", "Name");

            List<tblCustomer> custList = _Customer.GetAll();
            List<Customer> cList = _Map.Map<List<Customer>>(custList);
            ViewBag.Customers = cList;

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if(Id>0)
            {
                bool res = _Customer.Delete(Id);
                if(res == true)
                {
                    TempData["msg"] = "Deleted Successfully.";
                }
                else
                {
                    TempData["msg"] = "Failed to delete.";
                }
            }
            return RedirectToAction("CreateCustomer");
        }
    }
}