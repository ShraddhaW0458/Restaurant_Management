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
        IOrder _Order;
        IReservation _Reservation;
        IMenu _Menu;

        public MasterController(ITables tables, ICustomer customer, IMaster master, IOrderItems orderitems, IOrder order, IReservation reservation, IMenu menu)
        {
            _Tables = tables;
            _Customer = customer;
            _Master = master;
            _OrderItems = orderitems;
            _Order = order;
            _Reservation = reservation;
            _Menu = menu;

            MapperConfiguration mapConfig = new MapperConfiguration(m =>
            {
                m.CreateMap<tblTable, Tables>().ReverseMap();
                m.CreateMap<Customer, tblCustomer>().ReverseMap(); // Add this line to map Customer to tblCustomer
                m.CreateMap<Order_Items, tblOrderItem>()
                .ForMember(dest => dest.tblMenu, opt => opt.Ignore())  // Ignore navigation properties if not needed
                .ForMember(dest => dest.tblOrder, opt => opt.Ignore()).ReverseMap();
                m.CreateMap<Order, tblOrder>().ReverseMap();
                m.CreateMap<Reservation, tblReservation>().ReverseMap();
                m.CreateMap<Menu, tblMenu>().ReverseMap();

            }); 

            _Map = new Mapper(mapConfig);
            

        }

        Restaurant_ManagementEntities1 ctx = new Restaurant_ManagementEntities1();

        [HttpGet]
        // GET: Master
        public ActionResult Index()
        {
            int? LogUser_BranchId = (int?)TempData.Peek("BranchId_LogUser");

            if(LogUser_BranchId == null)
            {
                return RedirectToAction("Login", "User");
            }

            List<Tables> tableList = _Tables.GetAll_ByBranchId(LogUser_BranchId.Value);
            //List<Tables> tList = _Map.Map<List<Tables>>(tableList);
            ViewBag.Tables = tableList;

            ViewBag.ItemList = new SelectList(_Menu.GetAll().ToList(), "Id", "ItemName");
            return View();
        }

        #region Save Customer
        [HttpPost]
        public ActionResult SaveCustomer(Customer obj)
        {
            //Map Customer to tblCustomer.
            tblCustomer newCust = _Map.Map<tblCustomer>(obj);
            
            //mapped customer obj(entity) saved in db. and get that saved customer Id.
            int customerId = _Customer.Save_Cust(newCust);

            //Create reservation obj
            tblReservation r = new tblReservation();
            r.TableId = Convert.ToInt32(obj.TableNumber);     //This will get from custObj means from UI side
            r.CustomerId = customerId;         //This will get from above saved customer
            r.Status = "O";
            r.ReservationTimeFrom = DateTime.Now;
            r.CreatedOn = DateTime.Now;
            r.CreatedByUserId = 1;

            //Save above reservation object in db, and get that saved  reservation Id.
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

            return Json(orderId, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Save Order Items 
        [HttpPost]
        public ActionResult SaveOrderItems(Order_Items obj)
        {
            // Map and save the order item
            tblOrderItem newOrderItem = _Map.Map<tblOrderItem>(obj);
            newOrderItem.TotalPrice = newOrderItem.Quantity * newOrderItem.ItemPrice;
            

            _OrderItems.Save_OrderItems(newOrderItem);

            // Get and return the updated list of order items
            List<tblOrderItem> OrderItemList = _Master.GetOrderItemListByOrderId(Convert.ToInt32(obj.OrderId));

            var result = OrderItemList.Select(item => new
            {
                item.Id,
                item.tblMenu.ItemName,
                item.ItemPrice,
                item.TotalPrice,
                item.Quantity,
                item.Status,
                item.Portion
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Check Reservation
        public ActionResult CheckTable(int TableNumber, int Id)
        {
            var res = _Reservation.GetAll()
                .Where(w => w.TableId == TableNumber)
                .OrderByDescending(o => o.CreatedOn)
                .Select(s => new { s.Status, s.TableId, s.CustomerId, s.Id })
                .FirstOrDefault();

            if (res!=null && res.Status != "A")
            {
                //tblCustomer c = ctx.tblCustomers.Where(w => w.Id == res.CustomerId).FirstOrDefault();
                tblCustomer c = _Customer.GetAll().Where(w => w.Id == res.CustomerId).FirstOrDefault();

                //tblOrder o = ctx.tblOrders.Where(w => w.ReservationId == res.Id).FirstOrDefault();
                tblOrder o = _Order.GetAll().Where(w => w.ReservationId == res.Id).FirstOrDefault();
                
                List<tblOrderItem> OrderItemList = new List<tblOrderItem>();
                if (ctx != null && o != null)
                {
                    //OrderItemList = ctx.tblOrderItems.Where(w => w.OrderId == o.Id).ToList();
                    OrderItemList = _OrderItems.GetAll().Where(w => w.OrderId == o.Id).ToList();
                }

                //Ordered Items Grid
                var result = OrderItemList.Select(item => new
                    {
                        item.Id,
                        item.tblMenu.ItemName,
                        item.ItemPrice,
                        item.TotalPrice,
                        item.Quantity,
                        item.Status,
                        item.Portion
                    }).ToList();


                    // Assuming you already have c, o, and result from your existing code:
                    var output = new
                    {
                        customerName = c?.Name ?? "Unknown",  // Adjust to actual property for customer name
                        customerMobileNo = c?.MobileNo ?? "N/A",

                        orderId = o?.Id ?? 0,

                        orderItems = result // The list of selected items
                    };
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
               
            else
            {
                return Json(false);
            }

        }
        #endregion

        #region CheckOut
        [HttpPost]
        public ActionResult CheckOut(int orderId, float GrandTotal)
        {
            // Update the Order table
            //var order = ctx.tblOrders.Where(o => o.Id == orderId).FirstOrDefault();
            var order = _Order.GetAll().Where(o => o.Id == orderId).FirstOrDefault();

            if (order == null)
            {
                return Json(new { success = false, message = "Order not found." });
            }

            List<tblOrderItem> OrderItemList = _OrderItems.GetAll().Where(w => w.OrderId == orderId).ToList();

            GrandTotal = 0;
            foreach (var item in OrderItemList)
            {
                GrandTotal = GrandTotal + (float)item.TotalPrice;

            }

            // Set/Update total price, status, and end time
            order.TotalPrice = GrandTotal;
            order.Status = "C";               //Order Status completed
            order.OrderDateTime = DateTime.Now;
            order.CreatedOn = DateTime.Now;

            // Save changes to the Order table
            ctx.SaveChanges();

            int reservationId = (int)order.ReservationId;
            var reservation = _Reservation.GetAll().Where(o => o.Id == reservationId).FirstOrDefault();
            if (reservation == null)
            {
                return Json(new { success = false, message = "Reservation not found." });
            }

            // Set status and end time
            reservation.Status = "A";           //reservation status available
            reservation.ReservationTimeTo = DateTime.Now;
            reservation.CreatedOn = DateTime.Now;

            ctx.SaveChanges();
            var result = OrderItemList.Select(item => new
            {
                item.Id,
                item.tblMenu.ItemName,
                item.ItemPrice,
                item.TotalPrice,
                item.Quantity,
                item.Status,
                item.Portion
            }).ToList();

            string RestaurantName = "Hotel Radhesh";
            string RestAddress = "Shanivar Peth, Karad, Satara";
            DateTime DateNow = DateTime.Now;
            int TableNumber = (int)reservation.TableId;       //retrive form tblReservation.
            int BillNo = new Random().Next(1000, 9999); // Generate random bill number
            string GSTNO = "27AAZPA0999B1ZG";
         

            return Json(new
            {
                success = true,
                reservationId = reservationId,
                grandTotal = GrandTotal,
                result = result,
                billData = new
                {
                    RestaurantName,
                    RestAddress,
                    DateNow,
                    TableNumber,
                    BillNo,
                    GSTNO,
                    orderId
                }
            });
        
        }

        #endregion

        #region Bill Payment
        [HttpPost]
        public ActionResult SavePayment(int billOrderId, string FoodTotal)
        {
            try
            {
                tblPaymentTable p = new tblPaymentTable();
                p.OrderId = billOrderId;
                p.PaymentAmount = Convert.ToDouble(FoodTotal);
                p.PaymentDateTime = DateTime.Now;
                p.PaymentStatus = "C";

                ctx.tblPaymentTables.Add(p);
                ctx.SaveChanges();
                return Json("Payment Saved Successfully");
            }
            catch (Exception er)
            {

                return Json("Failed to save", er.Message);
            }
           
        }

        #endregion
    }
}