using AutoMapper;
using Restaurant_Management.IServices;
using Restaurant_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Management.Controllers
{
    public class MasterController : Controller
    {
        ITables _Tables;
        Mapper _Map;
        ICustomer _Customer;
        IMaster _Master;
        IOrderItems _OrderItems;
        public MasterController(ITables tables, ICustomer customer, IMaster master, IOrderItems orderitems)
        {
            _Tables = tables;
            _Customer = customer;
            _Master = master;
            _OrderItems = orderitems;

            MapperConfiguration mapConfig = new MapperConfiguration(m =>
            {
                m.CreateMap<tblTable, Tables>().ReverseMap();
                m.CreateMap<Customer, tblCustomer>().ReverseMap(); // Add this line to map Customer to tblCustomer
                m.CreateMap<Order_Items, tblOrderItem>()
                .ForMember(dest => dest.tblMenu, opt => opt.Ignore())  // Ignore navigation properties if not needed
                .ForMember(dest => dest.tblOrder, opt => opt.Ignore()).ReverseMap();
            }); 

            _Map = new Mapper(mapConfig);
            

        }
        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();
        // GET: Master
        public ActionResult Index()
        {
            int LogUser_BranchId = (int)TempData.Peek("BranchId_LogUser");

            List<tblTable> tableList = _Tables.GetAll_ByBranchId(LogUser_BranchId);
            List<Tables> tList = _Map.Map<List<Tables>>(tableList);
            ViewBag.Tables = tList;

            ViewBag.ItemList = new SelectList(ctx.tblMenus.ToList(), "Id", "ItemName");
            return View();
        }

        [HttpPost]

        public ActionResult SaveCustomer(Customer obj)
        {
            //Map Customer to tblCustomer.
            tblCustomer newCust = _Map.Map<tblCustomer>(obj);

            //mapped customer obj(entity) saved in db. and get that saved customer Id.
            int customerId =  _Customer.Save_Cust(newCust);

            //Create reservation obj
            tblReservation r = new tblReservation();
            r.TableId =  Convert.ToInt32(obj.TableNumber);     //This will get from custObj means from UI side
            r.CustomerId = customerId;         //This will get from above saved customer
            r.Status = "O";
            r.ReservationTimeFrom = DateTime.Now;
            r.CreatedOn = DateTime.Now;
            r.CreatedByUserId = 1;

            //Save above reservation object in db, and get that saved  rewservation Id.
            int ReservationId = _Master.SaveReservation(r);

            //Create Order obj.
            tblOrder o = new tblOrder();
            o.ReservationId = ReservationId;
            o.Status = "I";
            o.OrderDateTime = DateTime.Now;
            o.CreatedOn = DateTime.Now;
            o.CreatedByUserId = 1;

            //Save above order object in db, and get that saved  order Id.
            int orderId = _Master.SaveOrder(o);

            return Json(orderId);
        }

        [HttpPost]

        public ActionResult SaveOrderItems( Order_Items obj)
        {
            // Map and save the order item
            tblOrderItem newOrderItem = _Map.Map<tblOrderItem>(obj);

             _OrderItems.Save_OrderItems(newOrderItem);

            // Get and return the updated list of order items
            List<tblOrderItem> OrderItemList = _Master.GetOrderItemListByOrderId(Convert.ToInt32(obj.OrderId));

            return Json(OrderItemList); 
        }


    }
}