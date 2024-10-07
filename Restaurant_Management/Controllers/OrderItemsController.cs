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
    public class OrderItemsController : Controller
    {
        IOrderItems _OrderItems;
        Mapper _Map;

        public OrderItemsController(IOrderItems orderitems)
        {
            _OrderItems = orderitems;

            MapperConfiguration mapConfig = new MapperConfiguration(m => m.CreateMap<IOrderItems, Order_Items>().ReverseMap());
            _Map = new Mapper(mapConfig);
        }
        // GET: OrderItems
        [HttpGet]
        public ActionResult CreateOrderItems()
        {
            return View();
        }

        public ActionResult CreateOrderItems(Order_Items obj)
        {
            return View();
        }
    }
}